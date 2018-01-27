using Assets.Scripts.Echoes;
using Assets.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Characters.NPC
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Monster : NonPlayerCharacter
    {
        private NavMeshAgent agent;

        private int currentTarget;
        private int previousTarget;

        private GameObject[] checkpointList;
        private bool investigating = false;
        private bool waiting = false;

        private bool chasingPlayer = false;

        private IEnumerator coroutine;

        private EcoLocationAudioSource echoSound;

        public SoundContainer MonsterAmbientSoundContainer;
        public SoundContainer MonsterChaseSoundContainer;

        private List<AudioClip> MonsterAmbientSounds = new List<AudioClip>();
        private List<AudioClip> MonsterChaseSounds = new List<AudioClip>();
        
        private AudioSource audioSource;
        // Use this for initialization
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            checkpointList = GameObject.FindGameObjectsWithTag("Waypoint");
            previousTarget = 0;
            currentTarget = GetClosestWaypoint();
            var nextPath = (checkpointList[currentTarget].GetComponent("Waypoint") as Waypoint).GetNextPath(checkpointList[previousTarget].transform);
            agent.SetDestination(nextPath.transform.position);
            echoSound = this.GetComponent<EcoLocationAudioSource>();
            GameManager.Instance.RegisterMonster(this);
            MonsterAmbientSounds = MonsterAmbientSoundContainer.ListOfSounds;
            MonsterChaseSounds = MonsterChaseSoundContainer.ListOfSounds;
            audioSource = this.GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            if (agent.remainingDistance < 0.5f && !investigating && !waiting)
            {
                var nextPath = (checkpointList[currentTarget].GetComponent("Waypoint") as Waypoint).GetNextPath(checkpointList[previousTarget].transform);
                agent.SetDestination(nextPath.transform.position);
                previousTarget = currentTarget;
                currentTarget = FindIndexInList(nextPath);
            }
            else if(agent.remainingDistance < 0.5f && investigating)
            {
                if(coroutine != null)
                {
                    StopCoroutine(coroutine);
                }                
                previousTarget = currentTarget;
                currentTarget = GetClosestWaypoint();
                investigating = false;
                waiting = true;
                coroutine = WaitForSeconds(5.0f);
                StartCoroutine(coroutine);
            }

            if(!audioSource.isPlaying)
            {
                PlaySoundWithoutRing(MonsterAmbientSounds);
            }
            CheckIfNearPlayer();
        }

        int GetClosestWaypoint()
        {
            float distance = Mathf.Infinity;
            var closest = 0;
            for(int i=0; i<checkpointList.Length; i++)
            {
                var diff = checkpointList[i].transform.position - this.transform.position;
                var currentDistance = diff.sqrMagnitude;
                if(currentDistance < distance)
                {
                    closest = i;
                    distance = currentDistance;
                }
            }

            return closest;
        }

        int FindIndexInList(GameObject next)
        {
            for (int i = 0; i < checkpointList.Length; i++)
            {
                if (checkpointList[i] == next)
                {
                    return i;
                }
            }
            return 0;
        }

        public void WalkToPosition(Vector3 pos)
        {
            investigating = true;
            agent.SetDestination(pos);
        }

        IEnumerator WaitForSeconds(float time)
        {
            yield return new WaitForSeconds(time);
            waiting = false;
        }

        public void StartChasingPlayer()
        {
            chasingPlayer = true;
            PlaySoundWithRing(MonsterChaseSounds);
            InvokeRepeating("UpdateChase", 0.1f, 1.0f);
        }

        public void UpdateChase()
        {
            echoSound.PlaySonarRing(10.0f);
        }

        public void StopChasingPlayer()
        {
            if(IsInvoking("UpdateChase"))
            {
                CancelInvoke("UpdateChase");
            }
            chasingPlayer = false;
            previousTarget = currentTarget;
            currentTarget = GetClosestWaypoint();
            var nextPath = (checkpointList[currentTarget].GetComponent("Waypoint") as Waypoint).GetNextPath(checkpointList[previousTarget].transform);
            agent.SetDestination(nextPath.transform.position);

        }

        void PlaySoundWithoutRing(List<AudioClip> soundList)
        {
            var audio = soundList[Random.Range(0, soundList.Count)];
            audioSource.clip = audio;
            audioSource.Play();
        }

        void PlaySoundWithRing(List<AudioClip> soundList)
        {
            var audio = soundList[Random.Range(0, soundList.Count)];
            echoSound.PlaySound(audio, transform.position);
        }

        void CheckIfNearPlayer()
        {
            var player = GameManager.Instance.Player;

            if (player && player.isAlive)
            {
                var aggroDistance = 5.0f;
                var distance = (transform.position - player.transform.position).magnitude;
                if (distance <= aggroDistance)
                {
                    if (distance < 0.5f)
                    {
                        GameManager.Instance.KillPlayer();
                    }
                    if (!chasingPlayer)
                    {
                        StartChasingPlayer();
                    }
                    WalkToPosition(player.transform.position);
                }
                else if (distance > aggroDistance)
                {
                    if (chasingPlayer)
                    {
                        StopChasingPlayer();
                    }
                }
            }
        }

    }
}
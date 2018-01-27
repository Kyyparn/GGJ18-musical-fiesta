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

        private IEnumerator coroutine;

        private EcoLocationAudioSource echoSound;

        public SoundContainer MonsterSoundContainer;

        private List<AudioClip> MonsterAmbientSounds = new List<AudioClip>();
        // Use this for initialization
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            checkpointList = GameObject.FindGameObjectsWithTag("Waypoint");
            previousTarget = 0;
            currentTarget = GetClosestWaypoint();
            var nextPath = (checkpointList[currentTarget].GetComponent("Waypoint") as Waypoint).GetNextPath(checkpointList[previousTarget].transform);
            agent.SetDestination(nextPath.transform.position);
            echoSound = gameObject.GetComponent<EcoLocationAudioSource>();
            GameManager.Instance.RegisterMonster(this);
            MonsterAmbientSounds = MonsterSoundContainer.ListOfSounds;
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
    }
}
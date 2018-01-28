using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.Characters.Player;
using Assets.Scripts.Characters.NPC;

namespace Assets.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public Player Player { get; set; }

        public RectTransform gameOverPrefab;

        protected List<Monster> monsters = new List<Monster>();

        private void Awake()
        {
            Instance = this;
        }

        public void RegisterMonster(Monster monster)
        {
            monsters.Add(monster);
        }

        public void SoundWasPlayed(Vector3 position, float distance)
        {
            foreach(Monster monster in monsters)
            {
                if((monster.transform.position - position).magnitude <= distance * 0.1f)
                {
                    monster.WalkToPosition(position);
                }
            }
        }

        public void KillPlayer()
        {
            if (Player.isAlive)
            {
                Player.isAlive = false;

                var fps = Player.GetComponent<FirstPersonController>();

                fps.SetMouseLockActive(false);
                Destroy(fps);

                GameObject.Instantiate(gameOverPrefab);
            }
        }
    }
}

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

        protected List<Monster> monsters;

        public void RegisterMonster(Monster monster)
        {
            monsters.Add(monster);
        }

        public void SoundWasPlayed(Vector3 position, float distance)
        {
            foreach(Monster monster in monsters)
            {
                if((monster.transform.position - position).magnitude <= distance)
                {
                    monster.WalkToPosition(position);
                }
            }
        }
    }
}

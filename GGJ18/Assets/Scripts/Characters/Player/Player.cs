using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.Characters;
using Assets.Scripts.Managers;
using System;
using Assets.Scripts.Echoes;

namespace Assets.Scripts.Characters.Player
{
    public class Player : Character
    {
        public bool isAlive = true;

        [Header("Scream")]
        public SoundContainer screamSounds;
        public EcoLocationAudioSource scream;
        public float screamCooldown = 2.0f;
        private DateTime lastScream; 



        void Start()
        {
            GameManager.Instance.Player = this;
            lastScream = DateTime.MinValue;
        }

        public void Scream()
        {
            if ((DateTime.Now - lastScream).TotalSeconds >= screamCooldown)
            {
                lastScream = DateTime.Now;

                var audio = screamSounds.ListOfSounds[UnityEngine.Random.Range(0, screamSounds.ListOfSounds.Count)];
                scream.PlaySound(audio, transform.position);
            }
        }
    }
}

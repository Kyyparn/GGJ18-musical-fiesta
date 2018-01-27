using Assets.Scripts.Echoes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Characters.Player
{
    public class PlayerInputComponent : MonoBehaviour
    {
        public List<AudioClip> screamSounds;

        public EcoLocationAudioSource scream;

        void Update()
        {
            if(Input.GetButtonDown("Fire1"))
            {
                var audio = screamSounds[Random.Range(0, screamSounds.Count)];
                scream.PlaySound(audio);
            }
        }
    }
}

using Assets.Scripts.Echoes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Characters.Player
{
    public class PlayerInputComponent : MonoBehaviour
    {
        public EcoLocationAudioSource scream;

        void Update()
        {
            if(Input.GetButtonDown("Fire1"))
            {
                scream.PlaySound();
            }
        }
    }
}

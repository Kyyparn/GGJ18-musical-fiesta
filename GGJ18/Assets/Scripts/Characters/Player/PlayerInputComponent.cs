using Assets.Scripts.Echoes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Characters.Player
{
    [RequireComponent(typeof(Player))]
    public class PlayerInputComponent : MonoBehaviour
    {
        void Update()
        {
            if(Input.GetButtonDown("Fire1"))
            {
                GetComponent<Player>().Scream();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.Characters;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Characters.Player
{
    public class Player : Character
    {
        void Start()
        {
            GameManager.Instance.Player = this;
        }
    }
}

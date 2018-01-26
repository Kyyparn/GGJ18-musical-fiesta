using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.Characters.Player;

namespace Assets.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public Player Player { get; set; }
    }
}

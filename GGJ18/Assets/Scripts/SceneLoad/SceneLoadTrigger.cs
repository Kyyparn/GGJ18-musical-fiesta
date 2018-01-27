using Assets.Scripts.Characters.Player;
using Assets.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.SceneLoad
{
    public class SceneLoadTrigger : MonoBehaviour {

        public string sceneName;

        void OnTriggerEnter(Collider other)
        {
            var player = other.gameObject.GetComponent<Player>();

            if(player)
            {
                SceneLoadManager.Instance.LoadSceneWithRelativePlayerSpawnPosition(sceneName);
            }
        }
    }
}

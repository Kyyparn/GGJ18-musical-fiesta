using Assets.Scripts.Characters.Player;
using Assets.Scripts.SceneLoad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Managers
{
    public class SceneLoadManager : MonoBehaviour
    {
        public static SceneLoadManager Instance;

        public Player playerPrefab;

        RelativePoint entryPoint;
        RelativePoint endPoint;

        RelativeData relativeData;

        private void Awake()
        {
            Instance = this;

            DontDestroyOnLoad(this);
        }

        public void RegisterEntryPoint(RelativePoint entryPoint)
        {
            this.entryPoint = entryPoint;

            SpawnPlayer();
        }

        public void RegisterEndPoint(RelativePoint endPoint)
        {
            this.endPoint = endPoint;
        }

        public void LoadSceneWithRelativePlayerSpawnPosition(string sceneName)
        {
            if (endPoint)
            {
                Player player = GameManager.Instance.Player;

                relativeData.position = endPoint.transform.InverseTransformPoint(player.transform.position);
                relativeData.playerRotation = endPoint.transform.InverseTransformDirection(player.transform.forward);
                relativeData.cameraRotation = player.GetComponentInChildren<Camera>().transform.localRotation;
            }

            SceneManager.LoadScene(sceneName);
        }

        public void LoadScene(string sceneName)
        {
            relativeData.position = new Vector3();
            relativeData.playerRotation = Vector3.forward;
            relativeData.cameraRotation = Quaternion.identity;

            SceneManager.LoadScene(sceneName);
        }

        public void ReloadScene()
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        void SpawnPlayer()
        {
            if (entryPoint)
            {
                Player player = GameObject.Instantiate(playerPrefab);

                player.transform.position = entryPoint.transform.TransformPoint(relativeData.position);
                player.transform.rotation = Quaternion.LookRotation(entryPoint.transform.TransformDirection(relativeData.playerRotation), Vector3.up);
                player.GetComponentInChildren<Camera>().transform.localRotation = relativeData.cameraRotation;
            }
        }
    }
}

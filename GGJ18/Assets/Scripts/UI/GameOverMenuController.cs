using Assets.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class GameOverMenuController : MonoBehaviour
    {

        public void RestartLevel()
        {
            SceneLoadManager.Instance.ReloadScene();
        }

        public void GotoMainMenu()
        {
            SceneLoadManager.Instance.LoadScene("MainMenu_Level");
        }
    }
}

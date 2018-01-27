using Assets.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class MainMenuController : MonoBehaviour
    {

        public RectTransform MainMenuPanel;
        public RectTransform LevelSelectPanel;

        List<RectTransform> panels;

        void Awake()
        {
            panels = new List<RectTransform>();

            panels.Add(MainMenuPanel);
            panels.Add(LevelSelectPanel);

            DisableAllPanels();

            GotoMainMenu();
        }

        private void DisableAllPanels()
        {
            panels.ForEach(p => p.gameObject.SetActive(false));
        }

        public void GotoLevelSelect()
        {
            DisableAllPanels();
            LevelSelectPanel.gameObject.SetActive(true);
        }

        public void GotoMainMenu()
        {
            DisableAllPanels();
            MainMenuPanel.gameObject.SetActive(true);
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public void StartLevel(string levelName)
        {
            SceneLoadManager.Instance.LoadScene(levelName);
        }
    }
}

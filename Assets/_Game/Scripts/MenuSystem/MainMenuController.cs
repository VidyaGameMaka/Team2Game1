using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Team2Game1 {
    public class MainMenuController : MonoBehaviour {
        
        public void StartGame_BTN(string sceneName) {
            SceneManager.LoadScene(sceneName);
        }

        public void QuitGame_BTN() {
            Application.Quit();
        }

    }
}
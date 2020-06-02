using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Team2Game1 {
    public class MainMenuController : MonoBehaviour {
        
        public void StartGame_BTN(string sceneName) {
            GameMaster.soundFX.PlaySound(GameMaster.audioClip_SO.UI_Menu_Button_Confirm);

            SceneManager.LoadScene(sceneName);
        }

        public void QuitGame_BTN() {
            Application.Quit();
        }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Team2Game1 {
    public class MainMenuController : MonoBehaviour {
        
        public void StartGame_BTN(string sceneName) {
            AudioClip clipchoice = GameMaster.audioClip_SO.ZombieSoundGroup[Random.Range(0, GameMaster.audioClip_SO.ZombieSoundGroup.Length)];
            GameMaster.soundFX.PlaySound(clipchoice);

            SceneManager.LoadScene(sceneName);
        }

        public void QuitGame_BTN() {
            Application.Quit();
        }

    }
}
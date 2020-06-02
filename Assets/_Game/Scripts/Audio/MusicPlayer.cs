using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Team2Game1 {
    public class MusicPlayer : MonoBehaviour {

        private void Awake() {
            DontDestroyOnLoad(this);
        }

        private void Start() {
            GameMaster.music.PlayMusic(GameMaster.audioClip_SO.MainMenuMusic);
        }

        private void OnLevelWasLoaded(int level) {
            if (SceneManager.GetActiveScene().name == "MainMenu") {
                GameMaster.music.PlayMusic(GameMaster.audioClip_SO.MainMenuMusic);
            } else {
                GameMaster.music.PlayMusic(GameMaster.audioClip_SO.GameMusic);
            }
        }

    }
}
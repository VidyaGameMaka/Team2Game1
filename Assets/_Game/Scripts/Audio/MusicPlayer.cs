using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Team2Game1 {
    public class MusicPlayer : MonoBehaviour {

        private void Awake() {
            DontDestroyOnLoad(this);
        }

        // called second
        void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            if (SceneManager.GetActiveScene().name == "MainMenu") {
                GameMaster.music.PlayMusic(GameMaster.audioClip_SO.MainMenuMusic);
            } else {
                GameMaster.music.PlayMusic(GameMaster.audioClip_SO.GameMusic);
            }
        }

        // called third
        void Start() {
            SceneManager.sceneLoaded += OnSceneLoaded;
            GameMaster.music.PlayMusic(GameMaster.audioClip_SO.MainMenuMusic);
        }

        // called when the game is terminated
        void OnDisable() {           
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

    }
}
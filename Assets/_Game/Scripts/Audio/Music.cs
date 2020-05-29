using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team2Game1 {
    public class Music : MonoBehaviour {

        private AudioSource audioSource;

        private void Awake() {
            if (GameMaster.music != this && GameMaster.music != null) {
                Destroy(gameObject);
            } else {
                DontDestroyOnLoad(this.gameObject);
            }
        }

        private void Start() {
            audioSource = GetComponent<AudioSource>();
        }

        /// <summary>
        /// Usage: GameMaster.music.PlayMusic(GameMaster.audioClip_SO.BrainSquish);
        /// </summary>
        /// <param name="audioClip"></param>
        public void PlayMusic(AudioClip audioClip) {
            audioSource.clip = audioClip;
            audioSource.Play();
        }

    }
}
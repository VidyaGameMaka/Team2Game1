using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team2Game1 {
    public class SoundFX : MonoBehaviour {
        
        [Header("Audio Source Objects")]
        public AudioSource[] audioSources;

        private void Awake() {
            if (GameMaster.soundFX != this && GameMaster.soundFX != null) {
                Destroy(gameObject);
            } else {
                DontDestroyOnLoad(this.gameObject);
            }
        }

        /// <summary>
        /// Plays a sound effect on an unplaying sound effect object.
        /// Usage: GameMaster.soundFX.PlaySound(GameMaster.audioClip_SO.BrainSquish);
        /// </summary>
        /// <param name="clip"></param>
        public void PlaySound(AudioClip clip) {
            foreach (AudioSource source in audioSources) {
                if (!source.isPlaying) {
                    source.clip = clip;
                    source.Play();
                    return;
                }
            }
        }

    }
}
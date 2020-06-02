using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team2Game1 {
    [CreateAssetMenu]
    public class AudioClips_SO : ScriptableObject {

        public AudioClip Platepickup;
        public AudioClip PlateinTrash;
        public AudioClip BrainSquish;

        public AudioClip[] ZombieSoundGroup;

        public AudioClip MainMenuMusic;
        public AudioClip GameMusic;

        public AudioClip UI_Menu_Button_Confirm;
    }
}
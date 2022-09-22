using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewRhythmSystem
{
    public class SFXManager : MonoBehaviour
    {
        private static SFXManager instance;
        public static SFXManager GetInstance() { return instance; }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Utils.SingletonErrorMessage(this);
            }
        }

        AudioSource audioSource;
        public AudioClip[] aClips;

        // Start is called before the first frame update
        void Start()
        {
            audioSource = this.gameObject.AddComponent<AudioSource>() as AudioSource;
        }

        public void PlayAudio(int index, float volume) 
        { 
            if(index >= 0 && index < aClips.Length)
            {
                audioSource.clip = aClips[index];
                audioSource.volume = volume;
                audioSource.Play();
            }
        }
    }
}


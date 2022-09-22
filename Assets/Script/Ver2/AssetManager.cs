using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace NewRhythmSystem
{
    [System.Serializable]
    public class AssetManager : MonoBehaviour
    {
        #region Singleton stuff
        private static AssetManager instance;
        public static AssetManager GetInstance() { return instance; }
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
        #endregion

        public Sprite[] keySprites;
        public ParticleSystem PerfectParticles;
    }
}


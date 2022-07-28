using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewRhythmSystem
{
    public class KeyInputManager : MonoBehaviour
    {
        private static KeyInputManager instance;
        public static KeyInputManager GetInstance() { return instance; }
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

        private KeyCode[] keys;
        public KeyCode GetKey(int index) => keys[index];

        #region Default Keys
        private KeyCode key0 = KeyCode.UpArrow;
        private KeyCode key1 = KeyCode.LeftArrow;
        private KeyCode key2 = KeyCode.DownArrow;
        private KeyCode key3 = KeyCode.RightArrow;
        #endregion
        // Start is called before the first frame update
        void Start()
        {
            keys = new KeyCode[4];
            keys[0] = key0;
            keys[1] = key1;
            keys[2] = key2;
            keys[3] = key3;
        }
    }

}


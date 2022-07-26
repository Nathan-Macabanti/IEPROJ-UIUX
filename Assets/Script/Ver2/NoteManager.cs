using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewRhythmSystem
{
    public class NoteManager : MonoBehaviour
    {
        private static NoteManager instance;
        public static NoteManager GetInstance() { return instance; }
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Debug.LogError("Multiple instances");
            }
        }

        public Queue<Note> noteQueue = new Queue<Note>();
    }
}


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
                Utils.SingletonErrorMessage(this);
            }
        }

        public Queue<Note> noteQueue = new Queue<Note>();

        public void RemoveNote()
        {
            Note note = noteQueue.Dequeue();
            note.UnregisterPoolable();
        }
    }
}


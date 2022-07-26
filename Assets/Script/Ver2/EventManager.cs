using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NewRhythmSystem
{
    public class EventManager : MonoBehaviour
    {
        #region Singleton
        private static EventManager instance;
        public static EventManager GetInstance() { return instance; }
        private void Awake()
        {
            if (instance == null) { instance = this; }
        }
        #endregion

        public event UnityAction<float> onBeat;
        public event UnityAction<CollectionRank> onNoteCollected;
        public void Beat(float beat) { onBeat?.Invoke(beat); }
        public void NoteCollected(CollectionRank rank) { onNoteCollected?.Invoke(rank); }
    }
}


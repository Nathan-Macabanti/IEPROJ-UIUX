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
            else { Utils.SingletonErrorMessage(this); }
        }
        #endregion

        public event UnityAction<float> onBeat;
        public event UnityAction<HitRank> onNoteHit;
        public void Beat(float beat) { onBeat?.Invoke(beat); }
        public void NoteHit(HitRank rank) { onNoteHit?.Invoke(rank); }
    }
}


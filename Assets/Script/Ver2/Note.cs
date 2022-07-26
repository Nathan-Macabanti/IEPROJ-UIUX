using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewRhythmSystem
{
    public class Note : MonoBehaviour, IPoolable
    {
        #region Variables
        private Transform source;
        private Transform destination;
        private float beat;
        #endregion
        #region Getters
        public float GetBeat() { return beat; }
        #endregion
        public void InitializePoolable() { return; }
        public void UnregisterPoolable() { return; }
        public void InitializeNote(Transform _source, Transform _destination, float _beat)
        {
            source = _source;
            destination = _destination;
            beat = _beat;
        }

        // Update is called once per frame
        void Update()
        {
            Move();
            CheckIfDeactivate();
        }

        public void Move()
        {
            float songPosInBeats = SongManagerRhythmGame.GetInstance().songPositionInBeats;
            float beatsShownInAdvance = SongManagerRhythmGame.GetInstance().beatsShownInAdvance;
            float timeToDestination = (beat - songPosInBeats);
            float distance = (beatsShownInAdvance - timeToDestination) / beatsShownInAdvance;

            transform.position = Vector2.Lerp(source.position, destination.position, distance);
        }

        public void CheckIfDeactivate()
        {
            if (Vector3.Distance(destination.position, this.transform.position) <= 0.01f)
            {
                this.gameObject.SetActive(false);
                NoteManager.GetInstance().noteQueue.Dequeue();
                EventManager.GetInstance().NoteCollected(CollectionRank.MISS);
            }
        }
    }
}


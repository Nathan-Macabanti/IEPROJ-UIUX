using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewRhythmSystem
{
    public class NoteSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] Transform destination;

        private NoteManager noteManagerInstance;

        private void Start()
        {
            noteManagerInstance = NoteManager.GetInstance();
        }
        private void OnEnable()
        {
            EventManager.GetInstance().onBeat += Spawn;
        }

        private void OnDisable()
        {
            EventManager.GetInstance().onBeat -= Spawn;
        }

        public void Spawn(float beat)
        {
            GameObject obj = (GameObject)ObjectPool.GetInstance().GetFromPool("Note", this.transform.position, Quaternion.identity);
            if (obj.TryGetComponent<Note>(out Note note))
            {
                note.InitializeNote(this.transform, destination, beat, SongManager.GetInstance().CurrentNote().key);
                noteManagerInstance.noteQueue.Enqueue(note); //Queue the notes
            }
        }
    }
}


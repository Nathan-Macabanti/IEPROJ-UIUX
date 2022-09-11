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
        private int index = 0;

        private void Start()
        {
            noteManagerInstance = NoteManager.GetInstance();
        }
        private void OnEnable()
        {
            EventManager.GetInstance().onNoteSpawn += Spawn;
        }

        private void OnDisable()
        {
            EventManager.GetInstance().onNoteSpawn -= Spawn;
        }

        public void Spawn(float beat)
        {
            GameObject obj = (GameObject)ObjectPool.GetInstance().GetFromPool("Note", this.transform.position, Quaternion.identity);
            if (obj.TryGetComponent<Note>(out Note note))
            {
                Debug.Log(index);
                note.InitializeNote(this.transform, destination, beat, (int)SongManager.GetInstance().CurrentNote().input);
                noteManagerInstance.noteQueue.Enqueue(note); //Queue the notes
                index++;
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewRhythmSystem
{
    public enum CollectionRank { PERFECT, GOOD, BAD, MISS }
    public class NoteCollector : MonoBehaviour
    {
        [SerializeField] private float offsetGOOD = 0.7f;
        [SerializeField] private float offsetPERFECT = 0.5f;

        private SongManagerRhythmGame songManagerRhythmGameInstance;
        private NoteManager noteManagerInstance;

        private void Start()
        {
            songManagerRhythmGameInstance = SongManagerRhythmGame.GetInstance();
            noteManagerInstance = NoteManager.GetInstance();
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyInputManager.GetInstance().key1))
            {
                CheckIfNoteIsAlligned();
            }
        }

        private void CheckIfNoteIsAlligned()
        {
            Note note = noteManagerInstance.noteQueue.Peek();

            float timeOfLastPress = songManagerRhythmGameInstance.songPositionInBeats;
            float currentbeat = note.GetBeat();
            float difference = currentbeat - timeOfLastPress;

            //minimum distance for collection
            if(difference <= offsetPERFECT)
            {
                EventManager.GetInstance().NoteCollected(CollectionRank.PERFECT);
            }
            else if(difference <= offsetGOOD || difference > offsetPERFECT)
            {
                EventManager.GetInstance().NoteCollected(CollectionRank.GOOD);
            }
            else
            {
                EventManager.GetInstance().NoteCollected(CollectionRank.BAD);
            }
        }
    }
}


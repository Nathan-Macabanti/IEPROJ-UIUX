using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewRhythmSystem
{
    public enum HitRank { PERFECT, GOOD, BAD, MISS }
    public class NoteHitter : MonoBehaviour
    {
        [SerializeField] private float offsetPERFECT = 0.1f;
        [SerializeField] private float offsetGOOD = 0.2f;
        [SerializeField] private float offsetBAD = 0.3f;

        private Note currentNote;
        private SongManager songManagerInstance;
        private NoteManager noteManagerInstance;

        private void Start()
        {
            songManagerInstance = SongManager.GetInstance();
            noteManagerInstance = NoteManager.GetInstance();
        }


        private void Update()
        {
            if (Input.anyKeyDown && noteManagerInstance.noteQueue.Count > 0)
            {
                currentNote = noteManagerInstance.noteQueue.Peek();
                KeyCode key = KeyInputManager.GetInstance().GetKey(currentNote.GetKey());
                if (Input.GetKeyDown(key))
                {
                    CheckIfNoteIsAlligned();
                }
            }
        }

        private void CheckIfNoteIsAlligned()
        {
            //Get the difference of the actual beat time and difference
            float timeOfLastPress = songManagerInstance.songPositionInBeats;
            float currentbeat = currentNote.GetBeat();
            float difference = currentbeat - timeOfLastPress;

            //minimum distance for collection
            if(difference <= offsetPERFECT)
            {
                EventManager.GetInstance().NoteHit(HitRank.PERFECT);
                noteManagerInstance.RemoveNote();
            }
            else if(difference <= offsetGOOD && difference > offsetPERFECT)
            {
                EventManager.GetInstance().NoteHit(HitRank.GOOD);
                noteManagerInstance.RemoveNote();
            }
            else if(difference <= offsetBAD && difference > offsetGOOD)
            {
                EventManager.GetInstance().NoteHit(HitRank.BAD);
                noteManagerInstance.RemoveNote();
            }
#if false
            else
            {
                EventManager.GetInstance().NoteHit(HitRank.MISS);
            }
#endif
        }
    }
}


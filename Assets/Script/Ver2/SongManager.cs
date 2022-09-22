using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewRhythmSystem
{
    public class SongManager : MonoBehaviour
    {
        private void Awake()
        {
            InitializeSingleton();
        }

        #region Singleton
        private static SongManager instance;
        public static SongManager GetInstance() => instance;
        private void InitializeSingleton()
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
        #endregion

        #region Variables
        [SerializeField] private SongInfo songInfo;
        [Tooltip("Spawn notes advance")]
        public float beatsShownInAdvance;

        //Song Position variables for the calculation
        [HideInInspector] public float songPosition;
        [HideInInspector] public float songPositionInBeats;
        [HideInInspector] public float secPerBeat;
        [HideInInspector] public float dsptimesong;

        //Beats per minute, used to measure the speed of the song
        [HideInInspector] public float bpm;
        //The point of time in the song when the note will spawn
        [HideInInspector] public List<NoteInfo> notes;
        //The index of the next note to be spawned
        [HideInInspector] public int index = 0;
        #endregion

        public NoteInfo CurrentNote() { 
            if (index < notes.Count) { 
                return notes[index]; 
            }
            return null;
        }

        // Start is called before the first frame update
        void Start()
        {
            InitializeBeatPositions();
            InitializeMusic(songInfo.aClip);
            GetComponent<AudioSource>().Play();
        }

        private void InitializeMusic(AudioClip clip)
        {
            GetComponent<AudioSource>().clip = clip;
        }

        private void InitializeBeatPositions()
        {
            bpm = songInfo.bpm;
            #region Initialize Note Position In Beats
            notes = new List<NoteInfo>();
            notes = songInfo.notes;
            #endregion
            secPerBeat = 60f / songInfo.bpm;
            dsptimesong = (float)AudioSettings.dspTime;
        }

        // Update is called once per frame
        void Update()
        {
            CalculateSongPosition();
            CheckIfNoteCanBeSpawned();
        }

        private void CalculateSongPosition()
        {
            //calculate the position in seconds
            songPosition = (float)AudioSettings.dspTime - dsptimesong;

            //calculate the position in beats
            songPositionInBeats = songPosition / secPerBeat;
        }

        private void CheckIfNoteCanBeSpawned()
        {
            if (index < notes.Count && CurrentNote().beat < (songPositionInBeats + beatsShownInAdvance))
            {
                //Call all functions subscribed when a beat is played
                EventManager.GetInstance().NoteSpawn(CurrentNote().beat);
                index++;
            }
        }


    }
}


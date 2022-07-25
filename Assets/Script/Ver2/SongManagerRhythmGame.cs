using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManagerRhythmGame : MonoBehaviour
{
    public static SongManagerRhythmGame instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Multiple instances");
        }
    }

    [SerializeField] private SongInfo songInfo;
    public float beatsShownInAdvance;
    public AudioSource tickSound; //Remove later
    public NoteSpawner ntSpawner;

    #region Variables
    [HideInInspector]
    //Song Position
    public float songPosition;
    public float songPositionInBeats;
    public float secPerBeat;
    public float dsptimesong;

    //Beats per minute, used to measure the speed of the song
    public float bpm;
    //The point of time in the song when the note will spawn
    public List<float> notePosInBeat;
    //The index of the next note to be spawned
    public int nextIndex = 0;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        InitializeSongInfo();
        secPerBeat = 60f / songInfo.bpm;
        dsptimesong = (float)AudioSettings.dspTime;
        GetComponent<AudioSource>().clip = songInfo.aClip;
        GetComponent<AudioSource>().Play();
    }

    private void InitializeSongInfo()
    {
        bpm = songInfo.bpm;
        notePosInBeat = new List<float>();
        notePosInBeat = songInfo.notes;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateSongPosition();
        CheckIfNoteCanBeSpawned();
        //Debug.Log(songPosition.ToString() + " || " + nextIndex.ToString());
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
        if(nextIndex == 0)
        {
            nextIndex++;
        }
        if(nextIndex < notePosInBeat.Count && notePosInBeat[nextIndex] < (songPositionInBeats + beatsShownInAdvance))
        {
            //tickSound.Play();
            ntSpawner.Spawn(notePosInBeat[nextIndex]);
            //Debug.Log(nextIndex + ": " + songPositionInBeats + " = " + songPositionInBeats + " + " + beatsShownInAdvance);
            nextIndex++;
        }
    }
}

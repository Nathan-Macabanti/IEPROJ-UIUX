using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    [Header("Song Information")]
    [Tooltip("Beats per minute of a song")]
    [SerializeField] private float bpm;

    [Tooltip("Keep all the position-in-beats of notes in the song")]
    public float[] notes;

    [Tooltip("The index of the next note to be spawned")]
    public int nextIndex;

    [Header("Song Position Calculator")]
    [Tooltip("The current position of the song (in seconds)")]
    [SerializeField] float songPosition;

    [Tooltip("The current position of the song (in beats)")]
    public float songPositionInBeats;

    [Tooltip("The duration of a beat")]
    [SerializeField] float secondsPerBeat;

    [Tooltip("How much time (in seconds has passed since the song started)")]
    [SerializeField] float dspTimeOfSong;

    [SerializeField] private List<Spawner> spawners;

    // Start is called before the first frame update
    void Start()
    {
        //Calculate how many seconds is one beat
        secondsPerBeat = 60F / bpm;

        //Record the time when the song starts
        dspTimeOfSong = (float)AudioSettings.dspTime;

        //Play the song
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate the position in seconds
        songPosition = (float)(AudioSettings.dspTime - dspTimeOfSong);

        //Calculate the position in beats
        songPositionInBeats = songPosition / secondsPerBeat;

        if (nextIndex < notes.Length && notes[nextIndex] < songPositionInBeats + 3)
        {
            spawners[Random.Range(0, spawners.Count)].SpawnNote();
            nextIndex++;
        }
    }

    #region Getters
    public float BPM() { return bpm; }
    #endregion*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public static NoteManager noteManagerInstance;

    //Current position of the song (in seconds)
    float songPosition;
    //Current position of the song (in beats)
    float songPosInBeats;
    //Duration of a beat
    float secPerBeat;
    //Time (in seconds) has passed since start of song
    float dsptimesong;

    //beats per minute of a song
    float bpm;

    //List of notes
    TutorialNote[] Notes;

    //Note index
    int nextIndex = 0;

    private void Awake()
    {
        if(noteManagerInstance == null)
        {
            noteManagerInstance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        bpm = bpm / 4;
        secPerBeat = 60.0f / bpm;
        dsptimesong = (float)AudioSettings.dspTime;

        GetComponent<AudioSource>().Play();
        /*
        tutorialNotes = new List<TutorialNote>();
        for(int i = 0; i < _bankSize; i++)
        {
            GameObject noteInstance = new GameObject("note");
            noteInstance.AddComponent<TutorialNote>();
            noteInstance.transform.parent = this.transform;
            tutorialNotes.Add(noteInstance.GetComponent<TutorialNote>());
        }
        */
    }

    // Update is called once per frame
    public void Update()
    {
        songPosition = (float)(AudioSettings.dspTime - dsptimesong);

        songPosInBeats = songPosition / secPerBeat;


        /*
        for(int i = 0; i < tutorialNotes.Count; i++)
        {
            if(tutorialNotes[i] == null)
            {
                tutorialNotes[i] = note;
                return;
            }
        }
        GameObject noteInstance = new GameObject("note");
        noteInstance.AddComponent<TutorialNote>();
        noteInstance.transform.parent = this.transform;
        tutorialNotes.Add(noteInstance.GetComponent<TutorialNote>());
        */
    }
}

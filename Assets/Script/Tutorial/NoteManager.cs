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

    private float offset = 0.25f;

    //List of notes
    TutorialNote[] Notes;
    //Note index
    int nextIndex = 0;

    //Spawners
    public TutorialSpawner[] Spawners;
    public List<int> spawnerIndex;


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
        GetComponent<AudioSource>().Play();
       
    }

    // Update is called once per frame
    public void Update()
    {
        songPosition = (float)GetComponent<AudioSource>().time;
        songPosInBeats = (songPosition / secPerBeat) - offset + 1;


        for(int i = 0; i < spawnerIndex.Count; i++)
        {
            if(spawnerIndex[i] >= 0)
            {
                Spawners[spawnerIndex[i]].SpawnDodgeNote();
            }
        }
        nextIndex++;
       
    }
}

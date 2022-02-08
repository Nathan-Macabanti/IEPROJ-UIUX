using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNotesOnBeat : MonoBehaviour
{
    public NoteManager _noteManager;
    public AudioClip _tutorialBGM;
    int _randomSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(SimpleBPeerM._beatFull)
        {
           // _noteManager.SpawnNote();
        }
    }
}

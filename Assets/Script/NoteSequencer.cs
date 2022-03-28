using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNoteSequence", menuName = "ScriptableObjects/NoteSequence", order = 1)]
public class NoteSequencer : ScriptableObject
{
    public int song;
    public float bpm;
    public List<NoteInfo> Sequence;
}

[System.Serializable]
public struct NoteInfo
{
    public float beat;
    public string spawner;
}



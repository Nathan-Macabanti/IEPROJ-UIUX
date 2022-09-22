using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNoteSequence", menuName = "ScriptableObjects/NoteSequence", order = 1)]
public class NoteSequencer : ScriptableObject
{
    public AudioClip aClip;
    public int song;
    public float bpm;
    public List<NoteInfo> Sequence;
}

[System.Serializable] public enum BotType { Attack = 0, Defend = 1}

[System.Serializable] public class NoteInfo
{
    public float beat;
    public List<NoteGroup> noteGroups;
}

[System.Serializable] public struct NoteGroup
{
    public string spawner;
    public BotType botType;
}



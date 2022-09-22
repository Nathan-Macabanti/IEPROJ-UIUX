using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewRhythmSystem
{
    [CreateAssetMenu(fileName = "SongInfo", menuName = "ScriptableObjects/New/SongInfo", order = 1)]
    public class SongInfo : ScriptableObject
    {
        //The song a.k.a a disc
        public AudioClip aClip;

        //Beats per minute, used to measure the speed of the song
        public float bpm;
        
        //The point of time in the song when the note will spawn
        public List<NoteInfo> notes;
    }

    [System.Serializable]
    public class NoteInfo
    {
        public float beat;
        public Direction input;
        //[Range(0,3)]public int key = 0;
    }

    public enum Direction { up = 0, left = 1, down = 2, right = 3}
}


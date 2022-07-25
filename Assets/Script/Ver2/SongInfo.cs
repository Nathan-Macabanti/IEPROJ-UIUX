using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SongInfo", menuName = "ScriptableObjects/New/SongInfo", order = 1)]
public class SongInfo : ScriptableObject
{
    //The song a.k.a a disc
    public AudioClip aClip;

    //Beats per minute, used to measure the speed of the song
    public float bpm;

    //The point of time in the song when the note will spawn
    public List<float> notes;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    [SerializeField] private AudioClip[] SongClip;

    public AudioClip GetADisc(int index)
    {
        return SongClip[index];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    #region Singleton
    private static EventManager instance;
    public static EventManager GetInstance() { return instance; }
    private void Awake()
    {
        if (instance == null) { instance = this; }
        //else { Utils.SingletonErrorMessage(this); }
    }
    #endregion

    //This stores all methods that...
    /*...will be called every time the note is spawned, 
    it needs the beat that it will land on */
    public event UnityAction<float> onNoteSpawn;
    public event UnityAction onPlayerHit;
    //...will be called every beat
    //public event UnityAction onBeat;
    /*...will be called every time there was an attempt to hit the note, 
    it needs the rank of how well they hit*/
    //public event UnityAction<HitRank> onNoteHit;

    //These are meant to call the event, events are called in other classes 
    public void NoteSpawn(float beat) { onNoteSpawn?.Invoke(beat); }
    public void PlayerHit() { onPlayerHit?.Invoke(); }
    //public void Beat() { onBeat?.Invoke(); }
    //public void NoteHit(HitRank rank) { onNoteHit?.Invoke(rank); }

    /*When subscribing a method to an event:
     * Call the EventManager instance directly do not make a variable to reference the instance
     * Subscribe(+=) in the OnEnabled() method of Unity
     * You must also unsubscribe(-=) in the OnDisable() method or less there maybe a error or a memory leak
     * ex. 
     * private void OnEnable() => EventManager.GetInstance().onNoteSpawn += exampleMethod(float sampleVariable);
     * private void OnDisable() => EventManager.GetInstance().onNoteSpawn -= exampleMethod(float sampleVariable)
     */
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMovement : MonoBehaviour
{
    public Transform source;
    public Transform destination;
    public float beat;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float songPosInBeats = SongManagerRhythmGame.instance.songPositionInBeats;
        float beatsShownInAdvance = SongManagerRhythmGame.instance.beatsShownInAdvance;
        float timeToDestination = (beat - songPosInBeats);
        float distance = (beatsShownInAdvance - timeToDestination) / beatsShownInAdvance;

        transform.position = Vector2.Lerp(
                source.position,
                destination.position,
                distance
            );
        //Debug.Log(distance);
#if true
        if(Vector3.Distance(destination.position, this.transform.position) <= 0.0f)
        {
            Destroy(this.gameObject);
        }
#endif
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmNote : Note
{
    public override void Move()
    {
#if true
        float songPosInBeats = SongManager2.GetInstance().SongPositionInBeats;
        float beatsShownInAdvance = SongManager2.GetInstance().BeatsShownInAdvance;
        float timeToDestination = (beat - songPosInBeats);
        float distance = (beatsShownInAdvance - timeToDestination) / beatsShownInAdvance;

        transform.position = Vector3.Lerp(SpawnPosition, DestroyPosition, distance);
#endif
        if (distance > 0.976f)
            gameObject.SetActive(false);

        base.Move();
    }
}

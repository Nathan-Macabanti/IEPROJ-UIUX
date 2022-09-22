using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewRhythmSystem
{
    public class HitRankDisplay : MonoBehaviour
    {
        //Subscribe to NoteHit event
        private void OnEnable() => EventManager.GetInstance().onNoteHit += DisplayHitRank;

        private void OnDisable() => EventManager.GetInstance().onNoteHit -= DisplayHitRank;

        //When note hits or misses then display rank
        public void DisplayHitRank(HitRank rank)
        {
            switch (rank)
            {
                case HitRank.PERFECT: Debug.Log("PERFECT"); break;
                case HitRank.GOOD: Debug.Log("GOOD"); break;
                case HitRank.BAD: Debug.Log("BAD"); break;
                case HitRank.MISS: Debug.Log("MISS"); break;
            }
        }
    }
}


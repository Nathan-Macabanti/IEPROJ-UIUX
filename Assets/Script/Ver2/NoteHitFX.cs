using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewRhythmSystem
{
    public class NoteHitFX : MonoBehaviour
    {
        private void OnEnable() => EventManager.GetInstance().onNoteHit += SpawnParticles;

        private void OnDisable() => EventManager.GetInstance().onNoteHit -= SpawnParticles;

        void SpawnParticles(HitRank rank)
        {
            if(rank == HitRank.PERFECT)
            {
                GameObject particles = AssetManager.GetInstance().PerfectParticles.gameObject;
                GameObject obj = (GameObject)Instantiate(particles, this.transform.position, Quaternion.identity);
                Destroy(obj, 2.0f);
            }
        }
    }
}


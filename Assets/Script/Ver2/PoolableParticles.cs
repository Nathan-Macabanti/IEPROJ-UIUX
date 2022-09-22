using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewRhythmSystem
{
    public class PoolableParticles : MonoBehaviour, IPoolable
    {
        bool particleAnimIsPlaying;
        public void InitializePoolable()
        {
            if (!particleAnimIsPlaying)
            {
                StartCoroutine(ParticleBehaviour());
            }
        }

        public void UnregisterPoolable()
        {
            this.gameObject.SetActive(false);
        }

        IEnumerator ParticleBehaviour()
        {
            particleAnimIsPlaying = true;
            float seconds = GetComponent<ParticleSystem>().main.duration;
            yield return new WaitForSeconds(seconds);
            UnregisterPoolable();
            particleAnimIsPlaying = false;
        } 
    }
}


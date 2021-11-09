using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioVisualizer))]
public class ParamCube : MonoBehaviour
{
    public int _band = 0;
    public float _startScale, _scaleMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localScale = new Vector3(transform.localScale.x, 
            (GetComponent<AudioVisualizer>()._freqBand[_band] * _scaleMultiplier) + _startScale, 
            transform.localScale.z);
    }
}

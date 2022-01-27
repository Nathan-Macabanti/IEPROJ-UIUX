//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class BeatTracker : MonoBehaviour
{
    private static BeatTracker _BeatTrackerInstance;

    //[SerializeField] private GameObject Pulse;
    [SerializeField] private SongManager2 song;
    private float _beatInterval, _beatTimer;
    public static bool _beatFull;
    public static int _beatCountFull;

    private void Awake()
    {
        if(_BeatTrackerInstance != null && _BeatTrackerInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _BeatTrackerInstance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BeatDetection();
    }

    void BeatDetection()
    {
        _beatFull = false;
        _beatInterval = song.SecondsPerBeat;
        _beatTimer += Time.deltaTime;
        if(_beatTimer >= _beatInterval)
        {
            _beatTimer -= _beatInterval;
            _beatFull = true;
            _beatCountFull++;
        }
    }
}

//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Unity.Jobs;

//using System.IO;
using System.Linq;
using System;
using System.IO;

[RequireComponent(typeof(AudioSource))]
public class SongManager2 : MonoBehaviour
{
    public static SongManager2 songManager2Instance;

    [Header("Text File")]
    [SerializeField] private string ChartFile;

    [Header("Debugging")]
    [SerializeField] private bool isDebugging;
    [Header("UI stuff")]
    [SerializeField] private Text countDown;
    [SerializeField] private int nCountDown = 3;
    [SerializeField] private Slider songSlider;

    [Header("BPM calculator")]
    [SerializeField] private float offset = 0.25f;
    [SerializeField] private float bpm;
    [SerializeField] private List<float> notes;
    [SerializeField] private List<string> spawnerIndexStr;
    [SerializeField] private int nextIndex = 0;

    [Header("Song Position from seconds to beats calculator")]
    [SerializeField] private float songPosition;
    [SerializeField] private float songPositionInBeats;
    [SerializeField] private float secondsPerBeat;
    //[SerializeField] private float dspTimeSong;

    [Header("Spawner Manager")]
    [SerializeField] private Spawner[] spawners;
    public List<int> spawnerIndex;
    //[SerializeField] private Note _note;
    [SerializeField] private float beatsShownInAdvance;

    int playedOnce;

    private void Awake()
    {
        if (songManager2Instance == null)
        {
            songManager2Instance = this;
            //DontDestroyOnLoad(this);
        }
        else if (songManager2Instance != null)
        {
            Destroy(this);
        }
        //string Path = ChartFile + ".txt";
        string Path = Application.dataPath + "/" + ChartFile + ".txt";

        //Read From The File
        if (File.Exists(Path))
        {
            StreamReader reader = new StreamReader(Path);
            bpm = float.Parse(reader.ReadLine().ToString());
            int index = int.Parse(reader.ReadLine().ToString());
            notes.Clear();
            for (int i = 0; i < index; i++)
            {
                notes.Add(float.Parse(reader.ReadLine().ToString()));
            }
            spawnerIndexStr.Clear();
            for (int i = 0; i < index; i++)
            {
                spawnerIndexStr.Add(reader.ReadLine().ToString());
            }

            reader.Close();
            //offset += nCountDown;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        bpm = bpm / 4;
        //_note.gameObject.SetActive(false);
        secondsPerBeat = 60F / bpm;
        //dspTimeSong = (float)AudioSettings.dspTime;
        nextIndex = 0;
        countDown.text = nCountDown.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (nCountDown == -1)
        {
            countDown.text = " ";
            if (playedOnce != 1)
            {
                GetComponent<AudioSource>().Play();
                playedOnce = 1;
            }
            songPosition = (float)GetComponent<AudioSource>().time;
            songPositionInBeats = (songPosition / secondsPerBeat) - offset + 1;
            if (GetComponent<AudioSource>().isPlaying)
                songSlider.value = songPosition / GetComponent<AudioSource>().clip.length;
            //Debug.Log(songPositionInBeats);
            //Debug.Log(nextIndex);
            if (nextIndex < notes.Count && GetComponent<AudioSource>().isPlaying)
            {
                beatsShownInAdvance = 0.25f;
                if (notes[nextIndex] < songPositionInBeats + beatsShownInAdvance &&
                    spawnerIndexStr[nextIndex] != "")
                {
                    spawnerIndex.Clear();
                    spawnerIndex = spawnerIndexStr[nextIndex].Split(',').Select(Int32.Parse).ToList();

                    for (int i = 0; i < spawnerIndex.Count; i++)
                    {
                        //Debug.Log(spawners[spawnerIndex[i]]);
                        if (spawnerIndex[i] >= 0)
                            spawners[spawnerIndex[i]].SpawnNote();
                    }
                    nextIndex++;
                }
            }
            else if (!GetComponent<AudioSource>().isPlaying && nextIndex >= notes.Count)
            {
                countDown.text = "Level Done";
            }
        }
        else if (nCountDown != -1)
        {
            CountDown();
        }
    }

    [Header("CountDown till start")]
    [SerializeField] private float beatTimer = 0;
    #region Functions
    public void CountDown()
    {
        //bool beatFull;
        float beatInterval;
        //full beat count
        //beatFull = false;
        beatInterval = secondsPerBeat / 2;
        beatTimer += Time.deltaTime;
        if (beatTimer >= beatInterval)
        {
            beatTimer -= beatInterval;
            //beatFull = true;
            nCountDown -= 1;
            if (nCountDown > 0)
                countDown.text = nCountDown.ToString();
            else if (nCountDown == 0)
                countDown.text = "GO";
            else
                countDown.text = " ";
        }
    }

    public void LoadChart()
    {
        
    }
    public void ChangeAudioTime()
    {
        if (!GetComponent<AudioSource>().isPlaying && GetComponent<AudioSource>().clip.length != 0
            && isDebugging)
        {
            GetComponent<AudioSource>().time = GetComponent<AudioSource>().clip.length * songSlider.value;
        }
    }

    public void PauseMusic()
    {
        GetComponent<AudioSource>().Pause();
    }
    public void PlayMusic()
    {
        GetComponent<AudioSource>().Pause();
    }
    #endregion

    #region Getters
    public float BPM { get { return bpm; } }

    public float SecondsPerBeat { get { return secondsPerBeat; } }
    public float BeatsShownInAdvance { get { return beatsShownInAdvance; } }
    public List<float> Notes { get { return notes; } }

    public AudioSource AudioSource { get { return GetComponent<AudioSource>(); } }
    public Transform CenterSpawnerLocation { get { return spawners[1].transform; } }
    public List<string> SpawnerIndexArray { get { return spawnerIndexStr; } }
    public float SongPositionInBeats { get { return songPositionInBeats; } }
    #endregion
}

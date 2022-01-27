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

    [Header("Song")]
    [SerializeField] private Radio radio;

    [Header("Text File")]
    [SerializeField] private string ChartFile;

    [Header("Debugging")]
    [SerializeField] private bool isDebugging;

    [Header("UI stuff")]
    [SerializeField] private Text countDown;
    [SerializeField] private int nCountDown;
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
    [SerializeField] private NoteBlockade noteBlockade;
    [SerializeField] private Spawner[] spawners;
    //[SerializeField] protected bool isSpawnAttack;
    public List<int> spawnerIndex;
    //[SerializeField] private Note _note;
    [SerializeField] private float beatsShownInAdvance;

    [Header("Warning Diamonds")]
    [SerializeField] private GameObject[] warningDiamonds;
    [SerializeField] private List<int> spawnerNextIndex;

    [Header("Win Conditions")]
    //[SerializeField] private int index;
    [SerializeField] private PlayerCollision player;
    [SerializeField] private EnemyHealth enemy;
    [SerializeField] private BreakSplashScreen breakSplashScreen;
    //[SerializeField] private float[] enemiesHP;
    //[SerializeField] private string[] chartList;

    private int playedOnce;
    private int stopedOnce;

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
            this.GetComponent<AudioSource>().clip = radio.GetADisc(int.Parse(reader.ReadLine()));
            Debug.Log(this.GetComponent<AudioSource>().clip.name);
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
        else
        {
            Debug.LogError("File does not exist");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        bpm = bpm / 4;
        nCountDown = 3;
        //_note.gameObject.SetActive(false);
        secondsPerBeat = 60F / bpm;
        //dspTimeSong = (float)AudioSettings.dspTime;
        playedOnce = 0;
        nextIndex = 0;
        stopedOnce = 0;
        countDown.text = nCountDown.ToString();
        breakSplashScreen.Disappear();
        breakSplashScreen.GetWinCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        WarningPing();

        if (nCountDown == -1 && enemy.GetfHP > 0)
        {
            countDown.text = " ";
            if (playedOnce != 1)
            {
                GetComponent<AudioSource>().Play();
                playedOnce = 1;
            }
            songPosition = (float)GetComponent<AudioSource>().time;
            songPositionInBeats = (songPosition / secondsPerBeat) - offset + 1;
            //if (GetComponent<AudioSource>().isPlaying)
                songSlider.value = songPosition / GetComponent<AudioSource>().clip.length;
            //Debug.Log(songPositionInBeats);
            //Debug.Log(nextIndex);
            if (nextIndex < notes.Count && GetComponent<AudioSource>().isPlaying && !noteBlockade.GetIsAttackNote())
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
                            spawners[spawnerIndex[i]].SpawnDodgeNote();
                    }
                    nextIndex++;
                }
            }
            else if (nextIndex < notes.Count && GetComponent<AudioSource>().isPlaying && noteBlockade.GetIsAttackNote())
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
                            spawners[spawnerIndex[i]].SpawnAttackNote();
                    }
                    nextIndex++;
                }
            }
            else if (!GetComponent<AudioSource>().isPlaying && nextIndex >= notes.Count && enemy.GetfHP > 0)
            {
                nextIndex = 0;
                GetComponent<AudioSource>().Play();
                player.Heal(1);
            }
        }
        else if (enemy.GetfHP <= 0 && nCountDown == -1)
        {
            GetComponent<AudioSource>().Stop();
            Debug.Log("Destroyed all notes on map");
            spawners[0].DestroyAllNotes();
            spawners[1].DestroyAllNotes();
            spawners[2].DestroyAllNotes();
            countDown.text = "Keld 'em";
            if (stopedOnce != 1)
            {
                breakSplashScreen.GetIndex += 1;
                Debug.Log(breakSplashScreen.GetIndex);
                breakSplashScreen.AddPoints(player.GetHPPoints);
                stopedOnce = 1;
            }
            if (breakSplashScreen.GetIndex < breakSplashScreen.GetListSize)
                breakSplashScreen.Appear();
            else if (breakSplashScreen.GetIndex >= breakSplashScreen.GetListSize)
                breakSplashScreen.GetWinCanvas.gameObject.SetActive(true);
        }
        else if (enemy.GetfHP > 0 && nCountDown != -1)
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

    public void WarningPing()
    {
        /*
        Color D1 = warningDiamonds[0].GetComponent<SpriteRenderer>().material.color;
        Color D2 = warningDiamonds[1].GetComponent<SpriteRenderer>().material.color;
        Color D3 = warningDiamonds[2].GetComponent<SpriteRenderer>().material.color;

        Color Invisible = new Color(1,1,1,0);*/

        if (nextIndex < notes.Count)
        {
            spawnerNextIndex = spawnerIndexStr[nextIndex].Split(',').Select(Int32.Parse).ToList();
        }

        if(spawnerNextIndex != null)
        {
            warningDiamonds[0].SetActive(false);
            warningDiamonds[1].SetActive(false);
            warningDiamonds[2].SetActive(false);
            warningDiamonds[3].SetActive(false);
            for (int i = 0; i < spawnerNextIndex.Count; i++){
                if (spawnerNextIndex[i] >= 0)
                    warningDiamonds[spawnerNextIndex[i]].SetActive(true);
            }
        }
    }

    public void ChangeChart(string file)
    {
        //string Path = ChartFile + ".txt";
        string Path = Application.dataPath + "/" + file + ".txt";

        //Read From The File
        if (File.Exists(Path))
        {
            StreamReader reader = new StreamReader(Path);
            this.GetComponent<AudioSource>().clip = radio.GetADisc(int.Parse(reader.ReadLine()));
            Debug.Log(this.GetComponent<AudioSource>().clip.name);
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
        else
        {
            Debug.LogError("File does not exist");
        }
    }

    public void StartAgain()
    {
        bpm = bpm / 4;
        nCountDown = 3;
        //_note.gameObject.SetActive(false);
        nCountDown = 3;
        secondsPerBeat = 60F / bpm;
        //dspTimeSong = (float)AudioSettings.dspTime;
        playedOnce = 0;
        nextIndex = 0;
        stopedOnce = 0;
        countDown.text = nCountDown.ToString();
    }
#if false
    public void ChangeAudioTime()
    {
        if (!GetComponent<AudioSource>().isPlaying && GetComponent<AudioSource>().clip.length != 0
            && isDebugging)
        {
            GetComponent<AudioSource>().time = GetComponent<AudioSource>().clip.length * songSlider.value;
        }
    }
#endif

    public void PauseMusic()
    {
        GetComponent<AudioSource>().Pause();
    }
    public void StopMusic()
    {
        GetComponent<AudioSource>().Stop();
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

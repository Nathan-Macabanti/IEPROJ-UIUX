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

    [Header("CountDown till start")]
    [SerializeField] private float beatTimer = 0;

    [Header("Song")]
    [SerializeField] private Radio radio;
    [SerializeField] private NoteSequencer noteSequencer;
    //[SerializeField] private string ChartFile;

    [Header("Debugging")]
    [SerializeField] private bool isDebugging;

    [Header("UI stuff")]
    [SerializeField] private GameObject counterBG;
    [SerializeField] private Text countDown;
    [SerializeField] private int nCountDown;
    [SerializeField] private Slider songSlider;

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
    [SerializeField] private Color EnemyAttackingColor;
    [SerializeField] private Color EnemyVulnarableColor;
    [SerializeField] private GameObject[] warningDiamonds;
    [SerializeField] private List<int> spawnerNextIndex;

    [Header("Win Conditions")]
    //[SerializeField] private int index;
    [SerializeField] private PlayerCollision player;
    [SerializeField] private EnemyHealth enemy;
    [SerializeField] private BreakSplashScreen breakSplashScreen;
    [SerializeField] private Canvas VictoryCanvas;
    //[SerializeField] private float[] enemiesHP;
    //[SerializeField] private string[] chartList;

    [SerializeField] private List<float> notes;
    [SerializeField] private List<string> spawnerIndexStr;
    private float offset = 0.25f;
    private float bpm;
    private int nextIndex = 0;

    private int playedOnce;
    private int stopedOnce;
    private AudioSource audioSrc;

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
        if (songManager2Instance == null)
        {
            songManager2Instance = this;
            //DontDestroyOnLoad(this);
        }
        else if (songManager2Instance != null)
        {
            Destroy(this);
        }

        ChangeChart(noteSequencer);
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
        VictoryCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        WarningPing();

#if false
        if (nCountDown == -1 && enemy.GetfHP > 0)
        {
            RhythmGame();
        }
#endif
        if (enemy.GetfHP > 0)
        {
            RhythmGame();
        }
        else if (enemy.GetfHP <= 0 && nCountDown == -1)
        {
            CheckWinCondition();
        }
        else if (enemy.GetfHP > 0 && nCountDown != -1)
        {
            CountDown();
        }
    }

#region Functions

    private void RhythmGame()
    {
        countDown.text = " ";
        //Makesure that it is only played once
        if (playedOnce != 1)
        {
            player.IsInvincible = false;
            GetComponent<AudioSource>().Play();
            playedOnce = 1;
        }
        songPosition = (float)GetComponent<AudioSource>().time;
        songPositionInBeats = (songPosition / secondsPerBeat) - offset + 1;
        //if (GetComponent<AudioSource>().isPlaying)
        songSlider.value = songPosition / GetComponent<AudioSource>().clip.length;
        //Debug.Log(songPositionInBeats);
        //Debug.Log(nextIndex);
        ReadBeatMap();
    }

    private void ReadBeatMap()
    {
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
                    {
                        //Enemy Spawns a note
                        bool IsAttack = noteBlockade.GetIsAttackNote();
                        enemy.Spawn(spawners[spawnerIndex[i]], IsAttack, spawnerIndex[i]);
                        /*
                        if(!noteBlockade.GetIsAttackNote())
                            spawners[spawnerIndex[i]].SpawnDodgeNote();
                        else
                            spawners[spawnerIndex[i]].SpawnAttackNote();
                        */
                    }

                }
                nextIndex++;
            }
        }
        else if (!GetComponent<AudioSource>().isPlaying && nextIndex >= notes.Count && enemy.GetfHP > 0)
        {
            nextIndex = 0;
            //player.Heal(1);
            GetComponent<AudioSource>().Play();
        }
    }
    private void CheckWinCondition()
    {
        player.IsInvincible = true;
        GetComponent<AudioSource>().Stop();
        TellAllSpawnereToDestroyTheirNotes();
        //countDown.text = "Keld 'em";
        if (stopedOnce != 1)
        {
            Debug.Log("Destroyed all notes on map");
            breakSplashScreen.AddPoints(player.GetHPPoints);
            stopedOnce = 1;
            breakSplashScreen.AddIndex(1);
        }

        if (breakSplashScreen.GetListCount > breakSplashScreen.GetIndex)
        {
            breakSplashScreen.Appear();
        }
        else
        {
            VictoryCanvas.gameObject.SetActive(true);
        }
    }
    public void CountDown()
    {
        float beatInterval;
        beatInterval = secondsPerBeat / 2;
        beatTimer += Time.deltaTime;
        if (beatTimer >= beatInterval)
        {
            beatTimer -= beatInterval;
            //beatFull = true;
            nCountDown -= 1;
            if (nCountDown > 0)
            {
                counterBG.SetActive(true);
                countDown.text = nCountDown.ToString();
            }
            else if (nCountDown == 0)
            {
                counterBG.SetActive(true);
                countDown.text = "GO";
            }
            else
            {
                counterBG.SetActive(false);
                countDown.text = " ";
            }

        }
    }

    public void WarningPing()
    {
        if (nextIndex < notes.Count)
        {
            spawnerNextIndex = spawnerIndexStr[nextIndex].Split(',').Select(Int32.Parse).ToList();
        }

        if (spawnerNextIndex != null)
        {
            warningDiamonds[0].SetActive(false);
            warningDiamonds[1].SetActive(false);
            warningDiamonds[2].SetActive(false);
            warningDiamonds[3].SetActive(false);
            for (int i = 0; i < spawnerNextIndex.Count; i++) {
                if (spawnerNextIndex[i] >= 0)
                {
                    if (noteBlockade.AttackPhase)
                    {
                        warningDiamonds[spawnerNextIndex[i]].GetComponent<Renderer>().material.color = EnemyAttackingColor;
                    }
                    else
                    {
                        warningDiamonds[spawnerNextIndex[i]].GetComponent<Renderer>().material.color = EnemyVulnarableColor;
                    }
                    warningDiamonds[spawnerNextIndex[i]].SetActive(true);
                }
            }
        }
    }

    public void ChangeChart(NoteSequencer sequencer)
    {
        audioSrc.clip = radio.GetADisc(sequencer.song);
        bpm = sequencer.bpm;
        for (int i = 0; i < sequencer.Sequence.Count; i++)
        {
            NoteInfo ntInfo = sequencer.Sequence[i];
            notes.Add(ntInfo.beat);
            spawnerIndexStr.Add(ntInfo.spawner);
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

    public void TellAllSpawnereToDestroyTheirNotes()
    {
        spawners[0].DestroyAllNotes();
        spawners[1].DestroyAllNotes();
        spawners[2].DestroyAllNotes();
        spawners[3].DestroyAllNotes();
    }

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
    public Spawner LeftSpawner{ get { return spawners[0]; } }
    public Spawner MidSpawner { get { return spawners[1]; } }
    public Spawner RightSpawner { get { return spawners[2]; } }
    public Spawner JumpSpawner { get { return spawners[3]; } }
    public AudioSource AudioSource { get { return GetComponent<AudioSource>(); } }
    public Transform CenterSpawnerLocation { get { return spawners[1].transform; } }
    public List<string> SpawnerIndexArray { get { return spawnerIndexStr; } }
    public float SongPositionInBeats { get { return songPositionInBeats; } }
#endregion
}

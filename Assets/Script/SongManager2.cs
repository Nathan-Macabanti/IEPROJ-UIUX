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
    [SerializeField] private Enemy enemy;
    [SerializeField] private BreakSplashScreen breakSplashScreen;
    [SerializeField] private Canvas VictoryCanvas;
    //[SerializeField] private float[] enemiesHP;
    //[SerializeField] private string[] chartList;

    [SerializeField] private List<NoteInfo> notes;
    private float offset = 0.25f;
    private float bpm;
    private int nextIndex = 0;

    private int playedOnce;
    private int stopedOnce;
    private AudioSource audioSrc;

    private void Awake()
    {
        //Gets AudioSource
        audioSrc = GetComponent<AudioSource>();
        //Singleton
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
        //If the enemy is not dead play the game
        if (nCountDown == -1 && enemy.GetfHP > 0)
        {
            Debug.Log(nCountDown);
            RhythmGame();
        }
        // if dead and countdown is 0 check if you win
        else if (enemy.GetfHP <= 0 && nCountDown <= -1)
        {
            Debug.Log(enemy.GetfHP);
            CheckWinCondition();
        }
        //If enemy is not dead and countdown is not 0
        else if (enemy.GetfHP > 0 && nCountDown > -1)
        {
            Debug.Log("CountDown");
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
            //Player can get hit or can be collideded
            player.IsInvincible = false;
            GetComponent<AudioSource>().Play();
            //Played once so that the music will not keep repeating
            playedOnce = 1;
        }

        //song position in seconds
        songPosition = (float)GetComponent<AudioSource>().time;
        //song position in beats 4/4
        songPositionInBeats = (songPosition / secondsPerBeat) - offset + 1;
        //if (GetComponent<AudioSource>().isPlaying)
        //slider UI <maybe put this in another class>
        songSlider.value = songPosition / GetComponent<AudioSource>().clip.length;
        //Debug.Log(songPositionInBeats);
        //Debug.Log(nextIndex);
        ReadBeatMap();
    }

    private void ReadBeatMap()
    {
        //if index is not more than the list of notes and the music is playing...
        if (nextIndex < notes.Count && GetComponent<AudioSource>().isPlaying)
        {
            //beatsShownInAdvance = 0.25f;
            //If the current song position is more than the target song position and spawner is defined then spawn the note
            if (notes[nextIndex].beat < songPositionInBeats + beatsShownInAdvance &&
                notes[nextIndex].spawner != "")
            {
                //Clear the list of spawners that spawned a note
                spawnerIndex.Clear();
                //Parses the string and converts to INT
                spawnerIndex = notes[nextIndex].spawner.Split(',').Select(Int32.Parse).ToList();

                for (int i = 0; i < spawnerIndex.Count; i++)
                {
                    //Debug.Log(spawners[spawnerIndex[i]]);
                    if (spawnerIndex[i] >= 0)
                    {
                        //Enemy Spawns a note
                        bool IsAttack = noteBlockade.GetIsAttackNote();
                        enemy.Spawn(spawners[spawnerIndex[i]], notes[nextIndex].botType, spawnerIndex[i]);
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
            //Reset the index
            nextIndex = 0;
            //player.Heal(1);
            GetComponent<AudioSource>().Play();
        }
    }
    private void CheckWinCondition()
    {
        //Player cannot be hit
        player.IsInvincible = true;
        //Stop music
        GetComponent<AudioSource>().Stop();
        //Destroy all notes on the field
        TellAllSpawnereToDestroyTheirNotes();
        //countDown.text = "Keld 'em";

        //Destroys all the notes only once
        if (stopedOnce != 1)
        {
            Debug.Log("Destroyed all notes on map");
            breakSplashScreen.AddPoints(player.GetHPPoints);
            stopedOnce = 1;
            breakSplashScreen.AddIndex(1);
        }

        //Show the shop
        if (breakSplashScreen.GetListCount > breakSplashScreen.GetIndex)
        {
            breakSplashScreen.Appear();
        }
        else //Show the victory screen
        {
            VictoryCanvas.gameObject.SetActive(true);
        }
    }

    //Countdown before the song starts
    public void CountDown()
    {
        //interval of each beat in seconds
        float beatInterval;
        beatInterval = secondsPerBeat / 2;
        //timer
        beatTimer += Time.deltaTime;
        //Debug.Log(beatTimer.ToString() + beatInterval.ToString());

        //The if the seconds of the beat timer is greater or equal to the interval then reset the timer
        if (beatTimer >= beatInterval)
        {
            beatTimer -= beatInterval;
            //beatFull = true;
            //Reduce counter
            nCountDown -= 1;

            //If countdown is more than 0
            if (nCountDown > 0)
            {
                //Display current countdown number
                counterBG.SetActive(true);
                countDown.text = nCountDown.ToString();
            }
            else if (nCountDown == 0)
            {
                //Display GO instead of 0
                counterBG.SetActive(true);
                countDown.text = "GO";
            }
            else
            {
                //Just dissapear
                counterBG.SetActive(false);
                countDown.text = " ";
            }

        }
    }


    public void WarningPing()
    {
        if (nextIndex < notes.Count)
        {
            spawnerNextIndex = notes[nextIndex].spawner.Split(',').Select(Int32.Parse).ToList();
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
        //Get a new song from MusicList GameObject
        audioSrc.clip = radio.GetADisc(sequencer.song);
        //Change bpm
        bpm = sequencer.bpm;
        //Change note info
        for (int i = 0; i < sequencer.Sequence.Count; i++)
        {
            NoteInfo ntInfo = sequencer.Sequence[i];
            notes.Add(ntInfo);
            notes.Add(ntInfo);
        }
    }

    public void StartAgain()
    {
        //Change bpm
        bpm = bpm / 4;
        //Reset countdown
        nCountDown = 3;
        //_note.gameObject.SetActive(false);
        //Recalculate the seconds per beat
        secondsPerBeat = 60F / bpm;
        //dspTimeSong = (float)AudioSettings.dspTime;

        //Reset all the stoppers
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
    public List<NoteInfo> Notes { get { return notes; } }
    public Spawner LeftSpawner{ get { return spawners[0]; } }
    public Spawner MidSpawner { get { return spawners[1]; } }
    public Spawner RightSpawner { get { return spawners[2]; } }
    public Spawner JumpSpawner { get { return spawners[3]; } }
    public AudioSource AudioSource { get { return GetComponent<AudioSource>(); } }
    public Transform CenterSpawnerLocation { get { return spawners[1].transform; } }
    //public List<string> SpawnerIndexArray { get { return spawnerIndexStr; } }
    public float SongPositionInBeats { get { return songPositionInBeats; } }
#endregion
}

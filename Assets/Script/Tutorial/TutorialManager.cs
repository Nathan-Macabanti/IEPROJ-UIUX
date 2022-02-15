using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex;

    public Button lvlLoader;

    public GameObject dialogueObj;
    public GameObject dialogueManager;
    private Dialogue dialogueT;

    //[SerializeField] private GameObject GameUI;
    //[SerializeField] private GameObject TutsUI;
    [SerializeField]
    public TutorialSpawner leftSpawner;
    [SerializeField]
    public TutorialSpawner rightSpawner;
    [SerializeField]
    public TutorialSpawner midSpawner;
    [SerializeField] private GameObject GameUI;
    //jump spawner

    [SerializeField] private float _bpm;
    [SerializeField] private float _beatInterval, _beatTimer;
    [SerializeField] private static bool _beatFull;
    [SerializeField] private static int _beatCountFull;
    [SerializeField] private bool SpawningAllowed;
    [SerializeField] private PlayerController playerCtrl;
    [SerializeField] private GameObject ThisIsYou;
    [SerializeField] private NoteBlockadeTuts ntBlk;
    [SerializeField] private EnemyScript enemy;
    //[SerializeField] PlayerCollision playerCtrl;

    public float waitTime = 5f;
    private float startTime;
    private float elapsedTime;

    [SerializeField] private float movementCount = 0f;
    [SerializeField] private float moveCriteria = 5f;

    [SerializeField] private float jumpCount = 0f;
    [SerializeField] private float jumpCriteria = 5f;

    [SerializeField] private float attackCount = 0f;
    [SerializeField] private float attackCriteria = 10f;


    // Start is called before the first frame update
    void Start()
    {
        GameUI.SetActive(false);
        //TutsUI.SetActive(true);
        ThisIsYou.SetActive(false);
        dialogueT = dialogueManager.GetComponent<Dialogue>();
        SpawningAllowed = false;
    }

    // Update is called once per frame
    void Update()
    {
        _beatFull = false;
        _beatInterval = 60 / _bpm;
        _beatTimer += Time.deltaTime;

        if (_beatTimer >= _beatInterval && !SpawningAllowed)
        {
            _beatTimer -= _beatInterval;
            _beatFull = true;
            _beatCountFull++;
            Debug.Log("Full");
        }

        Debug.Log("Current PopUp no.: " + popUpIndex);

        for(int i = 0; i<popUps.Length; i++)
        {
            if(i == popUpIndex)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }

        }

        if(popUpIndex == 0)
        {
            //popUps[0].SetActive(false);
            dialogueObj.SetActive(true);
            if(dialogueT.dialogueEnded == true)
            {
                Debug.Log("Dialogue ended!");
                dialogueObj.SetActive(false);
                popUpIndex++;
            }

        }
        else if (popUpIndex == 1)
        {
            GameUI.SetActive(true);
            SpawningAllowed = false;

            Debug.Log("Starting Moving Splash tutorial");
        }
        else if (popUpIndex == 2)
        {
            ThisIsYou.SetActive(true);
            
            SpawningAllowed = true;
            BeatDetection(0, 4);
            if (ntBlk.AttackPhase)
            {
                popUpIndex++;
            }
            // Movement
            /*
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) ||
                Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                if (movementCount <= moveCriteria)
                {
                    movementCount++;
                }
                else
                {
                    popUpIndex++;
                }
            }*/
            Debug.Log("Starting Moving tutorial");
        }
        else if(popUpIndex == 3)
        {
            ThisIsYou.SetActive(false);
            SpawningAllowed = false;
            // Jumping
            leftSpawner.DestroyAllNotes();
            midSpawner.DestroyAllNotes();
            rightSpawner.DestroyAllNotes();
            Debug.Log("Starting Attack tutorial");
            ntBlk.AlwaysVulnerableSwitch(true);
        }
        else if (popUpIndex == 4)
        {
            ntBlk.TurnOffAllAlways();
            SpawningAllowed = true;
            BeatDetection(7, 15);
            
            // Jumping
            if(enemy.GetfHP <= 0)
            {
                popUpIndex++;
            }

            Debug.Log("Starting Attack tutorial");
        }
        else if (popUpIndex == 5)
        {
            SpawningAllowed = false;

            leftSpawner.DestroyAllNotes();
            midSpawner.DestroyAllNotes();
            rightSpawner.DestroyAllNotes();

            lvlLoader.onClick.Invoke();
            popUpIndex++;
            //StartCoroutine(TransitiontoFirstLevel(waitTime));
            //popUpIndex++;
        }

       /*IEnumerator TransitiontoFirstLevel(float t)
        {
            yield return new WaitForSeconds(t);
            SceneManager.LoadScene("Level1");
        }*/
    }

   void BeatDetection(int first, int last)
    {
        
        if (_beatTimer >= _beatInterval)
        {
            int SpawnIndex = Random.Range(first, last);
            _beatTimer -= _beatInterval;
            _beatFull = true;
            _beatCountFull++;
            Debug.Log("Full");
            if(SpawnIndex <= 1)
            {
                SpawnMid();
            }
            if (SpawningAllowed)
            {
                switch (SpawnIndex)
                {
                    case 0: SpawnLeft(); break;
                    case 1: SpawnRight(); break;
                    case 2: SpawnJump(); break;
                    case 3: SpawnAllDodge(); break;
                    case 4: SpawnAttackLeft(); break;
                    case 5: SpawnAttackMid(); break;
                    case 6: SpawnAttackRight(); break;
                    case 7: enemy.Spawn(leftSpawner, ntBlk.AttackPhase, 0); break;
                    case 8: enemy.Spawn(midSpawner, ntBlk.AttackPhase, 1); break;
                    case 9: enemy.Spawn(rightSpawner, ntBlk.AttackPhase, 2); break;
                    case 10: enemy.Spawn(midSpawner, ntBlk.AttackPhase, 3); break;
                    case 11: 
                        enemy.Spawn(leftSpawner, ntBlk.AttackPhase, 0);
                        enemy.Spawn(midSpawner, ntBlk.AttackPhase, 1);
                        break;
                    case 12:
                        enemy.Spawn(leftSpawner, ntBlk.AttackPhase, 0);
                        enemy.Spawn(rightSpawner, ntBlk.AttackPhase, 2);
                        break;
                    case 13:
                        enemy.Spawn(midSpawner, ntBlk.AttackPhase, 1);
                        enemy.Spawn(rightSpawner, ntBlk.AttackPhase, 2);
                        break;
                    case 14:
                        enemy.Spawn(leftSpawner, ntBlk.AttackPhase, 0);
                        enemy.Spawn(midSpawner, ntBlk.AttackPhase, 1);
                        enemy.Spawn(rightSpawner, ntBlk.AttackPhase, 2);
                        break;
                    default: Debug.Log("Out of index"); break;
                }
            }
        }

    }

    void SpawnLeft()
    {
        leftSpawner.SpawnDodgeNote();
    }

    void SpawnMid()
    {
        midSpawner.SpawnDodgeNote();
    }

    void SpawnRight()
    {
        rightSpawner.SpawnDodgeNote();
    }

    void SpawnJump()
    {
        midSpawner.SpawnJumpNote();
    }

    void SpawnAttackLeft()
    {
        leftSpawner.SpawnAttackNote();
    }

    void SpawnAttackMid()
    {
        midSpawner.SpawnAttackNote();
    }

    void SpawnAllDodge()
    {
        leftSpawner.SpawnDodgeNote();
        midSpawner.SpawnDodgeNote();
        rightSpawner.SpawnDodgeNote();
    }
    void SpawnAttackRight()
    {
        rightSpawner.SpawnAttackNote();
    }

    public void IncreaseIndex()
    {
        popUpIndex++;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

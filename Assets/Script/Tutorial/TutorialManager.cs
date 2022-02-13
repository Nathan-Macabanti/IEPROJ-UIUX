using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex;

    public GameObject dialogueObj;
    public GameObject dialogueManager;
    private Dialogue dialogueT;

    [SerializeField]
    public TutorialSpawner leftSpawner;
    [SerializeField]
    public TutorialSpawner rightSpawner;
    [SerializeField]
    public TutorialSpawner midSpawner;

    //jump spawner

    [SerializeField] private float _bpm;
    [SerializeField] private float _beatInterval, _beatTimer;
    [SerializeField] private static bool _beatFull;
    [SerializeField] private static int _beatCountFull;
    [SerializeField] private bool SpawningAllowed;
    [SerializeField] private PlayerController playerCtrl;
    [SerializeField] private GameObject ThisIsYou;
    //[SerializeField] PlayerCollision playerCtrl;

    public float waitTime = 5f;
    private float startTime;
    private float elapsedTime;

    private float movementCount = 0f;
    private float moveCriteria = 5f;

    private float jumpCount = 0f;
    private float jumpCriteria = 5f;

    private float attackCount = 0f;
    private float attackCriteria = 10f;


    // Start is called before the first frame update
    void Start()
    {
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
            ThisIsYou.SetActive(true);

            SpawningAllowed = false;

            // Movement
            if (Input.anyKeyDown)
            {
                popUpIndex++;
            }
            Debug.Log("Starting Moving tutorial");
        }
        else if (popUpIndex == 2)
        {
            ThisIsYou.SetActive(true);
            
            SpawningAllowed = true;
            BeatDetection(0,2);
                    
            // Movement
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                if (movementCount <= moveCriteria)
                {
                    movementCount++;
                }
                else
                {
                    popUpIndex++;
                }
            }
            Debug.Log("Starting Moving tutorial");
        }
        else if (popUpIndex == 3)
        {
            ThisIsYou.SetActive(true);

            SpawningAllowed = false;
            BeatDetection(0, 2);

            // 
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                popUpIndex++;
            }
            Debug.Log("Starting Moving tutorial");
        }
        else if (popUpIndex == 4)
        {
            ThisIsYou.SetActive(false);
            SpawningAllowed = true;
            BeatDetection(2,4);
            // Jumping
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                if (jumpCount <= jumpCriteria)
                {
                    jumpCount++;
                }
                else
                {
                    popUpIndex++;
                }

            }
            Debug.Log("Starting jumping tutorial");
        }
        else if (popUpIndex == 5)
        {
            ThisIsYou.SetActive(true);

            SpawningAllowed = false;
            BeatDetection(0, 2);

            // 
            if (Input.anyKeyDown)
            {
                popUpIndex++;
            }
            Debug.Log("Starting Moving tutorial");
        }
        else if(popUpIndex == 6)
        {
            SpawningAllowed = true;
            BeatDetection(4, 7);
            // Jumping
            
            if(playerCtrl.CollectedAttackNotes >= attackCriteria)
            {
                popUpIndex++;
            }
            Debug.Log("Starting jumping tutorial");
        }
        else if (popUpIndex == 7)
        {

            StartCoroutine(TransitiontoFirstLevel(waitTime));
            popUpIndex++;
        }


       IEnumerator TransitiontoFirstLevel(float t)
        {
            yield return new WaitForSeconds(t);
            SceneManager.LoadScene("Level1");
        }
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
}

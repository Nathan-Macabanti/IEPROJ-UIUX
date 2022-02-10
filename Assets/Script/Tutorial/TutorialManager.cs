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

    public float waitTime = 5f;
    private float startTime;
    private float elapsedTime;

    private float movementCount = 0f;
    private float moveCriteria = 10f;

    private float jumpCount = 0f;
    private float jumpCriteria = 10f;


    // Start is called before the first frame update
    void Start()
    {
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
            
            SpawningAllowed = true;
            BeatDetection(0,3);
                    
            // Movement
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                if (movementCount <= moveCriteria)
                {
                    movementCount++;
                }
                else
                {
                    leftSpawner.DestroyAllNotes();
                    rightSpawner.DestroyAllNotes();
                    midSpawner.DestroyAllNotes();
                    popUpIndex++;
                }
            }
            Debug.Log("Starting Moving tutorial");
        }
        else if (popUpIndex == 2)
        {
            
            SpawningAllowed = true;
            BeatDetection(3,4);
            // Jumping
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                if (jumpCount <= jumpCriteria)
                {
                    jumpCount++;
                }
                else
                {
                midSpawner.DestroyAllNotes();
                popUpIndex++;
                }

            }
            Debug.Log("Starting jumping tutorial");
        }
        else if (popUpIndex == 3)
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
            if (SpawningAllowed)
            {
                switch (SpawnIndex)
                {
                    case 0: SpawnLeft(); break;
                    case 1: SpawnMid(); break;
                    case 2: SpawnRight(); break;
                    case 3: SpawnJump(); break;
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
}

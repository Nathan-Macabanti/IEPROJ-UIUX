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
    }

    // Update is called once per frame
    void Update()
    {
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
                    leftSpawner.SpawnDodgeNote();
                    midSpawner.SpawnDodgeNote();
                    rightSpawner.SpawnDodgeNote();
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

            }
            else if (popUpIndex == 2)
            {

                    midSpawner.SpawnJumpNote();
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

   


}

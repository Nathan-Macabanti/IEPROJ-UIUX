using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    public GameObject[] popUps;
    private int popUpIndex;

    [SerializeField]
    public TutorialSpawner leftSpawner;
    [SerializeField]
    public TutorialSpawner rightSpawner;
    [SerializeField]
    public TutorialSpawner midSpawner;


    public float waitTime = 10f;
    private float startTime;
    private float elapsedTime;

    private float movementCount = 0f;
    private float moveCriteria = 25f;

    private float jumpCount = 0f;
    private float jumpCriteria = 15f;


    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Time elapsed:  " + elapsedTime);
        elapsedTime = Time.time - startTime;
        
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

        if (popUpIndex == 0)
        {
            
                // Movement
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                {
                    if(movementCount <= moveCriteria)
                    {
                    movementCount++;
                    }
                    else
                    {
                    popUpIndex++;
                    }
                }
            
          
           
            
        }
        else if(popUpIndex == 1)
        {

            // Jumping
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
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
            
            
        }
        else if(popUpIndex == 2)
        {
            //Spawn Notes to Attack
           
                popUpIndex++;
                
            
        }

    }

    


}

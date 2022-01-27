using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    public GameObject[] popUps;
    private int popUpIndex;
    public GameObject spawner;
    public float waitTime = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

                
                    popUpIndex++;
                    
                }
            
          
           
            
        }
        else if(popUpIndex == 1)
        {
                // Jumping
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                   
                    popUpIndex++;
                }
            
        }
        else if(popUpIndex == 2)
        {
            if(waitTime <= 0)
            {
                //spawn note(s)
            }
            else
            {
                waitTime -= Time.deltaTime;
            }

        }

    }




}

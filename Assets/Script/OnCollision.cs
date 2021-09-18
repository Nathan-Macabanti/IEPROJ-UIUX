using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnCollision : MonoBehaviour
{
    [SerializeField] private Text HP_points;
    [SerializeField] private Text Score_points;
    public Transform player;
    uint HPPoints = 5;
    uint ScorePoints = 0;

    private void Awake()
    {
        HP_points.text = "LIVES: " + HPPoints.ToString();
        Score_points.text = "SCORES: " + ScorePoints.ToString();

    }

    private void Update()
    {
        if(HPPoints <= 0)
        {
            Debug.Log("Game Over");
        }

        ScorePoints += (((uint)player.position.z));
        Score_points.text = "SCORES: " + ScorePoints.ToString();
        
    }

    private void OnCollisionEnter(Collision col)
    {
        Destroy(col.gameObject);

        if(HPPoints <= 0) { HPPoints = 0; }
        else { HPPoints -= 1; }
        
        HP_points.text = "LIVES: " + HPPoints.ToString();  
      
        HPPoints += 0;
        ScorePoints -= 200;
      
    }
}

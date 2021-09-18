using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnCollision : MonoBehaviour
{
    [SerializeField] private Text HP_points;
    [SerializeField] private Text Score_points;
    int HPPoints = 5;
    int ScorePoints = 0;

    private void Awake()
    {
        HP_points.text = "LIVES: " + HPPoints.ToString();
        Score_points.text = "LIVES: " + ScorePoints.ToString();

    }

    private void Update()
    {
        if(HPPoints <= 0)
        {
            Debug.Log("You are dead");
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        Destroy(col.gameObject);
        HPPoints -= 1;
        HP_points.text = "LIVES: " + HPPoints.ToString();
    }
}

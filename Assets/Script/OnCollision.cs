using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnCollision : MonoBehaviour
{
    [SerializeField] private Text points;
    int nPoints = 5;

    private void Awake()
    {
        points.text = "LIVES: " + nPoints.ToString();
    }

    private void Update()
    {
        if(nPoints <= 0)
        {
            Debug.Log("You are dead");
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        Destroy(col.gameObject);
        nPoints -= 1;
        points.text = "LIVES: " + nPoints.ToString();
    }
}

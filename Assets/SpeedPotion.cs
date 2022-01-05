using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpeedPotion : MonoBehaviour, IResetable
{
    [SerializeField] private bool hasBeenPressed;
    [SerializeField] private Button myButton;
    [SerializeField] private Note dodge;
    [SerializeField] private Note attack;
    [SerializeField] private Note jump;
    [SerializeField] private int cost;
    [SerializeField] private BreakSplashScreen breakSplash;
    // Start is called before the first frame update
    void Start()
    {
        hasBeenPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (cost <= breakSplash.GetPoints && !hasBeenPressed)
        {
            myButton.interactable = true;
        }
        else
        {
            myButton.interactable = false;
        }
    }

    public void ChangeSpeed(float percent)
    {
        if (cost <= breakSplash.GetPoints && !hasBeenPressed)
        {
            dodge.ChangeSpeed(percent);
            attack.ChangeSpeed(percent);
            jump.ChangeSpeed(percent);
            breakSplash.AddPoints(-cost);
            hasBeenPressed = true;
            Debug.Log(dodge.name + ": " + dodge.GetSpeed);
            Debug.Log(attack.name + ": " + attack.GetSpeed);
            Debug.Log(attack.name + ": " + jump.GetSpeed);
        }
    }

    public void ResetThis()
    {
        hasBeenPressed = true;
    }
}

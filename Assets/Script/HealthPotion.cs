using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPotion : MonoBehaviour, IResetable
{
    [SerializeField] private bool hasBeenPressed;
    [SerializeField] private Button myButton;
    [SerializeField] private PlayerCollision player;
    [SerializeField] private int cost;
    [SerializeField] private BreakSplashScreen breakSplash;

    private void Start()
    {
        hasBeenPressed = false;
    }

    private void Update()
    {
        if(cost <= breakSplash.GetPoints && !hasBeenPressed)
        {
            myButton.interactable = true;
        }
        else
        {
            myButton.interactable = false;
        }
    }

    public void Heal(int i)
    {
        if(cost <= breakSplash.GetPoints && !hasBeenPressed)
        {
            player.AddHealth(i);
            breakSplash.AddPoints(-cost);
            hasBeenPressed = true;
            Debug.Log(player.name + "Healed" + player.GetHPPoints);
        }
    }

    public void ResetThis()
    {
        hasBeenPressed = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreakSplashScreen : MonoBehaviour
{
    [Header("UI Stuff")]
    [SerializeField] private Text pointText;
<<<<<<< HEAD
    [SerializeField] private Canvas winCanvas;
=======
>>>>>>> working-branch

    [Header("Backend Stuff")]
    [SerializeField] private bool IsOn;
    //[SerializeField] private OnCollision player;
    [SerializeField] private SongManager2 song;
    [SerializeField] private EnemyHealth enemy;
    [SerializeField] private float[] enemiesHP;
    [SerializeField] private string[] chartList;
    [SerializeField] private int index;
    [SerializeField] private int points = 0;
    [SerializeField] private Button[] potions;

    private void Start()
    {
        index = 0;
        Disappear();
        //points = 0;
    }
    
    private void Update()
    {
        pointText.text = "Points: " + points.ToString();
    }

    public void DoneButton()
    {
        if (index <= chartList.Length)
        {
<<<<<<< HEAD
=======
            index++;
>>>>>>> working-branch
            enemy.UpdateHealth(enemiesHP[index]);
            song.ChangeChart(chartList[index]);
            song.StartAgain();
            Disappear();
        }
        else
        {
            return;
        }
    }

    public void Disappear()
    {
        gameObject.SetActive(false);
        IsOn = false;
    }

    public void Appear()
    {
        gameObject.SetActive(true);
        IsOn = true;
    }

    public void AddPoints(int p)
    {
        points += p;
    }

    public void ResetTheButton()
    {
        potions[0].GetComponent<HealthPotion>().ResetThis();
        potions[1].GetComponent<HealthPotion>().ResetThis();
    }

<<<<<<< HEAD
    public Canvas GetWinCanvas { get { return winCanvas; } }
    public int GetIndex { 
        get { return index; }
        set { index = value; }
    }
    public int GetListSize { get { return chartList.Length; } }
=======
>>>>>>> working-branch
    public int GetPoints { get { return points; } }
    public bool GetIsON { get { return IsOn; } }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreakSplashScreen : MonoBehaviour
{
    [Header("UI Stuff")]
    [SerializeField] private Text pointText;

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

    public void AddIndex(int increment)
    {
        index += increment;
    }

    public int GetPoints { get { return points; } }
    public bool GetIsON { get { return IsOn; } }
    public int GetIndex { get { return index; } }
    public int GetListCount { get { return chartList.Length; } }
}

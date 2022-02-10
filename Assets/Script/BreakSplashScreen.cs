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
    [SerializeField] private List<EnemyInfo> enemy;
    //[SerializeField] private OnCollision player;
    [SerializeField] private SongManager2 song;
    [SerializeField] private EnemyHealth enemyHealth;
    //[SerializeField] private float[] enemiesHP;
    //[SerializeField] private string[] chartList;
    [SerializeField] private int index;
    [SerializeField] private int points = 0;
    [SerializeField] private Button[] potions;

    private void Start()
    {
        Disappear();
        //points = 0;
    }
    
    private void Update()
    {
        pointText.text = "Points: " + points.ToString();
    }

    public void DoneButton()
    {
        if (index <= enemy.Count)
        {
            ChangeSprite();
            enemyHealth.UpdateHealth(enemy[index].Health);
            song.ChangeChart(enemy[index].ChartFile);
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
        Debug.Log("Index increased" + index);
    }

    public void ChangeSprite()
    {
        //Turn every enemy sprite off
        for (int i = 0; i < enemy.Count; i++)
        {
            enemy[i].EnemySprite.gameObject.SetActive(false);
        }
        for (int i = 0; i < enemy.Count; i++)
        {
            if (i == index)
            {
                Debug.Log("Spawning" + enemy[i].EnemySprite.gameObject.name);
                enemy[i].EnemySprite.gameObject.SetActive(true);
            }
                
        }
    }

    public int GetPoints { get { return points; } }
    public bool GetIsON { get { return IsOn; } }
    public int GetIndex { get { return index; } }
    public int GetListCount { get { return enemy.Count; } }
}

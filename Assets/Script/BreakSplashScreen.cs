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
            //Change Enemies Sprite
            ChangeSprite();
            //Change Health;
            enemyHealth.UpdateHealth(enemy[index].Health);
            //Change chart
            song.ChangeChart(enemy[index].ChartFile);
            
            //Change notes of Left Spawner
            song.LeftSpawner.attackNoteCopy = enemy[index].AttackNote;
            song.LeftSpawner.dodgeNoteCopy = enemy[index].DodgeNote;
            //Change notes of Mid Spawner
            song.MidSpawner.attackNoteCopy = enemy[index].AttackNote;
            song.MidSpawner.dodgeNoteCopy = enemy[index].DodgeNote;
            //Change notes of Right Spawner
            song.RightSpawner.attackNoteCopy = enemy[index].AttackNote;
            song.RightSpawner.dodgeNoteCopy = enemy[index].DodgeNote;
            //Change notes of Jump Spawner
            song.JumpSpawner.attackNoteCopy = enemy[index].JumpNote;
            song.JumpSpawner.dodgeNoteCopy = enemy[index].JumpNote;

            enemyHealth.enemyAnimator = enemy[index].EnemyAnimator;
            enemyHealth.LeftAtk = enemy[index].LAttack;
            enemyHealth.MidAtk = enemy[index].MAttack;
            enemyHealth.RightAtk = enemy[index].RAttack;
            enemyHealth.JumpAtk = enemy[index].JAttack;
            //Reset the song and chart nextIndex to 0
            song.StartAgain();

            //Get rid of splash screen
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
            Debug.LogWarning("Active off: " + enemy[i].EnemySprite.gameObject.name);
        }
        for (int i = 0; i < enemy.Count; i++)
        {
            if (i == index)
            {
                enemy[i].EnemySprite.gameObject.SetActive(true);
                Debug.LogWarning("Spawning: " + enemy[i].EnemySprite.gameObject.name);
            }
                
        }
    }

    public int GetPoints { get { return points; } }
    public bool GetIsON { get { return IsOn; } }
    public int GetIndex { get { return index; } }
    public int GetListCount { get { return enemy.Count; } }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnCollision : MonoBehaviour
{
    [SerializeField] private SongManager2 song;
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private float lengthOfInvincibility;
    [SerializeField] private float invincibilityTicks;
    [SerializeField] private bool isHurt;
    [SerializeField] private bool isInvincible;
    [SerializeField] private bool isDebugging;
    [SerializeField] private Text HP_points;
    [SerializeField] private Text Score_points;
    [SerializeField] private float damageToEnemyValue = 1;
    public Transform player;
    [SerializeField] private GameObject playerSprite;
    [SerializeField] private int HPPoints = 3;
    private int ScorePoints = 0;

    private void Awake()
    {
        if (isDebugging)
        {
            HPPoints = 9999;
        }
        HP_points.text = "LIVES: " + HPPoints.ToString();
        Score_points.text = "SCORES: " + ScorePoints.ToString();
        isInvincible = false;
        isHurt = false;
    }

    private void Update()
    {
#if true
        UpdatePlane(0, 2);
        UpdatePlane(2, 1);

#endif

        if (HPPoints <= 0)
        {
            Debug.Log("Game Over");
        }
        if (isInvincible)
        {
            BecomeInvicible();
        }
        HP_points.text = "LIVES: " + HPPoints.ToString();
        //ScorePoints += (((uint)player.position.z));
        //Score_points.text = "SCORES: " + ScorePoints.ToString();

    }

    public void BecomeInvicible()
    {
        playerSprite.GetComponent<SpriteRenderer>().material.color = new Color(1F, 1F ,1F ,0.5f);
        invincibilityTicks += Time.deltaTime * song.BPM / 4;
        if (invincibilityTicks >= lengthOfInvincibility)
        {
            playerSprite.GetComponent<SpriteRenderer>().material.color = new Color(1F, 1F, 1F, 1F);
            isInvincible = false;
            isHurt = false;
        }
    }

    public void UpdateScore(int addedPoints)
    {
        ScorePoints += addedPoints;
        Score_points.text = "SCORES: " + ScorePoints.ToString();
    }

    private void OnTriggerEnter(Collider col)
    {
        bool isAttackNote = col.GetComponent<Note>().GetIsAttackNote;
        if (!isInvincible && !isAttackNote /*&& col.gameObject.tag == "Note"*/)
        {
            if (!isHurt)
            {
                isHurt = true;
                invincibilityTicks = 0;
            }

            isInvincible = true;
            Destroy(col.gameObject);

            if (HPPoints <= 0) { HPPoints = 0; }
            else 
            { 
                HPPoints -= 1;
            }

            //HP_points.text = "LIVES: " + HPPoints.ToString();

            if (ScorePoints >= 0)
            {
                ScorePoints = 0;
            }
            else
            {
                ScorePoints -= 200;
            }
        }
        else if (isAttackNote)
        {
            enemyHealth.DamageEnemy(damageToEnemyValue);
            Destroy(col.gameObject);
        }
    }

    public int GetHPPoints { get { return HPPoints; } }

    public void AddHealth(int Health)
    {
        HPPoints = Health;
    }

    public void UpdatePlane(int index, int req)
    {
        if (HPPoints <= req)
        {
            GameObject plane = GetComponent<Trigger>().GetPlanes[index].gameObject;
            plane.GetComponent<Renderer>().material.color = Color.grey;
            //GetComponent<Trigger>().GetPlanes[2].gameObject.SetActive(false);
        }
        else
        {
            GameObject plane = GetComponent<Trigger>().GetPlanes[index].gameObject;
            plane.GetComponent<Renderer>().material.color = Color.white;
            //GetComponent<Trigger>().GetPlanes[2].gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnCollision : MonoBehaviour
{
    [SerializeField] private SongManager2 song;
    [SerializeField] private float lengthOfInvincibility;
    [SerializeField] private float invincibilityTicks;
    [SerializeField] private bool isHurt;
    [SerializeField] private bool isInvincible;
    [SerializeField] private bool isDebugging;
    [SerializeField] private Text HP_points;
    [SerializeField] private Text Score_points;
    public Transform player;
    [SerializeField] private GameObject playerSprite;
    [SerializeField] private uint HPPoints = 3;
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
        if(HPPoints <= 0)
        {
            Debug.Log("Game Over");
        }
        if (isInvincible)
        {
            BecomeInvicible();
        }

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
        if (!isInvincible)
        {
            if (!isHurt)
            {
                isHurt = true;
                invincibilityTicks = 0;
            }

            isInvincible = true;
            Destroy(col.gameObject);

            if (HPPoints <= 0) { HPPoints = 0; }
            else { 
                HPPoints -= 1;
                if (HPPoints == 2)
                    GetComponent<Trigger>().GetPlanes.RemoveAt(0);
                else if(HPPoints == 1)
                    GetComponent<Trigger>().GetPlanes.RemoveAt(2);
            }

            HP_points.text = "LIVES: " + HPPoints.ToString();

            HPPoints += 0;
            if (ScorePoints >= 0)
            {
                ScorePoints = 0;
            }
            else
            {
                ScorePoints -= 200;
            }
        }
    }

    public uint GetHPPoints { get { return HPPoints; } }
}

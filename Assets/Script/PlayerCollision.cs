using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private SongManager2 song;
    [SerializeField] private EnemyHealth enemyHealth;

    [Header("Damage")]
    [SerializeField] private float lengthOfInvincibility;
    [SerializeField] private float invincibilityTicks;
    [SerializeField] private bool isHurt;
    [SerializeField] private bool isInvincible;
    [SerializeField] private bool isDebugging;

    [Header("Health")]
    [SerializeField] private Text HP_points;
    //[SerializeField] private Text Score_points;
    [SerializeField] private Image TVStaticCanvas;
    [SerializeField] private GameObject playerSprite;
    [SerializeField] private Animator HPBarAnimator;
    [SerializeField] private int HPPoints = 3;

    [Header("Attacking")]
    [SerializeField] private float damageToEnemyValue = 1;
    [SerializeField] private int collectedAttackNotes = 0;
    [SerializeField] private int maxCollectedAttackNotes = 5;
    [SerializeField] private Text ComboText;
    [SerializeField] private Animator PlayerAnimator;
    [SerializeField] private AudioClip AtkSFX;
    [SerializeField] private AudioClip HitSFX;
    [SerializeField] private AudioSource PlayerSFX;
    
    private int ScorePoints = 0;
    public Transform player;

    private void Awake()
    {
        if (isDebugging)
        {
            HPPoints = 9999;
        }
        collectedAttackNotes = 0;
        HP_points.text = "LIVES: " + HPPoints.ToString();
        //Score_points.text = "SCORES: " + ScorePoints.ToString();
        isInvincible = false;
        isHurt = false;
        PlayerSFX = GetComponent<AudioSource>();
    }

    private void Update()
    {
#if true
        //UpdatePlane(0, 2);
        //UpdatePlane(2, 1);

#endif
        HPBarAnimator.SetInteger("Lives", HPPoints);
        //Health Update
        if (HPPoints <= 0)
        {
            Debug.Log("Game Over");
        }
        if (isInvincible)
        {
            BecomeInvicible();
        }
        if(HPPoints <= 1)
        {
            TVStaticCanvas.gameObject.SetActive(true);
        }
        else
        {
            TVStaticCanvas.gameObject.SetActive(false);
        }
        HP_points.text = "LIVES: " + HPPoints.ToString();

        //Combo Update
        if(collectedAttackNotes <= 0)
        {
            ComboText.text = " ";
        }
        else
        {
            ComboText.text = (collectedAttackNotes / 2).ToString() + "\nHITS";
        }
        //HPBarAnimator.SetBool("Healing", false);
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
        //Score_points.text = "SCORES: " + ScorePoints.ToString();
    }

    private void OnTriggerEnter(Collider col)
    {
        bool isAttackNote = col.GetComponent<Note>().GetIsAttackNote;
        if (!isInvincible && !isAttackNote)
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

            if (ScorePoints >= 0)
            {
                ScorePoints = 0;
            }
            else
            {
                ScorePoints -= 200;
            }
            PlayerSFX.clip = HitSFX;
            PlayerSFX.Play();
        }
        else if (isAttackNote)
        {
            collectedAttackNotes += 1;
            float temp = 1;
            if(collectedAttackNotes >=  maxCollectedAttackNotes)
                temp = damageToEnemyValue;
            else if(collectedAttackNotes < maxCollectedAttackNotes && collectedAttackNotes != 0)
                temp = damageToEnemyValue * ((float)collectedAttackNotes / (float)maxCollectedAttackNotes);
            enemyHealth.Damage(temp);
            PlayerAnimator.Play("VerenicaAttack");
            PlayerSFX.clip = AtkSFX;
            PlayerSFX.Play();
            Destroy(col.gameObject);
        }
    }

    public int GetHPPoints { get { return HPPoints; } }
    public bool IsInvincible {
        get { return isInvincible; }
        set { isInvincible = value; }
    }

    public void ChangeDamageValue(float newValue)
    {
        damageToEnemyValue = newValue;
    }

    public void AddHealth(int Health)
    {
        HPBarAnimator.SetBool("Healing", true);
        HPPoints = Health;
    }

    public void UpdatePlane(int index, int req)
    {
        if (HPPoints <= req)
        {
            GameObject plane = GetComponent<PlayerMovement>().GetPlanes[index].gameObject;
            plane.GetComponent<Renderer>().material.color = Color.black;
        }
        else
        {
            GameObject plane = GetComponent<PlayerMovement>().GetPlanes[index].gameObject;
            plane.GetComponent<Renderer>().material.color = Color.white;
        }
    }

    public void Heal(int health)
    {
        HPPoints += health;
        //HPBarAnimator.SetBool("Healing", true);
    }

    public int CollectedAttackNotes
    {
        get { return collectedAttackNotes; }
        set { collectedAttackNotes = value; }
    }
}

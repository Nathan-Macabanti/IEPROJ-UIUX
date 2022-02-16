using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool isInvincible;
    [SerializeField] private float lengthOfInvincibility;
    [SerializeField] private float invincibilityTicks;
    [SerializeField] private bool isHurt;

    [Header("Movement")]
    [SerializeField] private float min = -3;
    [SerializeField] private float max = 3;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float offset;
    [SerializeField] private float JUMP_INTERVAL = 0.15f;
    [SerializeField] private List<Transform> planes;

    [Header("Animations")]
    [SerializeField] private int index;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private AudioClip AtkSFX;
    [SerializeField] private AudioClip HitSFX;
    [SerializeField] private AudioSource PlayerSFX;
    [SerializeField] private int collectedAttackNotes = 0;
    [SerializeField] private Transform bloodSpawnTrans;
    [SerializeField] private GameObject bloodParticles;
    [SerializeField] private GameObject AttackNoteParticles;

    [Header("Jump")]
    [SerializeField] private AnimationClip jumpClip;
    [SerializeField] private float JumpOffset;

    [Header("Attacking")]
    [SerializeField] private EnemyScript enemy;
    [SerializeField] private float damageValue = 2;
    [SerializeField] private Text HITS;

    private bool isInAir;
    private float ticks = 0.0f;
    [SerializeField] private TutorialManager tutsMan;
    [SerializeField] private GameObject PlayerSprite;
    [SerializeField] private Quaternion InitialRotation;
    [SerializeField] private bool IsHoldingDodge;
    void Start()
    {
        min = planes[0].position.z;
        max = planes[2].position.z;
        index = 1;
        JUMP_INTERVAL = jumpClip.length - JumpOffset;
        PlayerSFX = GetComponent<AudioSource>();
        InitialRotation = new Quaternion(
            Mathf.Abs(PlayerSprite.GetComponent<Transform>().rotation.x),
            Mathf.Abs(PlayerSprite.GetComponent<Transform>().rotation.y),
            Mathf.Abs(PlayerSprite.GetComponent<Transform>().rotation.z),
            PlayerSprite.GetComponent<Transform>().rotation.w
            );
        IsHoldingDodge = false;
        isInAir = false;
    }

    // Update is called once per frame
    void Update()
    {
#if false
        if (isInvincible)
        {
            BecomeInvicible();
        }
#endif

        if(collectedAttackNotes <= 0)
        {
            HITS.text = " ";
        }
        else
        {
            HITS.text = (collectedAttackNotes / 2.0f).ToString() + "\nHITS";
        }
        Move();
    }

    void Move()
    {
        if (Time.timeScale == 1)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                PlayerSprite.GetComponent<Transform>().rotation = new Quaternion(
                    InitialRotation.x,
                    -InitialRotation.y,
                    InitialRotation.z,
                    InitialRotation.w
                    );
                /*
                if (!IsHoldingDodge)
                {
                    playerAnimator.StopPlayback();
                    playerAnimator.SetBool("Dodge", true);
                    IsHoldingDodge = true;
                }
                else
                {
                    playerAnimator.SetBool("Dodge", false);
                }*/

                if (index > 0)
                {
                    index--;
                    playerAnimator.StopPlayback();
                    playerAnimator.SetBool("Dodge", true);
                }
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                PlayerSprite.GetComponent<Transform>().rotation = new Quaternion(
                   -InitialRotation.x,
                   InitialRotation.y,
                   InitialRotation.z,
                   InitialRotation.w
                   );
                /*
                if (!IsHoldingDodge)
                {
                    playerAnimator.StopPlayback();
                    playerAnimator.SetBool("Dodge", true);
                    IsHoldingDodge = true;
                }
                else
                {
                    playerAnimator.SetBool("Dodge", false);
                }*/

                if (index < planes.Count - 1)
                {
                    index++;
                    playerAnimator.StopPlayback();
                    playerAnimator.SetBool("Dodge", true);
                }

            }
            else
            {
                //playerAnimator.StopPlayback();
                //IsHoldingDodge = false;
                playerAnimator.SetBool("Dodge", false);
                //transform.position = new Vector3(this.transform.position.x, this.transform.position.y, planes[1].position.z);
            }

            Jump();
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && !isInAir)
            {
                transform.position = transform.position + new Vector3(0, jumpHeight, 0);
                isInAir = true;
                JUMP_INTERVAL = jumpClip.length - JumpOffset;
            }
            Debug.Log("Current lane index: " + index);
            transform.position = new Vector3(this.transform.position.x, this.transform.position.y, planes[index].position.z);
        }
    }
    void Jump()
    {
        if (isInAir == true)
        {
            this.ticks += Time.deltaTime;
            playerAnimator.StopPlayback();
            playerAnimator.SetBool("Jump", true);
        }
        if (this.ticks >= JUMP_INTERVAL)
        {
            this.ticks = 0.0f;
            isInAir = false;
            transform.position = transform.position + new Vector3(0, -jumpHeight, 0);
            playerAnimator.SetBool("Jump", false);
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        //Debug.Log("Verenica Damaged");
        if (!isInvincible && !col.GetComponent<TutorialNote>().GetIsAttackNote)
        {
            if (!isHurt)
            {
                isHurt = true;
                //invincibilityTicks = 0;
            }

            GameObject dgg = Instantiate(bloodParticles, this.bloodSpawnTrans.position, this.bloodSpawnTrans.rotation);
            //isInvincible = true;
            PlayerSFX.clip = HitSFX;
            PlayerSFX.Play();
        }
        else if (col.GetComponent<TutorialNote>().GetIsAttackNote)
        {
            //Debug.Log("Verenica Attacks");
            collectedAttackNotes++;
            enemy.Damage(damageValue * collectedAttackNotes / 5.0f);
            playerAnimator.Play("VerenicaAttack");
            PlayerSFX.clip = AtkSFX;
            PlayerSFX.Play();
            GameObject atkParticle = Instantiate(AttackNoteParticles, this.transform.position, this.transform.rotation);
            Destroy(atkParticle, 3f);
            //Debug.Log(collectedAttackNotes + " " + col.GetComponent<TutorialNote>().GetIsAttackNote);
        }
        //Debug.Log("Colliding with: " + col.name);
        GameObject.Destroy(col.gameObject);

    }

    public void BecomeInvicible()
    {
        PlayerSprite.GetComponent<SpriteRenderer>().material.color = new Color(1F, 1F, 1F, 0.5f);
        invincibilityTicks += Time.deltaTime;
        if (invincibilityTicks >= lengthOfInvincibility)
        {
            PlayerSprite.GetComponent<SpriteRenderer>().material.color = new Color(1F, 1F, 1F, 1F);
            isInvincible = false;
            isHurt = false;
        }
    }
    public List<Transform> GetPlanes { get { return planes; } }

    public int CollectedAttackNotes
    {
        get { return collectedAttackNotes; }
        set { collectedAttackNotes = value; }
    }
}

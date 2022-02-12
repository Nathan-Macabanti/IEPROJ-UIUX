using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool isInvincible;
    [SerializeField] private float lengthOfInvincibility;
    [SerializeField] private float invincibilityTicks;
    [SerializeField] private bool isHurt;

    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float offset;
    [SerializeField] private float JUMP_INTERVAL = 0.15f;
    [SerializeField] private List<Transform> planes;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private AudioClip AtkSFX;
    [SerializeField] private AudioClip HitSFX;
    [SerializeField] private AudioSource PlayerSFX;
    [SerializeField] private int collectedAttackNotes = 0;

    [SerializeField] private AnimationClip jumpClip;
    [SerializeField] private float JumpOffset;

    private bool isInAir;
    private float ticks = 0.0f;
    //[SerializeField] private TutorialManager tutsMan;
    [SerializeField] private GameObject PlayerSprite;
    [SerializeField] private Quaternion InitialRotation;
    [SerializeField] private bool IsHoldingDodge;
    void Start()
    {
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
        if (isInvincible)
        {
            BecomeInvicible();
        }

        if (Time.timeScale == 1)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                PlayerSprite.GetComponent<Transform>().rotation = new Quaternion(
                    InitialRotation.x,
                    -InitialRotation.y,
                    InitialRotation.z,
                    InitialRotation.w
                    );
                if (!IsHoldingDodge)
                {
                    playerAnimator.StopPlayback();
                    playerAnimator.SetBool("Dodge", true);
                    IsHoldingDodge = true;
                }
                else
                {
                    playerAnimator.SetBool("Dodge", false);
                }

                if (transform.position.z > planes[0].position.z)
                {
                    transform.position = new Vector3(this.transform.position.x, this.transform.position.y, planes[0].position.z + offset);
                }
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                PlayerSprite.GetComponent<Transform>().rotation = new Quaternion(
                   -InitialRotation.x,
                   InitialRotation.y,
                   InitialRotation.z,
                   InitialRotation.w
                   );
                if (!IsHoldingDodge)
                {
                    playerAnimator.StopPlayback();
                    playerAnimator.SetBool("Dodge", true);
                    IsHoldingDodge = true;
                }
                else
                {
                    playerAnimator.SetBool("Dodge", false);
                }

                if (transform.position.z < planes[2].position.z)
                {
                    transform.position = new Vector3(this.transform.position.x, this.transform.position.y, planes[2].position.z - offset);
                }

            }
            else
            {
                playerAnimator.StopPlayback();
                IsHoldingDodge = false;
                playerAnimator.SetBool("Dodge", false);
                transform.position = new Vector3(this.transform.position.x, this.transform.position.y, planes[1].position.z);
            }

            Jump();
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && !isInAir)
            {
                transform.position = transform.position + new Vector3(0, jumpHeight, 0);
                isInAir = true;
            }
        }
    }

    void Jump()
    {
        if (isInAir == true)
        {
            playerAnimator.StopPlayback();
            playerAnimator.SetBool("Jump", true);
            this.ticks += Time.deltaTime;
        }
        if (this.ticks >= JUMP_INTERVAL)
        {
            playerAnimator.SetBool("Jump", false);
            this.ticks = 0.0f;
            isInAir = false;
            transform.position = transform.position + new Vector3(0, -jumpHeight, 0);
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (!isInvincible && !col.GetComponent<TutorialNote>().GetIsAttackNote)
        {
            if (!isHurt)
            {
                isHurt = true;
                invincibilityTicks = 0;
            }

            
            isInvincible = true;
            PlayerSFX.clip = HitSFX;
            PlayerSFX.Play();
        }
        else if (col.GetComponent<TutorialNote>().GetIsAttackNote)
        {
            CollectedAttackNotes++;
            playerAnimator.Play("VerenicaAttack");
            PlayerSFX.clip = AtkSFX;
            PlayerSFX.Play();
        }
        Debug.Log("Colliding with: " + col.name);
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

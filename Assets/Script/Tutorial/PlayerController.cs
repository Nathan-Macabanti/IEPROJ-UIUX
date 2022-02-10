using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float offset;
    [SerializeField] private float JUMP_INTERVAL = 0.15f;
    [SerializeField] private List<Transform> planes;
    [SerializeField] private Animator playerAnimator;

    private bool isInAir;
    private float ticks = 0.0f;
    [SerializeField] private GameObject PlayerSprite;
    [SerializeField] private Quaternion InitialRotation;
    [SerializeField] private bool IsHoldingDodge;
    void Start()
    {
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
        bool isAttackNote = col.GetComponent<Note>().GetIsAttackNote;
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
            if (collectedAttackNotes >= maxCollectedAttackNotes)
                temp = damageToEnemyValue;
            else if (collectedAttackNotes < maxCollectedAttackNotes && collectedAttackNotes != 0)
                temp = damageToEnemyValue * ((float)collectedAttackNotes / (float)maxCollectedAttackNotes);
            enemyHealth.DamageEnemy(temp);
            PlayerAnimator.Play("VerenicaAttack");
            PlayerSFX.clip = AtkSFX;
            PlayerSFX.Play();
            Destroy(col.gameObject);
        }
    }
    public List<Transform> GetPlanes { get { return planes; } }
}

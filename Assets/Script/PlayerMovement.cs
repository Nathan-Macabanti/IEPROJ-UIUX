using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
#if false
    //OnCollision col;
    [SerializeField] private float min = -3;
    [SerializeField] private float max = 3;
    [SerializeField] private float jumpHeight = 2;
    private float ticks = 0.0f;
    [SerializeField] private float JUMP_INTERVAL = 0.15f;
    private bool isInAir;

    [SerializeField] private List<Transform> planes; 
    [SerializeField] private Animator playerAnimator;

    [Header("InputsBool")]
    [SerializeField] private bool IsHoldingLeft;
    [SerializeField] private bool IsHoldingRight;
    //[SerializeField] private Animation Player;

    // Start is called before the first frame update
    void Start()
    {
        IsHoldingLeft = false;
        IsHoldingRight = false;
        isInAir = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 1)
        {
            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && GetComponent<PlayerCollision>().GetHPPoints >= 3)
            {
                if (!IsHoldingLeft)
                {
                    playerAnimator.SetBool("Left", true);
                    playerAnimator.SetBool("Right", false);
                    IsHoldingLeft = true;
                    IsHoldingRight = false;
                }
                else
                {
                    playerAnimator.SetBool("Left", false);
                    playerAnimator.SetBool("Right", false);
                }
                
                if (transform.position.z > planes[0].position.z)
                {
                    transform.position = new Vector3(this.transform.position.x, this.transform.position.y, planes[0].position.z);
                }
            }
            else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && (GetComponent<PlayerCollision>().GetHPPoints >= 3 || GetComponent<PlayerCollision>().GetHPPoints == 2))
            {
                if (!IsHoldingRight)
                {
                    playerAnimator.SetBool("Right", true);
                    playerAnimator.SetBool("Left", false);
                    IsHoldingLeft = false;
                    IsHoldingRight = true;
                }
                else
                {
                    playerAnimator.SetBool("Left", false);
                    playerAnimator.SetBool("Right", false);
                }
                if (transform.position.z < planes[2].position.z)
                {
                    transform.position = new Vector3(this.transform.position.x, this.transform.position.y ,planes[2].position.z);
                }
            }
            else
            {
                IsHoldingRight = false;
                IsHoldingLeft = false;
                playerAnimator.SetBool("Left", false);
                playerAnimator.SetBool("Right", false);
                transform.position = new Vector3(this.transform.position.x, this.transform.position.y, planes[1].position.z);
            }

            jumpAndFalling();
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && !isInAir)
            {
                transform.position = transform.position + new Vector3(0, jumpHeight, 0);
                isInAir = true;
            }
        }
    }

    void jumpAndFalling()
    {
        if (isInAir == true)
        {
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

    public List<Transform> GetPlanes { get { return planes; } }
#endif

    [Header("Jump")]
    //OnCollision col;
    [SerializeField] private float min = -3;
    [SerializeField] private float max = 3;
    [SerializeField] private float jumpHeight = 2;
    private float ticks = 0.0f;
    [SerializeField] private float JUMP_INTERVAL = 0.15f;
    private bool isInAir;

    [Header("Animations")]
    [SerializeField] private GameObject PlayerSprite;
    [SerializeField] private List<Transform> planes;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Quaternion InitialRotation;

    [Header("InputsBool")]
    [SerializeField] private bool IsHoldingDodge;
    //[SerializeField] private bool IsHoldingRight;
    //[SerializeField] private Animation Player;

    // Start is called before the first frame update
    void Start()
    {
        //(x, y)
        InitialRotation = new Quaternion (
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
            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && GetComponent<PlayerCollision>().GetHPPoints >= 3)
            {
                // (x, -y)
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
                    transform.position = new Vector3(this.transform.position.x, this.transform.position.y, planes[0].position.z);
                }
            }
            else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && (GetComponent<PlayerCollision>().GetHPPoints >= 3 || GetComponent<PlayerCollision>().GetHPPoints == 2))
            {
                //(-x, y)
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
                    transform.position = new Vector3(this.transform.position.x, this.transform.position.y, planes[2].position.z);
                }
            }
            else
            {
                playerAnimator.StopPlayback();
                IsHoldingDodge = false;
                playerAnimator.SetBool("Left", false);
                playerAnimator.SetBool("Right", false);
                transform.position = new Vector3(this.transform.position.x, this.transform.position.y, planes[1].position.z);
            }

            jumpAndFalling();
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && !isInAir)
            {
                transform.position = transform.position + new Vector3(0, jumpHeight, 0);
                isInAir = true;
            }
        }
    }

    void jumpAndFalling()
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

    public List<Transform> GetPlanes { get { return planes; } }
}

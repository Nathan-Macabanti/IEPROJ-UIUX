using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float jumpHeight = 8f;
    [SerializeField] private float JUMP_INTERVAL = 0.15f;

    [SerializeField] private List<Transform> planes;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animation Player;

    private float ticks = 0.0f;
    private bool isInAir;

    void Start()
    {
        
    }

    void Update()
    {

        if (Time.timeScale == 1)
        {
            //Left Movement
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                playerAnimator.SetBool("Left", true);

                if (transform.position.z > planes[0].position.z)
                {
                    transform.position = new Vector3(this.transform.position.x, this.transform.position.y, planes[0].position.z);
                }
            }

            //Right Movement
            else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                playerAnimator.SetBool("Right", true);

                if (transform.position.z < planes[2].position.z)
                {
                    transform.position = new Vector3(this.transform.position.x, this.transform.position.y, planes[2].position.z);
                }
            }

            else
            {
                playerAnimator.SetBool("Left", false);
                playerAnimator.SetBool("Right", false);
                transform.position = new Vector3(this.transform.position.x, this.transform.position.y, planes[1].position.z);
            }

            //Jumping
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

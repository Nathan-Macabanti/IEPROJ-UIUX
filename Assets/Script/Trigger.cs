using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    //OnCollision col;

    float min = -3;
    float max = 3;
    private float ticks = 0.0f;
    private float JUMP_INTERVAL = 0.13f;
    private bool isInAir;

    // Start is called before the first frame update
    void Start()
    {
        isInAir = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(transform.position.z > min)
                transform.position = transform.position + new Vector3(0, 0, min);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (transform.position.z < max)
                transform.position = transform.position + new Vector3(0, 0, max);
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isInAir != true)
        {
            isInAir = true;
            transform.position = transform.position + new Vector3(0, 2, 0);
        }

        jumpAndFalling();

        /*
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, min);
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, max);
        }*/
    }

    void jumpAndFalling()
    {
        if(isInAir == true)
        {
            this.ticks += Time.deltaTime;
        }

        if (this.ticks >= JUMP_INTERVAL)
        {
            this.ticks = 0.0f;
            isInAir = false;
            transform.position = transform.position + new Vector3(0, -2, 0);
        }
    }
}

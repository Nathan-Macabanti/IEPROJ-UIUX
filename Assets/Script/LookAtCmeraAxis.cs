using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCmeraAxis : MonoBehaviour
{
    Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetVector = this.transform.position - mainCamera.transform.position;
        transform.rotation = Quaternion.LookRotation(targetVector, mainCamera.transform.rotation * new Vector3(0.0f, 1.0f, 0.0f));
    }
}

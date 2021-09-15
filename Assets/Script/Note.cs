using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] protected GameObject noteObj;
    [SerializeField] private float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        RandomizeGeminiColor();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(1, 0, 0);
        noteObj.transform.Translate(pos * speed * Time.deltaTime);
    }

    public void RandomizeGeminiColor()
    {
        float r = Random.Range(0.0f, 1.0f);
        float g = Random.Range(0.0f, 1.0f);
        float b = Random.Range(0.0f, 1.0f);

        Color color = new Color(r, g, b);
        this.noteObj.GetComponent<MeshRenderer>().material.SetColor("_Color", color);
    }
}

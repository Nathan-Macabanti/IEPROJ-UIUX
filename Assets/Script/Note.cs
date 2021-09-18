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
        int r = Random.Range(0, 2);
        int g = Random.Range(0, 2);
        int b = Random.Range(0, 2);

        if (r < 2 && g < 2) { r = 0; g = 0; b = 1;}
        if (r < 2 && b < 2) { r = 0; g = 1; b = 0;}
        if (b < 2 && g < 2) { r = 1; g = 0; b = 0;}
        if (b < 2 && g < 2 && r < 2) { r = 0; g = 0; b = 1; }

        Color color = new Color(r, g, b);
        this.noteObj.GetComponent<MeshRenderer>().material.SetColor("_Color", color);
    }
}

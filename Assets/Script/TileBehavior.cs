using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehavior : MonoBehaviour
{
    [SerializeField] private Color initialColor;
    [SerializeField] private Color changeColor;
    // Start is called before the first frame update
    void Awake()
    {
        //initialColor = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = initialColor; 
    }

    public IEnumerator FadeToInitial()
    {
        for(float t = 0.0f; t < 1; t += 0.88f)
        {
            GetComponent<Renderer>().material.color = Color.Lerp(changeColor, initialColor, t / 1.0f);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Note"))
        {
            GetComponent<Renderer>().material.color = changeColor;
            //other.GetComponent<Renderer>().material.color = changeColor;
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(FadeToInitial());
    }
}

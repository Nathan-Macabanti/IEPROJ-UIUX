using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] dialogue;
    public int index;
    public float typingSpeed;
    public bool dialogueEnded = false;
  
    public GameObject continueButton;
    public GameObject dialogueBox;

    private void Start()
    {
        StartCoroutine(Type());
    }

    private void Update()
    {
        if(textDisplay.text == dialogue[index])
        {
            dialogueBox.SetActive(true);
            continueButton.SetActive(true);
        }
    }

    IEnumerator Type()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        dialogueBox.SetActive(false);
        continueButton.SetActive(false);

        if(index == dialogue.Length - 1)
        {
            dialogueEnded = true;
            this.gameObject.SetActive(false);
        }

        if(index < dialogue.Length - 1)
        {
            index++;
            textDisplay.text = "";
            dialogueBox.SetActive(true);
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            continueButton.SetActive(false);
        }
    }
}

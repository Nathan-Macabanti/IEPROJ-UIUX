using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void onPlayPress()
    {
        SceneManager.LoadScene("ChapterSelection");
    }

    public void onPlayGamePress()
    {
        SceneManager.LoadScene("Prototype");
    }

}

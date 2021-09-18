using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterSelection : MonoBehaviour
{
    public void onPlayPress()
    {
        SceneManager.LoadScene("Prototype");
    }

    public void onBackPress()
    {
        SceneManager.LoadScene("MainLobby");
    }

}

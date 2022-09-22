using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;

    public SongManager2 song;
    public AudioSource radio;
    public GameObject pauseMenuUI;

    private void Awake()
    {
        if(song != null)
            radio = song.GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Pressing Escape");
            if (GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        radio.Play();
        Time.timeScale = 1f;
        GamePaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        radio.Pause();
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void ReturnToChapterSelect()
    {
        SceneManager.LoadScene("ChapterSelection");
        Debug.Log("Returning to Chapter Selection...");
    }
}

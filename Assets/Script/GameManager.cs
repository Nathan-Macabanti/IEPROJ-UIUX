using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //[SerializeField] private List<string> chartsFiles;
    //[SerializeField] private GameObject shopPanel;
    //[SerializeField] private EnemyHealth enemy;
    [SerializeField] private SongManager2 songMan2;
    [SerializeField] private PlayerCollision player;
    [SerializeField] private Canvas LossScreen;
    [SerializeField] private string LoadThisSceneWhenGameOver;

    // Start is called before the first frame update
    private void Awake()
    {
        LossScreen.gameObject.SetActive(false);
    }

    void Start()
    {
        LossScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetHPPoints <= 0)
        {
            songMan2.StopMusic();
            songMan2.TellAllSpawnereToDestroyTheirNotes();
            LossScreen.gameObject.SetActive(true);
        }
    }

    public void SceneChange(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LossScreenCheckBox()
    {

    }
}

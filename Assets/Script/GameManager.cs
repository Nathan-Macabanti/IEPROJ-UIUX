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
    [SerializeField] private Canvas LoseScreen;
    [SerializeField] private string LoadThisSceneWhenGameOver;
    // Start is called before the first frame update
    void Start()
    {
        LoseScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetHPPoints <= 0)
        {
            songMan2.StopMusic();
            LoseScreen.gameObject.SetActive(true);
        }
    }

    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}

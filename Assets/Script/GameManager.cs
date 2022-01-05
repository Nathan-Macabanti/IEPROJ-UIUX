using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<string> chartsFiles;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private EnemyHealth enemy;
    [SerializeField] private SongManager2 songMan2;
    [SerializeField] private OnCollision player;
    [SerializeField] private string LoadThisSceneWhenGameOver;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetHPPoints <= 0)
        {
            SceneManager.LoadScene(LoadThisSceneWhenGameOver);
        }
    }
}

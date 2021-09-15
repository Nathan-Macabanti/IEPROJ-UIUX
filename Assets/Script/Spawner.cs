using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private static Spawner _spawnerInstance;
    //[SerializeField] private Transform spawnLoc;
    [SerializeField] private Note NoteCopy;
    [SerializeField] private List<Note> NoteList;
    //[SerializeField] private AudioSource music;
    public Note note;

    private float ticks = 0.0f;
    private float SPAWN_INTERVAL;

    /*
    private void Awake()
    {
        if (_spawnerInstance != null && _spawnerInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _spawnerInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }*/

    // Start is called before the first frame update
    void Start()
    {
        SPAWN_INTERVAL = Random.Range(1.0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        this.ticks += Time.deltaTime;
        if (this.ticks >= SPAWN_INTERVAL)
        {
            this.ticks = 0.0f;
            note = Instantiate(this.NoteCopy, this.transform.position, this.transform.rotation);
            NoteList.Add(note);
            SPAWN_INTERVAL = Random.Range(1.0f, 3.0f);
        }
        MaxNotes(5);
        RemoveFromList();
    }

    void MaxNotes(int maximum)
    {
        if(NoteList.Count >= maximum)
        {
            GameObject.Destroy(this.NoteList[0].gameObject);
            NoteList.RemoveAt(0);
        }
    }

    void RemoveFromList()
    {
        for(int i = 0; i < NoteList.Count; i++)
        {
            if(NoteList[i] == null)
            {
                NoteList.RemoveAt(i);
            }
        }
    }
}

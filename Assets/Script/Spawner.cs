using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private static Spawner _spawnerInstance;
    //[SerializeField] private Material mat;
    //[SerializeField] private Color initialColor;
    //[SerializeField] private Color changeColor;
    [SerializeField] private Vector3 decayFactor;
    [SerializeField] private int nMaxNotes;
    //[SerializeField] private Transform spawnLoc;
    [SerializeField] private Note NoteCopy;
    public List<Note> NoteList;
    //[SerializeField] private AudioSource music;

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
        NoteCopy.gameObject.SetActive(false);
        //mat = GetComponentInChildren<Renderer>().material;
        //initialColor = mat.color;
    }

    // Update is called once per frame
    void Update()
    {
        MaxNotes(50);
        RemoveFromList();
    }

    public void SpawnNote()
    {
        Note note;
        note = Instantiate(this.NoteCopy, this.transform.position, this.transform.rotation, this.transform);
        note.NoteObj().SetActive(true);
        NoteList.Add(note);

        //mat.color = changeColor;
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

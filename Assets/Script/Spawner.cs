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
    [SerializeField] private Note DodgeNoteCopy;
    [SerializeField] private Note AttackNoteCopy;
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
        DodgeNoteCopy.gameObject.SetActive(false);
        AttackNoteCopy.gameObject.SetActive(false);
        //mat = GetComponentInChildren<Renderer>().material;
        //initialColor = mat.color;
    }

    // Update is called once per frame
    void Update()
    {
        MaxNotes(50);
        RemoveFromList();
    }

    public void SpawnDodgeNote()
    {
        Note note;
        note = Instantiate(this.DodgeNoteCopy, this.transform.position, this.transform.rotation, this.transform);
        note.NoteObj().SetActive(true);
        NoteList.Add(note);

        //mat.color = changeColor;
    }

    public void SpawnAttackNote()
    {
        Note note;
        note = Instantiate(this.AttackNoteCopy, this.transform.position, this.transform.rotation, this.transform);
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

    public void DestroyAllNotes()
    {
        for (int i = 0; i < NoteList.Count; i++)
        {
            if(NoteList[i] != null)
            {
                Debug.Log("Destroying" + NoteList[i].name);
                GameObject.Destroy(NoteList[i].gameObject);
                NoteList.RemoveAt(i);
            }

        }
    }

    public bool CheckIfThereAreAttackNotes()
    {
        for (int i = 0; i < NoteList.Count; i++)
        {
            if (NoteList[i].GetIsAttackNote)
            {
                return true;
            }
        }
        return false;
    }

    public Note dodgeNoteCopy
    {
        //get { return DodgeNoteCopy; }
        set { DodgeNoteCopy = value; }
    }

    public Note attackNoteCopy
    {
        //get { return AttackNoteCopy; }
        set { AttackNoteCopy = value; }
    }
}

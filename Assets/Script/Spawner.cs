using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewRhythmSystem;

public class Spawner : MonoBehaviour
{
    private static Spawner _spawnerInstance;
    //[SerializeField] private Material mat;
    //[SerializeField] private Color initialColor;
    //[SerializeField] private Color changeColor;
    [SerializeField] private Vector3 decayFactor;
    [SerializeField] private int nMaxNotes;
    //[SerializeField] private Transform spawnLoc;
    [SerializeField] private string DodgeNoteTag;
    [SerializeField] private string AttackNoteTag;
    //[SerializeField] private Note DodgeNoteCopy;
    //[SerializeField] private Note AttackNoteCopy;
    public List<Note> NoteList;

    public Transform source;
    public Transform destination;

    [Header("FX")]
    [SerializeField] private GameObject DodgePortal;
    [SerializeField] private GameObject AttackPortal;
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
        //DodgeNoteCopy.gameObject.SetActive(false);
        //AttackNoteCopy.gameObject.SetActive(false);
        //mat = GetComponentInChildren<Renderer>().material;
        //initialColor = mat.color;
    }

    // Update is called once per frame
    void Update()
    {
        MaxNotes(50);
        RemoveFromList();
    }

    public void SpawnDodgeNote(float beat)
    {
        GameObject obj = (GameObject)ObjectPool.GetInstance().GetFromPool(DodgeNoteTag, this.source.position, Quaternion.identity);
        //note = (Note)Instantiate(this.AttackNoteCopy, this.transform.position, this.transform.rotation, this.transform);
        //note.NoteObj().SetActive(true);
        if(obj.TryGetComponent<Note>(out Note note))
        {
            note.InitializeNote(source.position, destination.position, beat);
            NoteList.Add(note);
        }
        

        if(DodgePortal != null)
        {
            GameObject fx = (GameObject)Instantiate(AttackPortal, this.source.position, Quaternion.identity);
            Destroy(fx, 2.0f);
        }
        
        //mat.color = changeColor;
    }

    public void SpawnAttackNote(float beat)
    {
        GameObject obj = (GameObject)ObjectPool.GetInstance().GetFromPool(AttackNoteTag, this.source.position, Quaternion.identity);
        //note = (Note)Instantiate(this.AttackNoteCopy, this.transform.position, this.transform.rotation, this.transform);
        //note.NoteObj().SetActive(true);
        if (obj.TryGetComponent<Note>(out Note note))
        {
            note.InitializeNote(source.position, destination.position, beat);
            NoteList.Add(note);
        }

        if (AttackPortal != null)
        {
            GameObject fx = (GameObject)Instantiate(DodgePortal, this.source.position, Quaternion.identity);
            Destroy(fx, 2.0f);
            //mat.color = changeColor;
        }

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
                //Debug.Log("Destroying" + NoteList[i].name);
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

    public void ChangeNotes(string dgNote, string atkNote)
    {
        DodgeNoteTag = dgNote;
        AttackNoteTag = atkNote;
    }
}

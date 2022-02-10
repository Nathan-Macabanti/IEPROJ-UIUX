using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSpawner : MonoBehaviour
{
    private static TutorialSpawner _spawnerInstance;
    [SerializeField] 
    private int nMaxNotes = 25;
    [SerializeField] 
    public TutorialNote DodgeNote;
    [SerializeField] 
    private TutorialNote AttackNote;
    [SerializeField]
    private TutorialNote JumpNote;
    public List<TutorialNote> NoteList;


    // Start is called before the first frame update
    void Start()
    {
        DodgeNote.gameObject.SetActive(false);
        AttackNote.gameObject.SetActive(false);
        JumpNote.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        MaxNotes(40);
    }

    public void SpawnDodgeNote()
    {
        TutorialNote note;
        note = Instantiate(this.DodgeNote, this.transform.position, this.transform.rotation, this.transform);
        note.NoteObj().SetActive(true);
        NoteList.Add(note);
    }

    public void SpawnAttackNote()
    {
        TutorialNote note;
        note = Instantiate(this.AttackNote, this.transform.position, this.transform.rotation, this.transform);
        note.NoteObj().SetActive(true);
        NoteList.Add(note);
    }

    public void SpawnJumpNote()
    {
        TutorialNote note;
        note = Instantiate(this.JumpNote, this.transform.position, this.transform.rotation, this.transform);
        note.NoteObj().SetActive(true);
        NoteList.Add(note);
    }

    void MaxNotes(int Max)
    {
        if (NoteList.Count >= Max)
        {
            GameObject.Destroy(this.NoteList[0].gameObject);
            NoteList.RemoveAt(0);
        }
    }

    void RemoveFromList()
    {
        for (int i = 0; i < NoteList.Count; i++)
        {
            if (NoteList[i] == null)
            {
                NoteList.RemoveAt(i);
            }
        }
    }

    public void DestroyAllNotes()
    {
        for (int i = 0; i < NoteList.Count; i++)
        {
            if (NoteList[i] != null)
            {
                Debug.Log("Destroying" + NoteList[i].name);
                Destroy(NoteList[i].gameObject);
                NoteList.RemoveAt(i);
            }

        }
    }

}

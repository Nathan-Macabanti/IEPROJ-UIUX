using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNote : MonoBehaviour
{

    [SerializeField] 
    public GameObject Note;
    [SerializeField]
    private float noteSpeed = 5f;
    [SerializeField] 
    public bool isAttackNote;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(noteSpeed, 0, 0);
        Note.transform.Translate(pos * Time.deltaTime);
    }

    public GameObject NoteObj() { return Note; }

    public bool GetIsAttackNote { get { return isAttackNote; } }


}

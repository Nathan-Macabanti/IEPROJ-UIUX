using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour, IPoolable
{
    //[SerializeField] private GameObject noteObj;
    [SerializeField] private float divisor = 6.0f;
    [SerializeField] private bool isAttackNote;
    
    private float speed = 0;
    //[SerializeField] private SongManager2 song;
    protected Vector3 SpawnPosition;
    protected Vector3 DestroyPosition;
    protected float beat;

    //[SerializeField] private ParticleSystem noteParticle;

    private void OnDisable()
    {
        return;
    }
    // Start is called before the first frame update
    void Start()
    {
        //speed = 0;
        if(SongManager2.GetInstance() != null)
        {
            speed = SongManager2.GetInstance().BPM / divisor;
        }
        
        //noteParticle.startColor = noteObj.GetComponent<Renderer>().material.color;
        //SpawnPosition = new Vector3(0F, transform.position.y, transform.position.z);
        //DestroyPosition = new Vector3(15F, transform.position.y, transform.position.z);
        //RandomizeGeminiColor();
    }

    public void InitializeNote(Vector3 source, Vector3 destination, float _beat)
    {
        SpawnPosition = source;
        DestroyPosition = destination;
        beat = _beat;
        //keyToPress = _keyToPress;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*transform.position = Vector3.Lerp(
            SpawnPosition,
            DestroyPosition,
            (song.BeatsShownInAdvance() - (song.Notes() - song.SongPositionInBeats())) / (song.BeatsShownInAdvance() + 1));*/
        Move();
        #if false
        float songPosInBeats = SongManager2.GetInstance().SongPositionInBeats;
        float beatsShownInAdvance = SongManager2.GetInstance().BeatsShownInAdvance;
        float timeToDestination = (beat - songPosInBeats);
        float distance = (beatsShownInAdvance - timeToDestination) / beatsShownInAdvance;

        transform.position = Vector3.Lerp(SpawnPosition, DestroyPosition, distance);
        #endif
    }

    public virtual void Move()
    {
        Vector3 pos = new Vector3(speed, 0, 0);
        transform.Translate(pos * Time.deltaTime);
    }

    //public GameObject NoteObj() { return noteObj; }

    public bool GetIsAttackNote { get { return isAttackNote; } }

    public void ChangeSpeed(float percentage)
    {
        speed *= percentage;
    }

    public void InitializePoolable()
    {
    }

    public void UnregisterPoolable()
    {
    }

    public float GetSpeed { get { return speed; } }
}

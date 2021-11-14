using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] protected GameObject noteObj;
    [SerializeField] private float divisor = 6.0f;
    private float speed = 0;
    [SerializeField] private SongManager2 song;
    [SerializeField] private Vector3 SpawnPosition;
    [SerializeField] private Vector3 DestroyPosition;
    //[SerializeField] private ParticleSystem noteParticle;

    // Start is called before the first frame update
    void Start()
    {
        //speed = 0;
        speed = song.BPM / divisor;
        //noteParticle.startColor = noteObj.GetComponent<Renderer>().material.color;
        //SpawnPosition = new Vector3(0F, transform.position.y, transform.position.z);
        //DestroyPosition = new Vector3(15F, transform.position.y, transform.position.z);
        //RandomizeGeminiColor();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*transform.position = Vector3.Lerp(
            SpawnPosition,
            DestroyPosition,
            (song.BeatsShownInAdvance() - (song.Notes() - song.SongPositionInBeats())) / (song.BeatsShownInAdvance() + 1));*/
        Vector3 pos = new Vector3(speed, 0, 0);
        noteObj.transform.Translate(pos * Time.deltaTime);
    }

    public void RandomizeGeminiColor()
    {
        int r = Random.Range(0, 2);
        int g = Random.Range(0, 2);
        int b = Random.Range(0, 2);

        if (r < 2 && g < 2) { r = 0; g = 0; b = 1;}
        if (g < 2 && r < 2) { r = 0; g = 0; b = 1; }
        if (r < 2 && b < 2) { r = 0; g = 1; b = 0;}
        if (b < 2 && r < 2) { r = 0; g = 1; b = 0; }
        if (b < 2 && g < 2) { r = 1; g = 0; b = 0;}
        if (g < 2 && b < 2) { r = 1; g = 0; b = 0; }
        if (b < 2 && g < 2 && r < 2) { r = 0; g = 0; b = 1; }

        Color color = new Color(r, g, b);
        this.noteObj.GetComponent<MeshRenderer>().material.SetColor("_Color", color);
    }

    public GameObject NoteObj() { return noteObj; }
    /*
    [SerializeField] SongManager song;
    [SerializeField] protected GameObject noteObj;
    [SerializeField] private float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        RandomizeGeminiColor();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Vector3 pos = new Vector3(1, 0, 0);
        noteObj.transform.Translate(pos * speed * Time.deltaTime);*/
        /*transform.position = Vector3.Lerp(
            transform.position,
            new Vector3(0, 0, transform.position.z),
            ((3) - (song.notes[song.nextIndex] - song.songPositionInBeats)) / 3);
    }
/*
    public void RandomizeGeminiColor()
    {
        int r = Random.Range(0, 2);
        int g = Random.Range(0, 2);
        int b = Random.Range(0, 2);

        if (r < 2 && g < 2) { r = 0; g = 0; b = 1; }
        if (g < 2 && r < 2) { r = 0; g = 0; b = 1; }
        if (r < 2 && b < 2) { r = 0; g = 1; b = 0; }
        if (b < 2 && r < 2) { r = 0; g = 1; b = 0; }
        if (b < 2 && g < 2) { r = 1; g = 0; b = 0; }
        if (g < 2 && b < 2) { r = 1; g = 0; b = 0; }
        if (b < 2 && g < 2 && r < 2) { r = 0; g = 0; b = 1; }

        Color color = new Color(r, g, b);
        this.noteObj.GetComponent<MeshRenderer>().material.SetColor("_Color", color);
    }*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    public Transform destination;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn(float beat)
    {
        GameObject obj = (GameObject)Instantiate(prefab, this.transform.position, Quaternion.identity);
        if(obj.TryGetComponent<NoteMovement>(out NoteMovement noteMovement))
        {
            noteMovement.source = this.transform;
            noteMovement.destination = destination;
            noteMovement.beat = beat;
        }
    }
}

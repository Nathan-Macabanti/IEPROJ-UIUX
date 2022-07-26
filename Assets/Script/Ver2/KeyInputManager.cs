using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeyInputManager : MonoBehaviour
{
    private static KeyInputManager instance;
    public static KeyInputManager GetInstance() { return instance; }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Multiple instances");
        }
    }
    public KeyCode key1 = KeyCode.UpArrow;
    public KeyCode key2 = KeyCode.LeftArrow;
    public KeyCode key3 = KeyCode.RightArrow;
    public KeyCode key4 = KeyCode.DownArrow;
}

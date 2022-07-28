using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace NewRhythmSystem
{
    public class NoteUI : MonoBehaviour
    {
        [SerializeField] private Note note;
        private Image keyImage;
        //TextMeshProUGUI keyText;

        private void OnEnable()
        {
            keyImage = GetComponent<Image>();
            //keyText = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            int index = note.GetKey();
            keyImage.sprite = AssetManager.GetInstance().keySprites[index];
            //keyText.text = note.GetKey().ToString();
        }
    }
}


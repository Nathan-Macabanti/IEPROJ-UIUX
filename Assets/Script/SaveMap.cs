using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

namespace BeatMap
{
    public class SaveMap : MonoBehaviour
    {
        [SerializeField] SongManager2 SngManager;
        [SerializeField] Text pathText;
        [SerializeField] Text IsSaving;
        public void WriteToTxtFile()
        {
            string path = Application.dataPath + "/" + pathText.text + ".txt";

            if (!File.Exists(path)) 
            { 
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.WriteLine(SngManager.BPM * 4);
                    for(int i = 0; i < SngManager.Notes.Length; i++)
                    {
                        IsSaving.text = "Saving...";
                        writer.WriteLine(SngManager.Notes[i].ToString());
                    }
                    for (int i = 0; i < SngManager.SpawnerIndexArray.Length; i++)
                    {
                        IsSaving.text = "Saving...";
                        writer.WriteLine(SngManager.SpawnerIndexArray[i]);
                    }
                    writer.WriteLine(SngManager.Notes.Length.ToString());
                }
                IsSaving.text = "Saved";
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewRhythmSystem
{
    public class RankManager : MonoBehaviour
    {
        EventManager eventManagerInstance;

        private void Start()
        {
            eventManagerInstance = EventManager.GetInstance();
        }

        // Start is called before the first frame update
        void OnEnable()
        {
            eventManagerInstance.onNoteCollected += DisplayRank;
        }

        // Update is called once per frame
        void OnDisable()
        {
            eventManagerInstance.onNoteCollected -= DisplayRank;
        }

        public void DisplayRank(CollectionRank rank)
        {
            switch (rank)
            {
                case CollectionRank.PERFECT: Debug.Log("Perfect"); break;
                case CollectionRank.GOOD: Debug.Log("Good"); break;
                case CollectionRank.BAD: Debug.Log("Bad"); break;
                case CollectionRank.MISS: Debug.Log("Miss"); break;
            }
        }
    }
}


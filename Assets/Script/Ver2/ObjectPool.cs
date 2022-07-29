using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewRhythmSystem
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private List<Pool> pools;
        private Dictionary<string, Queue<GameObject>> poolDictionary;
        private static ObjectPool instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        public static ObjectPool GetInstance()
        {
            return instance;
        }

        // Start is called before the first frame update
        void Start()
        {
            poolDictionary = new Dictionary<string, Queue<GameObject>>();
            foreach (Pool pool in pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefab);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                poolDictionary.Add(pool.tag, objectPool);
            }
        }

        //Sets the position and rotation and sets them to active
        public GameObject GetFromPool(string tag, Vector3 pos, Quaternion rot)
        {
            if (!poolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning("Pool " + tag + "does not exist");
                return null;
            }

            if(poolDictionary.Count == 0)
            {
                Debug.LogWarning("No more objects in pool " + tag);
                return null;
            }

            GameObject obj = poolDictionary[tag].Dequeue();

            obj.SetActive(true);
            obj.transform.position = pos;
            obj.transform.rotation = rot;

            if (obj.TryGetComponent<IPoolable>(out IPoolable poolable))
            {
                poolable.InitializePoolable();
            }

            poolDictionary[tag].Enqueue(obj);

            return obj;
        }

        public IEnumerator SetActiveTimed(GameObject obj, float time, bool active)
        {
            yield return new WaitForSeconds(time);
            obj.SetActive(active);
        }
    }

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
}


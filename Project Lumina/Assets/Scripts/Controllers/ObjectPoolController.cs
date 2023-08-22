using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProjectLumina.Controllers
{
    [Serializable]
    public class Pool
    {
        public GameObject prefab;

        [Range(0, 1000)]
        public int size;
    }

    public class ObjectPoolController : MonoBehaviour
    {
        [TableList]
        public List<Pool> pools;

        public Dictionary<string, Queue<GameObject>> poolDictionary;

        #region Singleton

        public static ObjectPoolController Instance
        { get { return _instance; } }

        private static ObjectPoolController _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        #endregion Singleton

        private void Start()
        {
            poolDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach (Pool pool in pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefab, transform);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                poolDictionary.Add(pool.prefab.name, objectPool);
            }
        }

        public GameObject GetPooledObject(string tag, Vector3 position, Quaternion rotation, Transform parent, bool worldPositionStays)
        {
            if (!poolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
                return null;
            }

            GameObject objectToSpawn = poolDictionary[tag].Dequeue();

            objectToSpawn.SetActive(true);
            objectToSpawn.transform.SetPositionAndRotation(position, rotation);
            objectToSpawn.transform.SetParent(parent, worldPositionStays);

            poolDictionary[tag].Enqueue(objectToSpawn);

            return objectToSpawn;
        }

        public GameObject GetPooledObject(string tag, Vector3 position, Transform parent, bool worldPositionStays)
        {
            if (!poolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
                return null;
            }

            GameObject objectToSpawn = poolDictionary[tag].Dequeue();

            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.SetParent(parent, worldPositionStays);

            poolDictionary[tag].Enqueue(objectToSpawn);

            return objectToSpawn;
        }

        public GameObject GetPooledObject(string tag, Vector3 position, Quaternion rotation, bool worldPositionStays)
        {
            if (!poolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
                return null;
            }

            GameObject objectToSpawn = poolDictionary[tag].Dequeue();

            objectToSpawn.SetActive(true);
            objectToSpawn.transform.SetPositionAndRotation(position, rotation);
            objectToSpawn.transform.SetParent(transform, worldPositionStays);

            poolDictionary[tag].Enqueue(objectToSpawn);

            return objectToSpawn;
        }

        public GameObject GetPooledObject(string tag, Vector3 position, bool worldPositionStays)
        {
            if (!poolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
                return null;
            }

            GameObject objectToSpawn = poolDictionary[tag].Dequeue();

            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.SetParent(transform, worldPositionStays);

            poolDictionary[tag].Enqueue(objectToSpawn);

            return objectToSpawn;
        }

        [Button("Sort Lists")]
        public void SortLists()
        {
            pools = pools.OrderBy(i => i.prefab.name.ToString()).ToList();
        }
    }
}
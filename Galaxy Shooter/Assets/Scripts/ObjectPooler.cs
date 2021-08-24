using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    #region Singleton
    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion
    
    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            GameObject poolContainer = new GameObject();
            poolContainer.name = pool.tag;
            poolContainer.transform.parent = transform;
            
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, poolContainer.transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            
            poolDictionary.Add(pool.tag, objectPool);
        }
    }
    
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion quaternion)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag" + tag + " doesn't exist");
            return null;
        }
        Queue<GameObject> pool = poolDictionary[tag];
        GameObject objectToSpawn = pool.Dequeue();
        
        if (objectToSpawn.activeSelf == true)
        {
            NewPoolObject(tag, position, quaternion);
        }
        else
        {
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = quaternion;
        }
        pool.Enqueue(objectToSpawn);
        return objectToSpawn;
    }
    
    GameObject NewPoolObject (String tag, Vector3 position, Quaternion quaternion)
    {
        Pool pool = FindPool(tag);
        GameObject obj = Instantiate(pool.prefab, transform.Find(tag));
        obj.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = quaternion;
        poolDictionary[tag].Enqueue(obj);
        return obj;
    }
    
    Pool FindPool(string tag)
    {
        Pool poolRef = null;
        foreach (Pool pool in pools)
        {
            if (pool.tag.Equals(tag))
            {
                poolRef = pool;
                break;
            }
        }
        return poolRef;
    }
}

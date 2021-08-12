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

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            
            poolDictionary.Add(pool.tag, objectPool);
        }
    }
    
    // private void LateUpdate()
    // {
    //     foreach (Pool pool in pools)
    //     {
    //         Debug.Log(pool.tag);
    //         
    //         GameObject objectToCheck = poolDictionary[pool.tag].Dequeue();
    //         Vector3 position = objectToCheck.transform.position;
    //         
    //         if (position.y > 10f)
    //         {
    //             objectToCheck.SetActive(false);
    //         }
    //
    //         poolDictionary[pool.tag].Enqueue(objectToCheck);
    //     }
    // }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion quaternion)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag" + tag + " doesn't exist");
            return null;
        }
        
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = quaternion;
        
        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }

    
    void NewPoolObject (Pool pool, Vector3 position, Quaternion quaternion)
    {
        GameObject obj = Instantiate(pool.prefab);
        
        obj.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = quaternion;
        
        poolDictionary[pool.tag].Enqueue(obj);
    }
    
    Pool GetPool(string tag)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _spawnRate;
    
    private float _nextSpawn;
    private Vector3 _nextSpawnLocation;
    private ObjectPooler _objectPooler;
    
    private Vector2 _positionMin = new Vector2(-10f, -6f);
    private Vector2 _positionMax = new Vector2(10, 8);
    
    // Start is called before the first frame update
    void Start()
    {
        _nextSpawn = _spawnRate;
        _objectPooler = ObjectPooler.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > _nextSpawn)
        {
            _nextSpawnLocation = new Vector3(Random.Range(_positionMin.x, _positionMax.x), _positionMax.y, 0);
            _objectPooler.SpawnFromPool("Enemy", _nextSpawnLocation, new Quaternion());
            _nextSpawn = Time.time + _spawnRate;
        }
    }
}

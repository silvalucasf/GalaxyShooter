using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float _spawnRate = 5f;
    
    private float _nextSpawn;
    private Vector3 _nextSpawnLocation;
    private ObjectPooler _objectPooler;
    
    private Vector2 _positionMin = new Vector2(-10f, -6f);
    private Vector2 _positionMax = new Vector2(10, 8);

    private bool _isSpawning = true;
    void Start()
    {
        _objectPooler = ObjectPooler.Instance;
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator SpawnRoutine()
    {
        while (_isSpawning)
        {
            _nextSpawnLocation = new Vector3(Random.Range(_positionMin.x, _positionMax.x), _positionMax.y, 0);
            _objectPooler.SpawnFromPool("Enemy", _nextSpawnLocation, new Quaternion());
            yield return new WaitForSeconds(_spawnRate);
        }
    }

    public bool IsSpawning
    {
        get => _isSpawning;
        set => _isSpawning = value;
    }
}

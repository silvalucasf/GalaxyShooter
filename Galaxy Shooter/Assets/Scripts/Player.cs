using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject laserPrefab;
    
    [SerializeField] private Transform _firePosition;
    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private int _lives = 3;
    
    private Vector2 _positionMin = new Vector2(-10f, -4f);
    private Vector2 _positionMax = new Vector2(10f, 6f);

    private float _inputHorizontal;
    private float _inputVertical;

    private float _nextFire;
    private ObjectPooler _objectPooler;
    
    private void Start()
    {
        _nextFire = 0;
        _objectPooler = ObjectPooler.Instance;
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (Time.time > _nextFire)
            {
                _objectPooler.SpawnFromPool("Laser", _firePosition.position, _firePosition.rotation);
                _nextFire = Time.time + _fireRate;
            }
        }
    }

    private void FixedUpdate()
    {
        NextPosition();
        CheckBoundsPosition();
    }

    void NextPosition()
    {
        _inputHorizontal = Input.GetAxis("Horizontal");
        _inputVertical = Input.GetAxis("Vertical");

        Vector3 nextPostion = new Vector3(_inputHorizontal,_inputVertical,0);
        nextPostion *= Time.fixedDeltaTime;
        nextPostion *= _speed;
        
        transform.Translate(nextPostion);
    }
    
    void CheckBoundsPosition()
    {
        Vector3 position = transform.position;
        
        position.y = Mathf.Clamp(position.y, _positionMin.y, _positionMax.y);

        if (position.x > _positionMax.x)
            position.x = _positionMin.x;
        
        else if (position.x < _positionMin.x)
            position.x = _positionMax.x;

        transform.position = position;
    }

    public void Damage(int damage)
    {
        _lives -= damage;
        
        if(_lives < 1)
            Destroy(this.gameObject);
    }
}

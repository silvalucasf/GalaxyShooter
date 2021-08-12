using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    
    private Vector2 _positionMin = new Vector2(-10f, -4f);
    private Vector2 _positionMax = new Vector2(10f, 6f);

    private float _inputHorizontal;
    private float _inputVertical;

    private void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        NextPosition();
        CheckBoundsPosition();
    }

    void NextPosition()
    {
        _inputHorizontal = Input.GetAxis("Horizontal");
        _inputVertical = Input.GetAxis("Vertical");

        Vector3 nextPostion = new Vector3(_inputHorizontal,_inputVertical,0);
        nextPostion *= Time.deltaTime;
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
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _speed = 15f;
    [SerializeField] private float _shootRange = 10f;
    private void FixedUpdate()
    {
        if (transform.position.y < _shootRange)
        {
            transform.Translate(Vector3.up * (_speed * Time.deltaTime));
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}

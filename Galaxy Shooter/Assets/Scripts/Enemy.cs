using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * (Time.deltaTime * -_speed));
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Player":
                Player player = other.transform.GetComponent<Player>();

                if (player != null)
                {
                    player.Damage(1);
                }
                
                gameObject.SetActive(false);
                break;
            
            case "Laser":
                other.gameObject.SetActive(false);
                gameObject.SetActive(false);
                break;
        }
    }
}

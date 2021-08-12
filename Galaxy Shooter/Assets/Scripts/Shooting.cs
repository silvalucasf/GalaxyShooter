using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject laserPrefab;
    public List<GameObject> _activeLasers;
    public List<GameObject> _inactiveLasers;
    
    // Start is called before the first frame update
    void Start()
    {
        _activeLasers = new List<GameObject>();
        _inactiveLasers = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(Transform transform)
    {
        GameObject laser;
        if (_inactiveLasers.Count <= 0)
        {
            laser = Instantiate(laserPrefab, transform.position, transform.rotation);
            _activeLasers.Add(laser);
        }
        else
        {
           // laser = _inactiveLasers(0);
        }
    }
}

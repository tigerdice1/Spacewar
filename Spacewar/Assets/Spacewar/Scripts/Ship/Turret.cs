using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed;
    [SerializeField]
    private GameObject[] _bulletSpawn;

    private GameObject _bullet;

    public float RotationSpeed{
        set { _rotationSpeed = value; }
        get { return _rotationSpeed; }
    }

    public GameObject[] BulletSpawn{
        set { _bulletSpawn = value; }
        get { return _bulletSpawn; }
    }

    void Initailize(){
        _bulletSpawn = GameObject.FindGameObjectsWithTag("BulletSpawn");
    }
    // Start is called before the first frame update
    void Start()
    {
        Initailize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

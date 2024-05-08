using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    MainShip _ownerShip;
    [SerializeField]
    private float _rotationSpeed;
    [SerializeField]
    private List<GameObject> _bulletSpawn;

    private int _fireOrder = 0;
    [SerializeField]
    private float _rpm;
    [SerializeField]
    private GameObject _bullet;

    private bool _isFire;

    private float _time = 0f;
    public float RotationSpeed{
        set => _rotationSpeed = value;
        get => _rotationSpeed;
    }

    public List<GameObject> BulletSpawn{
        set => _bulletSpawn = value;
        get => _bulletSpawn;
    }

    public bool IsFire{
        set => _isFire = value; 
        get => _isFire; 
    }
    private void Fire(){
        _time += Time.deltaTime;
        float rpm = _rpm * 0.000167f;
            if(_bullet != null && _time >= rpm){
                if(_fireOrder >= _bulletSpawn.Count){
                    _fireOrder = 0;
                }
                _bullet.GetComponent<Projectile>().OwnerShip = _ownerShip;
                Instantiate(_bullet,_bulletSpawn[_fireOrder].transform);
                _time = 0f;
                _fireOrder++;
        }
    }
    void Initailize(){
        foreach(GameObject bulletSpawn in GameObject.FindGameObjectsWithTag("BulletSpawn")){
            _bulletSpawn.Add(bulletSpawn);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Initailize();
    }

    // Update is called once per frame
    void Update()
    {
        if(_isFire){
            Fire();
        }
    }
}

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
        float rpm = _rpm / 60;
        float spr = 1 / rpm;
            if(_bullet != null && _time >= spr){
                if(_fireOrder >= _bulletSpawn.Count){
                    _fireOrder = 0;
                }
                _bullet.GetComponent<Projectile>().OwnerShip = _ownerShip;
                Instantiate(_bullet, _bulletSpawn[_fireOrder].transform);
                //Instantiate(_bullet, new Vector3(_bulletSpawn[_fireOrder].transform.position.x ,_bulletSpawn[_fireOrder].transform.position.y, _bulletSpawn[_fireOrder].transform.position.z), _bullet.transform.localRotation, _bulletSpawn[_fireOrder].transform);
                _time = 0f;
                _fireOrder++;
        }
    }
    void Initailize(){
        List<GameObject> bulletSpawn = new List<GameObject>(GameObject.FindGameObjectsWithTag("BulletSpawn"));
        for(int i = 0; i < bulletSpawn.Count; i++){
            _bulletSpawn.Add(bulletSpawn[i]);
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

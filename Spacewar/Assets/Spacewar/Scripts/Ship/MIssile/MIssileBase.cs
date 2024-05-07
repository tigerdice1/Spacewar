using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBase : MonoBehaviour
{
    [SerializeField]
    protected MainShip _ownerShip;
    protected float _missileVelocity;
    protected float _missileDamage;

    protected float _rotationSpeed;

    protected GameObject _tgt;

    protected bool _isLaunched;

    public MainShip OwnerShip{
        set { _ownerShip = value; }
    }
    protected virtual void Initailze(){
        this.transform.SetParent(null);
        _missileDamage = 1.0f;
        _missileVelocity = 50.0f;
        _rotationSpeed = 2.0f;
        _isLaunched = true;
    }

    // Start is called before the first frame update
    protected void Start(){
        Initailze();
    }

    // Update is called once per frame
    protected void Update(){
        if(_isLaunched){
            Rigidbody rid = this.GetComponent<Rigidbody>();
            rid.AddRelativeForce(Vector3.up * _missileVelocity);
            //rid.AddRelativeTorque(Vector3.up * _rotationSpeed);
        }
    }
}

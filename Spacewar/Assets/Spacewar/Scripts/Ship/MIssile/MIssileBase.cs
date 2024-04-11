using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBase : MonoBehaviour
{
    [SerializeField]
    protected MainShip _ownerShip;
    protected float _missileVelocity;
    protected float _missileDamage;

    protected bool _isLaunched;

    public MainShip OwnerShip{
        set { _ownerShip = value; }
    }
    protected void Initailze(){
        _missileDamage = 1.0f;
        _missileVelocity = 10.0f;
        _isLaunched = false;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        gameObject.transform.SetParent(null);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(_isLaunched){
            Rigidbody rid = gameObject.GetComponent<Rigidbody>();
            rid.AddRelativeForce(Vector3.forward * _missileVelocity);
        }
    }
}

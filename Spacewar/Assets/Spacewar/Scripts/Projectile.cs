using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    protected MainShip _ownerShip;
    [SerializeField]
    protected float _projectileVelocity;
    [SerializeField]
    protected float _projectileDamage;
    protected bool _isLaunched;

    [SerializeField]
    protected float _destoryTimer;

    private float _timer;

    public MainShip OwnerShip{
        set { _ownerShip = value; }
        get { return _ownerShip; }
    }
    protected void Initailze(){
        this.transform.SetParent(null);
        _isLaunched = true;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Initailze();
        if(_isLaunched){
            Rigidbody rid = this.GetComponent<Rigidbody>();
            rid.AddRelativeForce(Vector3.up * _projectileVelocity * 20f);
            Destroy(gameObject,_destoryTimer);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

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
        set =>  value = _ownerShip;
        get => _ownerShip;
    }

    public float ProjectileDamage{
        set =>  value = _projectileDamage;
        get => _projectileDamage;
    }
    protected void Initailze(){
        this.transform.SetParent(null);
        Rigidbody rid = this.GetComponent<Rigidbody>();
        rid.AddRelativeForce(Vector3.forward * _projectileVelocity * rid.mass * 10f);
        Destroy(gameObject,_destoryTimer);
    }

    // Start is called before the first frame update
    protected virtual void Start(){
        Initailze(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Vector3 _moveDirection;
    private float _moveSpeed;

    private float _asteroidHP;

    private DamageManager _damageManager;

    private void Initalize(){
        if(!gameObject.GetComponent<MeshCollider>()){
            Debug.Log("MeshCollider Not Contained");
        }
        _damageManager = new DamageManager();
        _moveSpeed = Random.Range(0f, 100f);
        _moveDirection = new Vector3(Random.Range(0f,1f), 0f, Random.Range(0f,1f));
        gameObject.GetComponent<Rigidbody>().AddForce(_moveDirection * _moveSpeed);
        gameObject.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
    }

    private void OnTriggerEnter(Collider overlappedObject){
        if(overlappedObject.CompareTag("MainShip")){

        }
        else if(overlappedObject.CompareTag("Projectile")){

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Initalize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

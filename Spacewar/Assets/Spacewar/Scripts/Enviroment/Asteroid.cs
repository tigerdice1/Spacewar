using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Tooltip("소행성의 초기 방향을 지정하는 변수입니다. 자동으로 지정됩니다.")]
    private Vector3 _moveDirection;
    [Tooltip("소행성의 초기 속도입니다.")]
    private float _moveSpeed;

    [Tooltip("소행성의 초기 체력입니다.")]
    private float _asteroidHP;

    public float AsteroidHP{
        set => _asteroidHP = value;
        get => _asteroidHP;
    }

    private void Initalize(){
        if(SceneManager.Instance().IsDebugMode()){
            if(!this.GetComponent<MeshCollider>()){
                Debug.Log("MeshCollider Not Contained");
            }
            _asteroidHP = 100f;
        }
        
        float scale = Random.Range(1f, 50f);
        this.transform.localScale = new Vector3(scale, scale, scale);
        //_damageManager = new DamageManager();
        _moveSpeed = Random.Range(0f, 1000f);
        _moveDirection = new Vector3(Random.Range(0f,1f), 0f, Random.Range(0f,1f));
        this.GetComponent<Rigidbody>().AddForce(_moveDirection * _moveSpeed);
        this.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
    }

    private void OnTriggerEnter(Collider overlappedObject){
        if(overlappedObject.CompareTag("MainShip")){

        }
        else if(overlappedObject.CompareTag("Projectile")){
            Debug.Log("ddd");
            DamageManager damageMgr = new DamageManager();
            damageMgr.Damage(overlappedObject.transform.gameObject, this.gameObject);
            
        }
    }
    // Start is called before the first frame update
    void Start(){
        Initalize();
    }

    // Update is called once per frame
    void Update()
    {
        if(this._asteroidHP <= 0){
            Destroy(this);
        }
    }
}

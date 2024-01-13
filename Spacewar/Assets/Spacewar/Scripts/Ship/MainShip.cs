using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainShip : MonoBehaviour{

    [SerializeField]
    [Tooltip("")]
    private PlayerController _playerController;

    [SerializeField]
    [Tooltip("")]
    private float _speed;

    private float _currentAngularSpeed;
    private Quaternion _previousRotation;
    private Vector3 _axis = Vector3.zero;

    private float _angle = 0.0f;
    private bool _isReverseThrusterActive = true;

    public bool IsReverseThrusterActive{
        get {return _isReverseThrusterActive;}
        set {_isReverseThrusterActive = value;}
    }
    public float Speed{
        get{ return _speed;}
        set{ _speed = value;}
    }

    public float GetAngularSpeed{
        get{return _currentAngularSpeed;}
    }

    void CalcAngularSpeed(){ 
        Quaternion currentRotation = transform.rotation;
        Quaternion deltaRotation = currentRotation * Quaternion.Inverse(_previousRotation);


        deltaRotation.ToAngleAxis(out _angle, out _axis);
        _currentAngularSpeed = _angle / Time.deltaTime;

        _previousRotation = transform.rotation;
        //Debug.Log(_currentAngularSpeed);
    }

    void ReverseThruster(){
        Rigidbody rid = gameObject.GetComponent<Rigidbody>();
        //Debug.Log(rid.velocity.magnitude);
        //Debug.Log(rid.transform.forward);
        if(_isReverseThrusterActive){
            Vector3 relativeForwardVelocity = transform.InverseTransformDirection(rid.velocity);
            ReverseThrusterRotate(rid);
            ReverseThrusterForward(rid);
        }
    }

    void ReverseThrusterRotate(Rigidbody rb){
        float fixedTorque = Mathf.Lerp(_currentAngularSpeed * 0.2f, 0.0f, Time.deltaTime);
        if(_axis.y > -0.0f){
            rb.AddRelativeTorque(Vector3.down * fixedTorque);
        }
        else if(_axis.y < 0.0f){
            rb.AddRelativeTorque(Vector3.up * fixedTorque);
        }
    }
    /*
    Debug.Log(rid.transform.forward);
    전방(0.0f, 0.0f, 1.0f)
    후방(0.0f, 0.0f, -1.0f)
    좌측(-1.0f, 0.0f, 0.0f)
    우측(1.0f, 0.0f, 0.0f)
    Debug.Log(rid.velocity);
    전진(0.0f, 0.0f, 1.0f)
    후진(0.0f, 0.0f, -1.0f)
    좌측(-1.0f, 0.0f, 0.0f)
    우측(1.0f, 0.0f, 0.0f)
*/
    void ReverseThrusterForward(Rigidbody rb){
        Debug.Log(rb.velocity);
        Debug.Log(rb.transform.forward);
        
        float fixedSpeed = 10.0f;//Mathf.Lerp(rb.velocity.magnitude, 0.0f, Time.deltaTime);
        
        if(rb.velocity.z > 0.0f){ // 상단으로 이동하는 상황
            if(rb.transform.forward.z > 0.0f){ // 함선의 머리가 상단을 향해 있을 떄
                rb.AddRelativeForce(Vector3.back * fixedSpeed);
                Debug.Log("상단이동 상단");
            }
            else if(rb.transform.forward.z < 0.0f){ // 함선의 머리가 하단을 향해 있을 떄
                rb.AddRelativeForce(Vector3.forward * fixedSpeed);
                Debug.Log("상단이동 하단");
            }
            if(rb.transform.forward.x > 0.0f){ // 함선의 머리가 우측을 향해 있을 때
                rb.AddRelativeForce(Vector3.right * fixedSpeed);
                Debug.Log("상단이동 우측");
            }
            if(rb.transform.forward.x < 0.0f){ // 함선의 머리가 좌측을 향해 있을 때
                rb.AddRelativeForce(Vector3.left * fixedSpeed);
                Debug.Log("상단이동 좌측");
            }
        }
        else if(rb.velocity.z < 0.0f){ // 하단으로 이동하는 상황
            if(rb.transform.forward.z > 0.0f){ // 함선의 머리가 상단을 향해 있을 떄
                rb.AddRelativeForce(Vector3.forward * fixedSpeed);
                Debug.Log("하단이동 상단");
            }
            else if(rb.transform.forward.z < 0.0f){ // 함선의 머리가 하단을 향해 있을 떄
                rb.AddRelativeForce(Vector3.back * fixedSpeed);
                Debug.Log("하단이동 하단");
            }
            if(rb.transform.forward.x > 0.0f){ // 함선의 머리가 우측을 향해 있을 때
                rb.AddRelativeForce(Vector3.left * fixedSpeed);
                Debug.Log("하단이동 우측");
            }
            if(rb.transform.forward.x < 0.0f){ // 함선의 머리가 좌측을 향해 있을 때
                rb.AddRelativeForce(Vector3.right * fixedSpeed);
                Debug.Log("하단이동 좌측");
            }
        }
        if(rb.velocity.x > 0.0f){ // 우측으로 이동하는 상황
            if(rb.transform.forward.z > 0.0f){ // 함선의 머리가 상단을 향해 있을 떄
                rb.AddRelativeForce(Vector3.right * fixedSpeed);
                Debug.Log("우측이동 상단");
            }
            else if(rb.transform.forward.z < 0.0f){ // 함선의 머리가 하단을 향해 있을 떄
                rb.AddRelativeForce(Vector3.left * fixedSpeed);
                Debug.Log("우측이동 하단");
            }
            if(rb.transform.forward.x > 0.0f){ // 함선의 머리가 우측을 향해 있을 때
                rb.AddRelativeForce(Vector3.back * fixedSpeed);
                Debug.Log("우측이동 우측");
            }
            if(rb.transform.forward.x < 0.0f){ // 함선의 머리가 좌측을 향해 있을 때
                rb.AddRelativeForce(Vector3.forward * fixedSpeed);
                Debug.Log("우측이동 좌측");
            }
        }
        else if(rb.velocity.x < 0.0f){ // 좌측으로 이동하는 상황
            if(rb.transform.forward.z > 0.0f){ // 함선의 머리가 상단을 향해 있을 떄
                rb.AddRelativeForce(Vector3.left * fixedSpeed);
                Debug.Log("좌측이동 상단");
            }
            else if(rb.transform.forward.z < 0.0f){ // 함선의 머리가 하단을 향해 있을 떄
                rb.AddRelativeForce(Vector3.right * fixedSpeed);
                Debug.Log("좌측이동 하단");
            }
            if(rb.transform.forward.x > 0.0f){ // 함선의 머리가 우측을 향해 있을 때
                rb.AddRelativeForce(Vector3.forward * fixedSpeed);
                Debug.Log("좌측이동 우측");
            }
            if(rb.transform.forward.x < 0.0f){ // 함선의 머리가 좌측을 향해 있을 때
                rb.AddRelativeForce(Vector3.back * fixedSpeed);
                Debug.Log("좌측이동 좌측");
            }
        }
    }
    void ReverseThrusterSlide(Rigidbody rb){
        float fixedSpeed = Mathf.Lerp(rb.velocity.magnitude, 0.0f, Time.deltaTime);
        if(rb.transform.forward.x < 0.0f && rb.velocity.z > 0.0f){
            rb.AddRelativeForce(Vector3.left * fixedSpeed);
        }
        else if(rb.transform.forward.x > 0.0f  && rb.velocity.z < 0.0f){
            
            rb.AddRelativeForce(Vector3.right * fixedSpeed);
        }
    }


    
    // Start is called before the first frame update
    void Start()
    {
        _previousRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    void FixedUpdate(){
        CalcAngularSpeed();
        ReverseThruster();
    }
}

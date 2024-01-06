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
        if(_isReverseThrusterActive){
            Vector3 relativeForwardVelocity = transform.InverseTransformDirection(rid.velocity);

            float absSpeed = relativeForwardVelocity.x + relativeForwardVelocity.y + relativeForwardVelocity.z;
            float fixedSpeed = Mathf.Lerp(absSpeed, 0.0f, Time.deltaTime);
            Debug.Log(fixedSpeed);
            float fixedTorque = Mathf.Lerp(_currentAngularSpeed * 0.2f, 0.0f, Time.deltaTime);
            if(_axis.y > -0.0f){
                rid.AddRelativeTorque(Vector3.down * fixedTorque);
            }
            else if(_axis.y < 0.0f){
                rid.AddRelativeTorque(Vector3.up * fixedTorque);
            }
            
            if(fixedSpeed < 0.0f){
                rid.AddRelativeForce(Vector3.forward * fixedSpeed);
            }
            else if(fixedSpeed > 0.0f){
                rid.AddRelativeForce(Vector3.back * fixedSpeed);
            }
            

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

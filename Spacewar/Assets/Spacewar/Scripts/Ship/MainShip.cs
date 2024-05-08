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

    [SerializeField]
    private List<MissileRoom> _missileRooms;

    [SerializeField]
    private List<MissileRoom> _loadedMissileRooms;
    [Tooltip("")]

    private GameObject _targetObject;

    private float _shipHP;
    private float _rotationSpeed = 1f;
    private float _currentAngularSpeed;
    private Quaternion _previousRotation;
    private Vector3 _axis = Vector3.zero;

    private float _angle = 0.0f;
    private bool _isReverseThrusterActive = true;

    public bool IsReverseThrusterActive{
        set => _isReverseThrusterActive = value;
        get => _isReverseThrusterActive;
    }

    public float ShipHP{
        set => _shipHP = value;
        get => _shipHP;
    }
    public float Speed{
        set => _speed = value;
        get => _speed;
    }

    public float RotationSpeed{
        set => _rotationSpeed = value;
        get => _rotationSpeed;
    }

    public float GetAngularSpeed{
        get => _currentAngularSpeed;
    }

    public List<MissileRoom> GetLoadedMissileRooms{
        get => _loadedMissileRooms;
    }

    public GameObject TargetObject{
        set => _targetObject = value;
        get => _targetObject;
    }

    void ChcekLoadedMissileRooms(){
        for(int i = 0; i < _missileRooms.Count; i++){
            if(_missileRooms[i] && !_loadedMissileRooms.Contains(_missileRooms[i])){
                _loadedMissileRooms.Add(_missileRooms[i]);
            }
            if(!_missileRooms[i].IsMissileLoaded && _loadedMissileRooms.Contains(_missileRooms[i])){
                _loadedMissileRooms.Remove(_missileRooms[i]);
            }
        } 
    }
    void CalcAngularSpeed(){
        Quaternion currentRotation = transform.rotation;
        Quaternion deltaRotation = currentRotation * Quaternion.Inverse(_previousRotation);

        deltaRotation.ToAngleAxis(out _angle, out _axis);
        _currentAngularSpeed = _angle / Time.deltaTime;
        _previousRotation = transform.rotation;
    }

    void ReverseThruster(){
        Rigidbody shipRigidBody = gameObject.GetComponent<Rigidbody>();
        if(_isReverseThrusterActive){
            ReverseThrusterRotate(shipRigidBody);
            ReverseThrusterForward(shipRigidBody);
        }
    }

    void ReverseThrusterRotate(Rigidbody rb){
        float fixedTorque = Mathf.Lerp(_currentAngularSpeed * 0.2f, 0.0f, Time.deltaTime);
        if(_axis.y > 0.0f){
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
        if(rb.velocity.z > 0.0f){ // 게임화면 기준 상단이동
            rb.AddRelativeForce(Vector3.back * rb.transform.forward.z * rb.velocity.magnitude);
            rb.AddRelativeForce(Vector3.right * rb.transform.forward.x * rb.velocity.magnitude);
        }
        if(rb.velocity.z < 0.0f){ // 게임화면 기준 하단이동
            rb.AddRelativeForce(Vector3.forward * rb.transform.forward.z * rb.velocity.magnitude);
            rb.AddRelativeForce(Vector3.left * rb.transform.forward.x * rb.velocity.magnitude);
        }
        if(rb.velocity.x < 0.0f){ // 게임화면 기준 좌측이동
            rb.AddRelativeForce(Vector3.right * rb.transform.forward.z * rb.velocity.magnitude);
            rb.AddRelativeForce(Vector3.forward * rb.transform.forward.x * rb.velocity.magnitude);
        }
        if(rb.velocity.x > 0.0f){ // 게임화면 기준 우측이동
            rb.AddRelativeForce(Vector3.left * rb.transform.forward.z * rb.velocity.magnitude);
            rb.AddRelativeForce(Vector3.back * rb.transform.forward.x * rb.velocity.magnitude);
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
        ChcekLoadedMissileRooms();
        
    }
    void FixedUpdate(){
        CalcAngularSpeed();
        //ReverseThruster();
    }
}

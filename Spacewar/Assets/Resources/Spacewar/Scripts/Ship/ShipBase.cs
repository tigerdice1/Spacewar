using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBase : MonoBehaviour, IControllable
{
    public float Speed;
    public float ShipRotationSpeed;
    public float RotationSpeed => ShipRotationSpeed;
    public float ShipMaxHP;
    public float ShipHP;

    protected Rigidbody _rigidbody;



    [SerializeField]
    [Tooltip("")]
    protected PlayerController _playerController;

    [SerializeField]
    protected List<MissileRoom> _missileRooms;

    [SerializeField]
    protected List<MissileRoom> _loadedMissileRooms;
    [Tooltip("")]

    protected GameObject _targetObject;

    protected float _currentAngularSpeed;
    protected Quaternion _previousRotation;
    protected Vector3 _axis = Vector3.zero;

    protected float _angle = 0.0f;

    public float GetAngularSpeed{
        get => _currentAngularSpeed;
    }

    public List<MissileRoom> GetLoadedMissileRooms(){
        return _loadedMissileRooms;
    }

    public GameObject TargetObject{
        set => _targetObject = value;
        get => _targetObject;
    }

    protected void ChcekLoadedMissileRooms(){
        for(int i = 0; i < _missileRooms.Count; i++){
            if(_missileRooms[i] && !_loadedMissileRooms.Contains(_missileRooms[i])){
                _loadedMissileRooms.Add(_missileRooms[i]);
            }
            if(!_missileRooms[i].IsMissileLoaded && _loadedMissileRooms.Contains(_missileRooms[i])){
                _loadedMissileRooms.Remove(_missileRooms[i]);
            }
        } 
    }
    protected void CalcAngularSpeed(){
        Quaternion currentRotation = transform.rotation;
        Quaternion deltaRotation = currentRotation * Quaternion.Inverse(_previousRotation);

        deltaRotation.ToAngleAxis(out _angle, out _axis);
        _currentAngularSpeed = _angle / Time.deltaTime;
        _previousRotation = transform.rotation;
    }

    public void Move(PlayerController controller){
        Vector3 force = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) force += Vector3.forward;
        if (Input.GetKey(KeyCode.A)) force += Vector3.left;
        if (Input.GetKey(KeyCode.S)) force += Vector3.back;
        if (Input.GetKey(KeyCode.D)) force += Vector3.right;

        _rigidbody.AddRelativeForce(force.normalized * Speed);

        float maxVelocity = Speed;
        _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, maxVelocity);
    }

    public void Look(PlayerController controller, float maxRotationSpeed, bool useSlerp){
        controller.LookAtCursor(maxRotationSpeed, useSlerp);
    }

    public void HandleMouseClick(PlayerController controller){
        var missileRooms = GetLoadedMissileRooms();
        foreach (var room in missileRooms){
            room.LaunchMissile();
        }
    }

    public void UpdateAnimation(PlayerController controller){
        // 필요 시 구현
    }

    protected void Awake(){
        _rigidbody = GetComponent<Rigidbody>();
    }

    protected virtual void Initalize(){
        _previousRotation = transform.rotation;
    }
    // Start is called before the first frame update
    protected virtual void Start(){
        
    }

    // Update is called once per frame
    protected virtual void Update(){
        
    }
}

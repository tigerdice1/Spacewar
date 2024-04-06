using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileRoom : MonoBehaviour
{
    [SerializeField]
    float _doorTimer;
    [SerializeField]
    bool _missileLoadSeq;
    [SerializeField]
    bool _isMissileLoaded;
    [SerializeField]
    Transform _innerDoorLeft;
    [SerializeField]
    Transform _innerDoorRight;
    [SerializeField]
    Transform _outerDoorLeft;
    [SerializeField]
    Transform _outerDoorRight;
    Vector3 _innerDoorLeftClosedPosition;
    Vector3 _innerDoorRightClosedPosition;
    Vector3 _outerDoorLeftClosedPosition;
    Vector3 _outerDoorRightClosedPosition;
    Vector3 _innerDoorLeftOpenPosition;
    Vector3 _innerDoorRightOpenPosition;
    Vector3 _outerDoorLeftOpenPosition;
    Vector3 _outerDoorRightOpenPosition;

    [SerializeField]
    GameObject _missileSpawn;

    void Initalize(){
        _innerDoorLeft = transform.Find("Room_Missile_InnerDoorLeft");
        _innerDoorRight = transform.Find("Room_Missile_InnerDoorRight");
        _outerDoorLeft = transform.Find("Room_Missile_OuterDoorLeft");
        _outerDoorRight = transform.Find("Room_Missile_OuterDoorRight");
        _innerDoorLeftClosedPosition= _innerDoorLeft.position;
        _innerDoorRightClosedPosition = _innerDoorRight.position;
        _outerDoorLeftClosedPosition = _outerDoorLeft.position;
        _outerDoorRightClosedPosition = _outerDoorRight.position;

        _innerDoorLeftOpenPosition = new Vector3(_innerDoorLeftClosedPosition.x - 3.5f, _innerDoorLeftClosedPosition.y, _innerDoorLeftClosedPosition.z);
        _innerDoorRightOpenPosition = new Vector3(_innerDoorRightClosedPosition.x + 3.5f, _innerDoorRightClosedPosition.y, _innerDoorRightClosedPosition.z);
        _innerDoorLeftOpenPosition = new Vector3(_innerDoorLeftClosedPosition.x - 3.5f, _innerDoorLeftClosedPosition.y, _innerDoorLeftClosedPosition.z);
        _innerDoorRightOpenPosition = new Vector3(_innerDoorRightClosedPosition.x + 3.5f, _innerDoorRightClosedPosition.y, _innerDoorRightClosedPosition.z);
        _outerDoorLeftOpenPosition = new Vector3(_outerDoorLeftClosedPosition.x - 3.5f, _outerDoorLeftClosedPosition.y, _outerDoorLeftClosedPosition.z);
        _outerDoorRightOpenPosition = new Vector3(_outerDoorRightClosedPosition.x + 3.5f, _outerDoorRightClosedPosition.y, _outerDoorRightClosedPosition.z);
        _outerDoorLeftOpenPosition = new Vector3(_outerDoorLeftClosedPosition.x - 3.5f, _outerDoorLeftClosedPosition.y, _outerDoorLeftClosedPosition.z);
        _outerDoorRightOpenPosition = new Vector3(_outerDoorRightClosedPosition.x + 3.5f, _outerDoorRightClosedPosition.y, _outerDoorRightClosedPosition.z);
        if(!_isMissileLoaded){
            _innerDoorLeft.position = _innerDoorLeftOpenPosition;
            _innerDoorRight.position = _innerDoorRightOpenPosition;
            _missileLoadSeq = false;
        }
    }
    public void LaunchMissile(){
        MissileLaunchSeq();
    }
    private void MissileLaunchSeq(){
        if(_isMissileLoaded){

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
        if(_missileLoadSeq && !_isMissileLoaded){
            _doorTimer += Time.deltaTime;
            _outerDoorLeft.position = Vector3.Lerp(_outerDoorLeft.position, _outerDoorLeftClosedPosition, Time.deltaTime);
            _outerDoorRight.position = Vector3.Lerp(_outerDoorRight.position, _outerDoorRightClosedPosition, Time.deltaTime);
            if(_doorTimer >= 5f){
                _innerDoorLeft.position = Vector3.Lerp(_innerDoorLeft.position, _innerDoorLeftOpenPosition, Time.deltaTime);
                _innerDoorRight.position = Vector3.Lerp(_innerDoorRight.position, _innerDoorRightOpenPosition, Time.deltaTime);
            }
            if(_doorTimer >= 10){
                _doorTimer = 0f;
                _missileLoadSeq = false;
            }
        }
        if(_missileLoadSeq){
            _doorTimer += Time.deltaTime;
            _innerDoorLeft.position = Vector3.Lerp(_innerDoorLeft.position, _innerDoorLeftClosedPosition, Time.deltaTime);
            _innerDoorRight.position = Vector3.Lerp(_innerDoorRight.position, _innerDoorRightClosedPosition, Time.deltaTime);
            if(_doorTimer >= 10){
                _isMissileLoaded = true;
                _doorTimer = 0f;
                _missileLoadSeq = false;
            }
        }
    }
}

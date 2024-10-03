using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    [Tooltip("작동할 문의 Transform입니다. 자동으로 지정됩니다.")]
    private Transform _door;
    [Tooltip("문이 자동으로 닫히면 위치하게 될 위치좌표입니다.")]
    private Vector3 _doorClosedPosition;

    [Tooltip("문이 자동으로 닫히기까지 걸리는 시간입니다.")]
    private float _autocloseTime = 2.0f;

    [Tooltip("문이 닫히기까지 열린 상태를 유지하기 위한 타이머.")]
    private float _timer;

    [Tooltip("플레이어가 센서에 들어왔는지 확인하는 변수입니다.")]
    private bool _isTriggered;

    // 현재 GameObject 에서 문을 담당할 자식오브젝트를 찾아 변수에 저장하고 현재 위치를 닫았을 때 위치로 지정합니다.
    private void Initalize(){
        _door = transform.Find("Corridor_Door");
        _doorClosedPosition = _door.position;
    }

    private void Start(){
        Initalize();
    }

    // 트리거 존에 플레이어가 들어오면 true
    private void OnTriggerEnter(Collider other){
     if(other.CompareTag("Player")){
        _isTriggered = true;
     }
    }

    // 트리거 존에 플레이어가 있는 동안 문이 닫히지 않도록 타이머 초기화

    private void OnTriggerStay(Collider other){
        _timer = _autocloseTime;
    }

    // 트리거 존에 플레이어가 나가면 false
    private void OnTriggerExit(Collider other) {
        _isTriggered = false;
    }

    private void OpenDoor(){
        _door.position = Vector3.Lerp(_door.position, new Vector3(_door.position.x,-10.0f,_door.position.z), Time.deltaTime);
    }

    private void CloseDoor(){
        _door.position = Vector3.Lerp(_door.position, _doorClosedPosition, Time.deltaTime);
    }
    private void Update(){
        _timer += Time.deltaTime;
        if(_isTriggered){
            OpenDoor();
        }
        else if(!_isTriggered && _timer >= _autocloseTime){
            CloseDoor();
        }
    }
}

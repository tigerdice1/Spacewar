using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    Transform _door;
    Vector3 _doorClosedPosition;

    float _autocloseTime = 2.0f;
    float _timer;
    bool _isTriggered;
    private void Initalize(){
        _door = transform.Find("Corridor_Door");
        _doorClosedPosition = _door.position;
    }

    private void Start()
    {
        Initalize();

    }

    private void OnTriggerEnter(Collider other)
    {
     if(other.CompareTag("Player")){
        _isTriggered = true;
     }
    }

    private void OnTriggerStay(Collider other)
    {
        _timer = _autocloseTime;
    }

    private void OnTriggerExit(Collider other) {
        _isTriggered = false;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if(_isTriggered){
            _door.position = Vector3.Lerp(_door.position, new Vector3(_door.position.x,-10.0f,_door.position.z), Time.deltaTime);
        }
        else if(!_isTriggered && _timer >= _autocloseTime){
            _door.position = Vector3.Lerp(_door.position, _doorClosedPosition, Time.deltaTime);
        }
    }
}

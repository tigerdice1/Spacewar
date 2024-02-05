using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /* Essential Variables */
    // Specifies the main camera. If not, you may not be able to see the time point properly.
    [SerializeField]
    [Tooltip("카메라")]
    private Camera _cameraObject;
    private bool _isCameraLoaded = false;
    /* Optional Variables */
    [SerializeField]
    [Tooltip("따라갈 물체")]
    private GameObject _followObject;

    [field: SerializeField]
    [Tooltip("카메라와 따라갈 물체 사이의 거리")]
    private Vector3 _offset;

	[Tooltip("카메라가 해당 물체를 따라가게 할지 선택합니다")]
    private bool _isFollowingTarget;

    public Camera GetCamera(){
        return _cameraObject;
    }
    private void Initalize(){
        if(!_cameraObject){
            Debug.Log("Camera is not initialized. The associated functions are disabled. Please Set the Camera. Location : " + gameObject);
            _isCameraLoaded = false;
        }
        else{
            _isCameraLoaded = true;
        }
    }
    // Change the target to follow
    public void SetFollowTarget(GameObject target){
        _followObject = target;
    }
    
    // Move the location of the camera to the target location.
    public void MoveCameraToTarget(){
        if(_isCameraLoaded){
            Vector3 newPosition = new Vector3(
            _followObject.transform.position.x + _offset.x,
            _followObject.transform.position.y + _offset.y,
            _followObject.transform.position.z + _offset.z
            );
            _cameraObject.transform.position = newPosition;
        } 
    }
    // Select whether the camera tracks the target.
    void SetFollowingState(bool isFollowingTarget){
        _isFollowingTarget = isFollowingTarget;
    }

    void UpdateFollowingTarget(){
        if(_isFollowingTarget){
            Vector3 fixedPosition = new Vector3(
            _followObject.transform.position.x + _offset.x,
            _followObject.transform.position.y + _offset.y,
            _followObject.transform.position.z + _offset.z
            );
            _cameraObject.transform.position = Vector3.Lerp(_cameraObject.transform.position, fixedPosition, Time.deltaTime * 8);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Initalize();
        SetFollowingState(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(_isCameraLoaded){
            UpdateFollowingTarget();
        }
    }
}

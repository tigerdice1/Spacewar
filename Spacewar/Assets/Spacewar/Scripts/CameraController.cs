using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("카메라")]
    private Camera _cameraObject;

    [SerializeField]
    [Tooltip("따라갈 물체")]
    private GameObject _followObject;

    [field: SerializeField]
    [Tooltip("카메라와 따라갈 물체 사이의 거리")]
    private Vector3 _offset;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 fixedPosition = new Vector3(
            _followObject.transform.position.x + _offset.x,
            _followObject.transform.position.y + _offset.y,
            _followObject.transform.position.z + _offset.z
        );
        _cameraObject.transform.position = fixedPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 fixedPosition = new Vector3(
            _followObject.transform.position.x + _offset.x,
            _followObject.transform.position.y + _offset.y,
            _followObject.transform.position.z + _offset.z
        );
        _cameraObject.transform.position = Vector3.Lerp(_cameraObject.transform.position, fixedPosition, Time.deltaTime * 8);
    }
}

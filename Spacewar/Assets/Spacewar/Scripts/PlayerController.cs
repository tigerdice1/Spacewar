using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("플레이어가 컨트롤 할 오브젝트")]
    private GameObject _controlObject;
    
    [SerializeField]
    [Tooltip("플레이어가 접촉한 오브젝트")]
    private GameObject _triggerObject;

    [SerializeField]
    [Tooltip("카메라 컨트롤러")]
    private CameraController _cameraController;

    [SerializeField]
    [Tooltip("유저 인터페이스")]
    private GameObject _userInterface;

    [SerializeField]
    [Tooltip("플레이어의 고유 ID")]
    private int _id;    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        Rigidbody rid = _controlObject.GetComponent<Rigidbody>();
        // 태그가 플레이어일 경우
        if(_controlObject.CompareTag("Player")){
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            Vector3 velocity = new Vector3(x, 0, z);
            velocity *= _controlObject.GetComponent<PlayerHuman>().PlayerSpeed;
            rid.velocity = velocity;
        }   
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : MonoBehaviour
{

    [SerializeField]
    [Tooltip("기본 컨트롤 오브젝트")]
    private GameObject _defaultControlObject;
    [SerializeField]
    [Tooltip("플레이어가 컨트롤 할 오브젝트")]
    private GameObject _controlObject;
    
    [SerializeField]
    [Tooltip("플레이어가 접촉한 오브젝트")]
    private GameObject _triggerObject;
    private UIManager _uiManager;

    [SerializeField]
    [Tooltip("플레이어의 고유 ID")]
    private int _ID;

    /* Properties */
    public GameObject TriggerObject{
        set => _triggerObject = value; 
        get => _triggerObject; 
    }

    public GameObject ControlObject{
        set => _controlObject = value;
        get => _controlObject; 
    }

    private void Initailize(){
        // MainUI always visible
        _uiManager = gameObject.AddComponent<UIManager>();
        _uiManager.SetPlayerUIState(true);
    }
    
    private void CheckOnTriggerExit(){
        if(_triggerObject == null && _uiManager.GetUIActivated()){
            _uiManager.ReleaseUI();
        }
        if(_triggerObject == null && !_controlObject.CompareTag("Player")){
            _controlObject = _defaultControlObject;
        }
    }
    private RaycastHit GetCursorRaycastResult(){
        Ray ray = gameObject.GetComponent<CameraController>().GetCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitResult;
        if(!Physics.Raycast(ray, out hitResult)){
            
        }
        return hitResult;
        
    }
    private void LookCursor(float maxRotationSpeed){
        RaycastHit hitResult = GetCursorRaycastResult();
            Vector3 mouseDir = new Vector3(hitResult.point.x, _controlObject.transform.position.y, hitResult.point.z) - _controlObject.transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(mouseDir);
            float currentRotationSpeed = Quaternion.Angle(_controlObject.transform.rotation, lookRotation) / Time.deltaTime;
            float limitedRotationSpeed = Mathf.Clamp(currentRotationSpeed, 0f, maxRotationSpeed);
            _controlObject.transform.rotation = Quaternion.Slerp(_controlObject.transform.rotation, lookRotation,limitedRotationSpeed * Time.deltaTime);
    }

    private void MovePlayer(){
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        _controlObject.transform.Translate(new Vector3(moveX, 0, moveZ)* Time.deltaTime * _controlObject.GetComponent<PlayerInventoryTest>().PlayerSpeed, Space.World);
    }

    private void MoveShip(){
        Rigidbody rid = _controlObject.GetComponent<Rigidbody>();
        if (Input.GetKey(KeyCode.W)){
            _controlObject.GetComponent<MainShip>().IsReverseThrusterActive = false;
            rid.AddRelativeForce(Vector3.forward * 10.0f);
        }
        if (Input.GetKey(KeyCode.A)){
            _controlObject.GetComponent<MainShip>().IsReverseThrusterActive = false;
            rid.AddRelativeForce(Vector3.right * 10.0f);
        }
        if (Input.GetKey(KeyCode.S)){
            _controlObject.GetComponent<MainShip>().IsReverseThrusterActive = false;
            rid.AddRelativeForce(Vector3.back * 10.0f);
        }
        if (Input.GetKey(KeyCode.D)){
            _controlObject.GetComponent<MainShip>().IsReverseThrusterActive = false;
            rid.AddRelativeForce(Vector3.left * 10.0f);
        }
        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W)){
            _controlObject.GetComponent<MainShip>().IsReverseThrusterActive = true;
        } 
        float MaxVelocity = _controlObject.GetComponent<MainShip>().Speed;
        if(rid.velocity.x > MaxVelocity){
            rid.velocity = new Vector3(MaxVelocity, rid.velocity.y, rid.velocity.z);
        }
        if(rid.velocity.x < (MaxVelocity * - 1)){
            rid.velocity = new Vector3(MaxVelocity * -1, rid.velocity.y, rid.velocity.z);
        }
        if(rid.velocity.y > MaxVelocity){
            rid.velocity = new Vector3(rid.velocity.x, MaxVelocity, rid.velocity.z);
        }
        if(rid.velocity.y < (MaxVelocity * - 1)){
            rid.velocity = new Vector3(rid.velocity.x, MaxVelocity  * -1, rid.velocity.z);
        } 
        if(rid.velocity.z > MaxVelocity){
            rid.velocity = new Vector3(rid.velocity.x, rid.velocity.y, MaxVelocity);
        }
        if(rid.velocity.z < (MaxVelocity * - 1)){
            rid.velocity = new Vector3(rid.velocity.x, rid.velocity.y, MaxVelocity  * -1);
        }
    }
    private void CheckKeyInput(){
        if(Input.GetKeyDown(KeyCode.E) && _triggerObject != null){
            if(_triggerObject.GetComponent<Console_PowerGenerator>() && !_uiManager.GetUIActivated()){
                _uiManager.SetUIState(_triggerObject.GetComponent<Console_PowerGenerator>().GetUI(), true);
            }
            else if(_triggerObject.GetComponent<Console_PowerGenerator>() && _uiManager.GetUIActivated()){
                _uiManager.SetUIState(_triggerObject.GetComponent<Console_PowerGenerator>().GetUI(), false);
            }
            if(_triggerObject.GetComponent<Console_ControlPanel>() && !_uiManager.GetUIActivated()){
                //_uiManager.SetUIState(_triggerObject.GetComponent<Console_ControlPanel>().GetUI(), true);
                _triggerObject.GetComponent<Console_ControlPanel>().SwapContorlObject();
            }
            else if(_triggerObject.GetComponent<Console_ControlPanel>() && _uiManager.GetUIActivated()){
                //wwww_uiManager.SetUIState(_triggerObject.GetComponent<Console_ControlPanel>().GetUI(), false);
                _triggerObject.GetComponent<Console_ControlPanel>().SwapContorlObject();
            }
        }
    }
    // Start is called before the first frame update
    private void Start(){
        Initailize();

    }
    // Update is called once per frame
    void Update(){
        CheckKeyInput();
        CheckOnTriggerExit();
    }

    void FixedUpdate(){
        
        // 태그가 플레이어일 경우
        if(_controlObject.CompareTag("Player")){
            LookCursor(_controlObject.GetComponent<PlayerInventoryTest>().PlayerRotationSpeed);
            MovePlayer();
        }
        else if(_controlObject.CompareTag("MainShip")){
            LookCursor(_controlObject.GetComponent<MainShip>().RotationSpeed);
            MoveShip();
        }
    }
}
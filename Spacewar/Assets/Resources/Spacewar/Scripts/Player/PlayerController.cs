using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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

    /* Properties */
    public GameObject DefaultControlObject{
        set => _defaultControlObject = value; 
        get => _defaultControlObject; 
    }
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
        _uiManager = this.gameObject.AddComponent<UIManager>();
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
        Ray ray = this.GetComponent<CameraController>().GetCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitResult;
        if(!Physics.Raycast(ray, out hitResult)){
            
        }
        return hitResult;
        
    }

    private void MouseClickEvent(){
        if(Input.GetMouseButtonDown(0)){
            if(_controlObject.CompareTag("MainShip")){
                for(int i = 0; _controlObject.GetComponent<MainShip>().GetLoadedMissileRooms.Count < 0; i++){
                    _controlObject.GetComponent<MainShip>().GetLoadedMissileRooms[i].LaunchMissile();
                }
            }
            if(_controlObject.CompareTag("Turret")){
                _controlObject.GetComponent<Turret>().IsFire = true;
            }
        }
        if(Input.GetMouseButtonUp(0)){
            if(_controlObject.CompareTag("Turret")){
                _controlObject.GetComponent<Turret>().IsFire = false;
            }
        }
    }

    private void LookCursorBySlerp(float maxRotationSpeed){
        RaycastHit hitResult = GetCursorRaycastResult();
            Vector3 mouseDir = new Vector3(hitResult.point.x, _controlObject.transform.position.y, hitResult.point.z) - _controlObject.transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(mouseDir);
            float currentRotationSpeed = Quaternion.Angle(_controlObject.transform.rotation, lookRotation) / Time.deltaTime;
            float limitedRotationSpeed = Mathf.Clamp(currentRotationSpeed, 0f, maxRotationSpeed);
            _controlObject.transform.rotation = Quaternion.Slerp(_controlObject.transform.rotation, lookRotation,limitedRotationSpeed * Time.deltaTime);
           //_controlObject.transform.rotation = Quaternion.RotateTowards(_controlObject.transform.rotation, lookRotation, limitedRotationSpeed * Time.deltaTime);
        /*
            _controlObject.transform.forward = mouseDir;
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 lookDir = worldPos - _controlObject.transform.position;
        Debug.Log(worldPos);
        lookDir.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(lookDir);
        _controlObject.transform.rotation = lookRotation;
        */
    }

    private void LookCursor(float maxRotationSpeed){
        RaycastHit hitResult = GetCursorRaycastResult();
            Vector3 mouseDir = new Vector3(hitResult.point.x, _controlObject.transform.position.y, hitResult.point.z) - _controlObject.transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(mouseDir);
            float currentRotationSpeed = Quaternion.Angle(_controlObject.transform.rotation, lookRotation) / Time.deltaTime;
            float limitedRotationSpeed = Mathf.Clamp(currentRotationSpeed, 0f, maxRotationSpeed);
            _controlObject.transform.rotation = Quaternion.Lerp(_controlObject.transform.rotation, lookRotation,limitedRotationSpeed * Time.deltaTime);
    }

    private void MovePlayer(){
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        _controlObject.transform.Translate(new Vector3(moveX, 0, moveZ)* Time.deltaTime * _controlObject.GetComponent<Human>().PlayerSpeed, Space.World);
    }

    private void MoveShip(){
        Rigidbody rid = _controlObject.GetComponent<Rigidbody>();
        float spd = _controlObject.GetComponent<MainShip>().Speed;
        
        if (Input.GetKey(KeyCode.W)){
            rid.AddRelativeForce(Vector3.forward * spd);
        }
        if (Input.GetKey(KeyCode.A)){
            rid.AddRelativeForce(Vector3.left * spd);
        }
        if (Input.GetKey(KeyCode.S)){
            rid.AddRelativeForce(Vector3.back * spd);
        }
        if (Input.GetKey(KeyCode.D)){
            rid.AddRelativeForce(Vector3.right * spd);
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
                _triggerObject.GetComponent<Console_ControlPanel>().SwapContorlObject();
            }
            else if(_triggerObject.GetComponent<Console_ControlPanel>() && _uiManager.GetUIActivated()){
                _triggerObject.GetComponent<Console_ControlPanel>().SwapContorlObject();
            }
        }
        if(Input.GetKeyDown(KeyCode.T)){

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
        MouseClickEvent();
    }

    void FixedUpdate(){
        // 태그가 플레이어일 경우
        if(_controlObject.CompareTag("Player")){
            MovePlayer();
            LookCursorBySlerp(_controlObject.GetComponent<Human>().PlayerRotationSpeed);
        }
        else if(_controlObject.CompareTag("MainShip")){
            MoveShip();
            LookCursorBySlerp(_controlObject.GetComponent<MainShip>().ShipRotationSpeed);
        }
        else if(_controlObject.CompareTag("Turret")){
            LookCursor(_controlObject.GetComponent<Turret>().RotationSpeed);
        }
    }
}
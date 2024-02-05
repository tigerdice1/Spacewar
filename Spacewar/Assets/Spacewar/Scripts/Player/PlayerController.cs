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

    [SerializeField]
    [Tooltip("플레이어의 고유 ID")]
    private int _ID;

    private float _mouseX;
    private float _mouseY;
    private float _sensitivity = 50.0f;

    /* Properties */
    public GameObject TriggerObject{
        get { return _triggerObject; }
        set { _triggerObject = value; }
    }

    public GameObject ControlObject{
        get {return _controlObject; }
        set {_controlObject = value;}
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

    private void LookCursor(){
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 lookDir = worldPos - _controlObject.transform.position;
        lookDir.y = 0; // y 값 고정
        Quaternion rotation = Quaternion.LookRotation(lookDir);
        // 회전 적용
        _controlObject.transform.rotation = rotation;
    }

    private void MovePlayer(){

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
        Rigidbody rid = _controlObject.GetComponent<Rigidbody>();
        // 태그가 플레이어일 경우
        if(_controlObject.CompareTag("Player")){
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            
            
            Vector3 velocity = new Vector3(x, 0, z);
            velocity *= _controlObject.GetComponent<PlayerHuman>().PlayerSpeed;
            rid.velocity = velocity;
            LookCursor();
        }
        else if(_controlObject.CompareTag("MainShip")){
            if (Input.GetKey(KeyCode.W)){
                _controlObject.GetComponent<MainShip>().IsReverseThrusterActive = false;
                rid.AddRelativeForce(Vector3.forward * 10.0f);
            }
            if (Input.GetKey(KeyCode.A)){
                _controlObject.GetComponent<MainShip>().IsReverseThrusterActive = false;
                rid.AddRelativeTorque(Vector3.down * 10.0f);
            }
            if (Input.GetKey(KeyCode.S)){
                _controlObject.GetComponent<MainShip>().IsReverseThrusterActive = false;
                rid.AddRelativeForce(Vector3.back * 10.0f);
            }
            if (Input.GetKey(KeyCode.D)){
                _controlObject.GetComponent<MainShip>().IsReverseThrusterActive = false;
                rid.AddRelativeTorque(Vector3.up * 10.0f);
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
    }
}
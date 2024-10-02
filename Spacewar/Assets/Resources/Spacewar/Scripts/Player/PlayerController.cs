using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks
{

    [SerializeField]
    [Tooltip("기본 컨트롤 오브젝트.")]
    private GameObject _defaultControlObject;
    [SerializeField]
    [Tooltip("플레이어가 컨트롤 할 오브젝트")]
    private GameObject _controlObject;
    private Rigidbody _controlRigidBody;
    
    [SerializeField]
    [Tooltip("플레이어가 접촉한 오브젝트")]
    private GameObject _triggerObject;
    private UIManager _uiManager;
    private CameraController _cameraController;

    public bool _isMine;

    public bool _isTeam1;

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
        set{
            _controlObject = value;
            _controlRigidBody = _controlObject.GetComponent<Rigidbody>();
        }
        get => _controlObject; 
    }

    private void Initialize(){
        // MainUI always visible
        _isMine = GetComponent<PhotonView>().IsMine;
        _uiManager = this.gameObject.AddComponent<UIManager>();
        _uiManager.SetPlayerUIState(true); 
        if(_controlObject == null){
            _controlObject = _defaultControlObject;
            var playerBase = _controlObject.GetComponent<PlayerBase>();
            if (playerBase != null){
                playerBase.PlayerController = this;
            }
        }
        _cameraController = GetComponent<CameraController>();
        if(GameManager.Instance().IsDebugMode){
            _isMine = true;
        }
    }
    
    private void CheckOnTriggerExit(){
        if(_triggerObject == null && _uiManager.GetUIActivated()){
            _uiManager.ReleaseUI();
        }
        if(_triggerObject == null && !_controlObject.CompareTag("Player")){
            _controlObject = _defaultControlObject;
        }
    }

    public RaycastHit GetCursorRaycastResult(){
        Ray ray = _cameraController.GetCamera().ScreenPointToRay(Input.mousePosition);
        RaycastHit hitResult;
        if(!Physics.Raycast(ray, out hitResult)){
            
        }
        return hitResult;
    }

    private void MouseClickEvent(){
        if (Input.GetMouseButtonDown(0)){
            var controllable = _controlObject.GetComponent<IControllable>();
            controllable?.HandleMouseClick(this);
        }

        if (Input.GetMouseButtonUp(0)){
            var turret = _controlObject.GetComponent<Turret>();
            if (turret != null){
                turret.IsFiring = false;
            }
        }
        /*
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
    */
    }
    public void LookAtCursor(float maxRotationSpeed, bool useSlerp){
        var hitResult = GetCursorRaycastResult();
        Vector3 direction = new Vector3(hitResult.point.x, _controlObject.transform.position.y, hitResult.point.z) - _controlObject.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        if (useSlerp){
            _controlObject.transform.rotation = Quaternion.Slerp(_controlObject.transform.rotation, lookRotation, maxRotationSpeed * Time.deltaTime);
        }
        else{
            _controlObject.transform.rotation = Quaternion.Lerp(_controlObject.transform.rotation, lookRotation, maxRotationSpeed * Time.deltaTime);
        }
    }

    private void CheckKeyInput(){
        if (Input.GetKeyDown(KeyCode.E) && _triggerObject != null){
            var powerGenerator = _triggerObject.GetComponent<Console_PowerGenerator>();
            var controlPanel = _triggerObject.GetComponent<Console_ControlPanel>();

            if (powerGenerator != null){
                bool uiActivated = _uiManager.GetUIActivated();
                _uiManager.SetUIState(powerGenerator.GetUI(), !uiActivated);
            }
            else if (controlPanel != null){
                controlPanel.SwapControlObject(this);
                bool uiActivated = _uiManager.GetUIActivated();
                _uiManager.SetUIState(controlPanel.GetUI(), !uiActivated);
            }
        }
    }

/*
    private void MoveShip(){
        _controlRigidBody = _controlObject.GetComponent<Rigidbody>();
        float spd = _controlObject.GetComponent<MainShip>().Speed;
        
        if (Input.GetKey(KeyCode.W)){
            _controlRigidBody.AddRelativeForce(Vector3.forward * spd);
        }
        if (Input.GetKey(KeyCode.A)){
            _controlRigidBody.AddRelativeForce(Vector3.left * spd);
        }
        if (Input.GetKey(KeyCode.S)){
            _controlRigidBody.AddRelativeForce(Vector3.back * spd);
        }
        if (Input.GetKey(KeyCode.D)){
            _controlRigidBody.AddRelativeForce(Vector3.right * spd);
        }
        float MaxVelocity = _controlObject.GetComponent<MainShip>().Speed;
        if(_controlRigidBody.velocity.x > MaxVelocity){
            _controlRigidBody.velocity = new Vector3(MaxVelocity, _controlRigidBody.velocity.y, _controlRigidBody.velocity.z);
        }
        if(_controlRigidBody.velocity.x < (MaxVelocity * - 1)){
            _controlRigidBody.velocity = new Vector3(MaxVelocity * -1, _controlRigidBody.velocity.y, _controlRigidBody.velocity.z);
        }
        if(_controlRigidBody.velocity.y > MaxVelocity){
            _controlRigidBody.velocity = new Vector3(_controlRigidBody.velocity.x, MaxVelocity, _controlRigidBody.velocity.z);
        }
        if(_controlRigidBody.velocity.y < (MaxVelocity * - 1)){
            _controlRigidBody.velocity = new Vector3(_controlRigidBody.velocity.x, MaxVelocity  * -1, _controlRigidBody.velocity.z);
        } 
        if(_controlRigidBody.velocity.z > MaxVelocity){
            _controlRigidBody.velocity = new Vector3(_controlRigidBody.velocity.x, _controlRigidBody.velocity.y, MaxVelocity);
        }
        if(_controlRigidBody.velocity.z < (MaxVelocity * - 1)){
            _controlRigidBody.velocity = new Vector3(_controlRigidBody.velocity.x, _controlRigidBody.velocity.y, MaxVelocity  * -1);
        }
    }
*/

    
    // Start is called before the first frame update
    private void Start(){
        Initialize();

    }
    // Update is called once per frame
    void Update(){
        if(_isMine){
            CheckKeyInput();
            CheckOnTriggerExit();
            MouseClickEvent();
            
        }
    }

    void FixedUpdate(){
        if (_isMine){
            var controllable = _controlObject.GetComponent<IControllable>();
            controllable?.Move(this);
            controllable?.Look(this, controllable.RotationSpeed, true);
            controllable?.UpdateAnimation(this);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks
{ 
    public GameObject PlayerUI;
    public bool IsMine;
    public bool IsTeam1;
    [Tooltip("플레이어가 접촉한 오브젝트")]
    public GameObject TriggerObject;

    [SerializeField]
    [Tooltip("기본 컨트롤 오브젝트.")]
    private GameObject _defaultControlObject;
    [SerializeField]
    [Tooltip("플레이어가 컨트롤 할 오브젝트")]
    private GameObject _controlObject;
    private Rigidbody _controlRigidBody;
    private int _inventoryIndex = 0;
    private UIManager _uiManager;
    private CameraController _cameraController;

    /* Properties */
    public GameObject DefaultControlObject{
        set => _defaultControlObject = value; 
        get => _defaultControlObject; 
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
        IsMine = GetComponent<PhotonView>().IsMine;
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
            IsMine = true;
        }
    }
    
    private void CheckOnTriggerExit(){
        if(TriggerObject == null && _uiManager.GetUIActivated()){
            _uiManager.ReleaseUI();
        }
        if(TriggerObject == null && !_controlObject.CompareTag("Player")){
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
            _uiManager.PlayerUI.gameObject.GetComponent<UI_Player>().GetClickedUIElement();
        }

        if (Input.GetMouseButtonUp(0)){
            var turret = _controlObject.GetComponent<Turret>();
            if (turret != null){
                turret.IsFiring = false;
            }
        }
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
        if (Input.GetKeyDown(KeyCode.E) && TriggerObject != null){
            var powerGenerator = TriggerObject.GetComponent<PowerGenerator>();
            var controlPanel = TriggerObject.GetComponent<ControlPanel>();
            var item = TriggerObject.GetComponent<PickableItem>();

            if (powerGenerator != null){
                bool uiActivated = _uiManager.GetUIActivated();
                _uiManager.SetUIState(powerGenerator.ConsoleUI, !uiActivated);
            }
            else if (controlPanel != null){
                if(controlPanel.SwapControlObject(this)){
                    bool uiActivated = _uiManager.GetUIActivated();
                    _uiManager.SetUIState(controlPanel.ConsoleUI, !uiActivated);
                }
            }
            else if (item != null){
                item.PickupItem(_inventoryIndex);
            }
        }
        for (int i = 0; i <= 9; i++){
            KeyCode keyCode = (KeyCode)((int)KeyCode.Alpha0 + i);
            if (Input.GetKeyDown(keyCode)){
                // 0 키를 누르면 pickerNumber는 9(인벤토리의 마지막 슬롯), 그렇지 않으면 그대로
                int pickerNumber = (i == 0) ? 10 : i;

                // 인벤토리 인덱스는 0부터 시작하므로 pickerNumber를 -1 해서 맞춤
                _inventoryIndex = pickerNumber - 1;

                // UI 상에서 인벤토리 선택 표시를 업데이트
                _uiManager.MoveInventoryPicker(pickerNumber);

                // 인덱스가 범위를 벗어나지 않도록 안전 장치
                if (_inventoryIndex >= 0 && _inventoryIndex < 9){
                    // 해당 슬롯에 아이템이 있는지 확인 후 장착 애니메이션 실행
                    var item = _controlObject.GetComponent<PlayerBase>().Inventory[_inventoryIndex];
                    if (item != null && item.ItemType != 0) // 아이템이 존재하고 타입이 0이 아닐 경우에만 장착 애니메이션
                    {
                        _controlObject.GetComponent<PlayerBase>().EquipItemAnimation(_inventoryIndex);
                    }
                }
        else{
            Debug.LogWarning("Invalid inventory index selected.");
        }
    }

        }
        if (Input.GetKeyDown(KeyCode.F)){
            var player = _controlObject.GetComponent<PlayerBase>();
            if(player != null){
                ItemManager.Instance().UseItem(_inventoryIndex, TriggerObject, player);
            }
        }
        if (Input.GetKeyDown(KeyCode.G)){
            var player = _controlObject.GetComponent<PlayerBase>();
            if(player != null){
                player.DropItemAnimation(_inventoryIndex);
                ItemManager.Instance().DropItem(_inventoryIndex, player);
            }
        }
    }  
    // Start is called before the first frame update
    private void Start(){
        Initialize();

    }
    // Update is called once per frame
    void Update(){
        if(IsMine){
            CheckKeyInput();
            CheckOnTriggerExit();
            MouseClickEvent();
            
        }
    }

    void FixedUpdate(){
        if (IsMine){
            var controllable = _controlObject.GetComponent<IControllable>();
            controllable?.Move(this);
            controllable?.Look(this, controllable.RotationSpeed, true);
            controllable?.UpdateAnimation(this);
        }
    }
}
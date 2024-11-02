using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks
{ 
    #region Public Variables
    public bool IsMine;
    public bool Team1;
    [Tooltip("플레이어가 접촉한 오브젝트")]
    public GameObject TriggerObject;
    public PickableItem TriggerItem;
    public UIManager UIController;
    
    #endregion Public Variables

    #region Private Variables
    [SerializeField]
    [Tooltip("기본 컨트롤 오브젝트.")]
    private GameObject _defaultControlObject;
    [SerializeField]
    [Tooltip("플레이어가 컨트롤 할 오브젝트")]
    private GameObject _controlObject;
    private Rigidbody _controlRigidBody;
    private int _inventoryIndex = 0;
    private CameraController _cameraController;
    #endregion Private Variables
    
    #region Public Properties
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
    #endregion Public Properties

    #region Public Methods
    public RaycastHit GetCursorRaycastResult(){
        Ray ray = _cameraController.GetCamera().ScreenPointToRay(Input.mousePosition);
        RaycastHit hitResult;
        if(!Physics.Raycast(ray, out hitResult)){
            
        }
        return hitResult;
    }
    public void LookAtCursor(float maxRotationSpeed, bool useSlerp){
        var hitResult = GetCursorRaycastResult();
        Vector3 direction = new Vector3(hitResult.point.x, _controlObject.transform.position.y, hitResult.point.z) - _controlObject.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        _controlObject.transform.rotation = useSlerp ? Quaternion.Slerp(_controlObject.transform.rotation, lookRotation, maxRotationSpeed * Time.deltaTime) :
        Quaternion.Lerp(_controlObject.transform.rotation, lookRotation, maxRotationSpeed * Time.deltaTime);
    }
    #endregion Public Methods

    #region Private Methods
    private void Initialize(){
        IsMine = GetComponent<PhotonView>().IsMine;
        
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
        PlayerBase.OnObjectEnterTrigger += HandleTriggerEnter;
        PlayerBase.OnObjectStayTrigger += HandleTriggerStay;
        PlayerBase.OnObjectExitTrigger += HandleTriggerExit;
    }

    public void HandleTriggerEnter(Collider other){
        Debug.Log("HandleTriggerEnter");
        var item = other.GetComponent<PickableItem>();
        // 들어간 트리거가 아이템일 때
        if(item != null && !item.IsAttached){
            TriggerItem = item;
            return;
        }
        
        var console = other.GetComponent<FixableObjects>();
        // 들어간 트리거가 엑세스 가능한 FixableObjects 일 때.
        if(console != null){
            TriggerObject = other.gameObject;
            // UI 가 있다면 할당
            if(console.ConsoleUI != null){
                UIController.SetOtherUI(console.ConsoleUI);
            }
        }
    }
    public void HandleTriggerStay(Collider other){
        Debug.Log("HandleTriggerStay");
        
    }
    public void HandleTriggerExit(Collider other){
        Debug.Log("HandleTriggerExit");
        var item = other.GetComponent<PickableItem>();
        // 나온 트리거가 아이템일 때
        if(item != null){
            TriggerItem = null;
            return;
        }

        var controlPanel = other.GetComponent<FixableObjects>();
        // 나온 트리거가 콘솔일 떄
        if(controlPanel != null){
            // 플레이어캐릭터 말고 다른 오브젝트를 조종중이었다면 오브젝트를 기본 컨트롤 오브젝트로 변경
            if(!_controlObject.CompareTag("Player")){
                _controlObject = _defaultControlObject;
            }
            // 다른 오브젝트 UI가 존재하고 UI 가 표시되는 중이라면 UI를 숨김
            if(controlPanel.ConsoleUI != null && UIController.IsOtherUIVisible){
                UIController.HideObjectUI();
                UIController.SetOtherUI(null);
            }
        }
        TriggerObject = null;
    }

    private void MouseClickEvent(){
        if (Input.GetMouseButtonDown(0)){
            var controllable = _controlObject.GetComponent<IControllable>();
            controllable?.HandleMouseClick(this);
            UIController.GetPlayerUI().gameObject.GetComponent<UI_Player>().GetClickedUIElement();
        }

        if (Input.GetMouseButtonUp(0)){
            var turret = _controlObject.GetComponent<Turret>();
            if (turret != null){
                turret.IsFiring = false;
            }
        }
    }

    private void CheckKeyInput(){
        if (Input.GetKeyDown(KeyCode.E)){
            if(TriggerObject != null){
                // 오브젝트 판정인 트리거일 시 
                var powerGenerator = TriggerObject.GetComponent<PowerGenerator>();
                var controlPanel = TriggerObject.GetComponent<ControlPanel>();

                if (powerGenerator != null){
                bool IsOtherUIVisible = UIController.IsOtherUIVisible;
                    UIController.SetUIVisible(!IsOtherUIVisible);
                }
                else if (controlPanel != null){
                    if(controlPanel.SwapControlObject(this)){
                        if(controlPanel.ConsoleUI != null){
                            bool IsOtherUIVisible = UIController.IsOtherUIVisible;
                            UIController.SetUIVisible(!IsOtherUIVisible);
                        }
                    }
                }
            }
            if(TriggerItem != null){
                // 아이템 판정인 트리거일 시
                var item = TriggerItem.GetComponent<PickableItem>();
                if (item != null){
                    item.PickupItem(ControlObject.GetComponent<PlayerBase>() ,_inventoryIndex);
                }
            }
        }
        for (int i = 0; i <= 9; i++){
            KeyCode keyCode = (KeyCode)((int)KeyCode.Alpha0 + i);
            if (Input.GetKeyDown(keyCode)){
                // 0 키를 누르면 pickerNumber는 9(인벤토리의 마지막 슬롯), 그렇지 않으면 그대로
                int pickerNumber = (i == 0) ? 10 : i;

                // UI 상에서 인벤토리 선택 표시를 업데이트
                UIController.GetPlayerUI().gameObject.GetComponent<UI_Player>().MoveInventoryPicker(pickerNumber);

                _inventoryIndex = (pickerNumber + 9) % 10;
                _controlObject.GetComponent<PlayerBase>().EquipItemAnimation(_inventoryIndex);
                
            }
        }
        if (Input.GetKeyDown(KeyCode.F)){
            var player = _controlObject.GetComponent<PlayerBase>();
            // 현재 플레이어를 조종중 && 현재 활성화된 인벤토리 인덱스가 비어있지 않을 때
            if(player != null && _controlObject.GetComponent<PlayerBase>().Inventory[_inventoryIndex].ID != 0){
                var usableitem = _controlObject.GetComponent<PlayerBase>().Inventory[_inventoryIndex];
                var foundItem = ItemManager.Instance().FindItemByID(usableitem.ID);
                if(foundItem!= null){
                    foundItem.GetComponent<PickableItem>().UseItem(_controlObject.transform, TriggerObject.transform);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.G)){
            var player = _controlObject.GetComponent<PlayerBase>();
            // 현재 플레이어를 조종중 && 현재 활성화된 인벤토리 인덱스가 비어있지 않을 때
            if(player != null && _controlObject.GetComponent<PlayerBase>().Inventory[_inventoryIndex].ID != 0){
                var itemWillDrop = _controlObject.GetComponent<PlayerBase>().Inventory[_inventoryIndex];
                var foundItem = ItemManager.Instance().FindItemByID(itemWillDrop.ID);
                player.DropItemAnimation();
                itemWillDrop.ClearItemData();
                if(foundItem!= null){
                    foundItem.GetComponent<PickableItem>().DropItem(_controlObject.transform);
                }
            }
        }
    }
    #endregion Private Methods

    private void Awake(){
        UIController = gameObject.AddComponent<UIManager>();
        UIController.ShowPlayerUI(true); 
    }
    // Start is called before the first frame update
    private void Start(){
        Initialize();

    }
    // Update is called once per frame
    void Update(){
        if(IsMine){
            CheckKeyInput();
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
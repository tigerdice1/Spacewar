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
    private UIManager _uiManager;

    [SerializeField]
    [Tooltip("플레이어의 고유 ID")]

    /* Properties */
    public GameObject TriggerObject{
        get { return _triggerObject; }
        set { _triggerObject = value; }
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
    }
    private void CheckKeyInput(){
        if(Input.GetKeyDown(KeyCode.E) && _triggerObject != null){
            if(_triggerObject.GetComponent<PowerGenerator>() && !_uiManager.GetUIActivated()){
                _uiManager.SetUIState(_triggerObject.GetComponent<PowerGenerator>().GetUI(), true);
            }
            else if(_triggerObject.GetComponent<PowerGenerator>() && _uiManager.GetUIActivated()){
                _uiManager.SetUIState(_triggerObject.GetComponent<PowerGenerator>().GetUI(), false);
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
        }   
    }
}
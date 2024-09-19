using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleBase : MonoBehaviour
{
    [SerializeField]
    [Tooltip("콘솔의 소유 함선을 지정합니다. 해당 변수를 통해서 스크립트를 통해 함선 정보에 도달합니다.")]
    protected MainShip _ownerShip;
    [SerializeField]
    [Tooltip("콘솔을 사용할 경우 조종하게 될 오브젝트를 지정합니다. 조종할 오브젝트가 따로 없고 UI만 띄우려고 한다면 따로 지정하지 않아도 됩니다.")]
    protected GameObject _objectToControl;

    [Tooltip("콘솔을 사용중인 플레이어를 지정합니다. _triggeredControllers 에 할당된 플레이어가 사용버튼을 누르면 자동으로 할당됩니다.")]
    protected List<PlayerBase> _handlingPlayers;

    [Tooltip("콘솔의 사용가능 범위 안에 들어와있는 PlayerController를 토글합니다. Trigger를 통해 자동으로 지정됩니다. Trigger 범위 안에 PlayerController 가 없는 경우 Null 을 반환하니 이 점에 유의해야합니다.")]
    protected List<PlayerController> _triggeredControllers;
    
    // 콘솔을 누군가 사용중인지 판별하는 Bool 변수입니다. 누군가 콘솔을 사용중일 경우 false, 사용중이지 않을경우 true 를 반환합니다.
    protected bool _isInteractive;

    // 콘솔을 혼자서만 사용할 수 있는지 판별하는 Bool 변수입니다. true 로 설정시 한 번에 한 명씩만 접근할 수 있으며, false 로 설정시 동시에 여러명이 접근할 수 있습니다.
    protected bool _soloUseOnly;

    [SerializeField]
    [Tooltip("콘솔에 접근하면 사용할 UI 입니다. 없어도 작동에 이상은 없지만 많이 불편할 수도 있습니다.")]
    protected GameObject _consoleUI;

    protected virtual void Initalize(){
        _handlingPlayers = new List<PlayerBase>();
        _triggeredControllers = new List<PlayerController>();
        _isInteractive = true;
        _soloUseOnly = false;
    }

    protected virtual void OnDebugMode(){
        if(!_ownerShip) Debug.Log("OwnerShip is not initialized. The associated functions are disabled. Please Set the OwnerShip. Location : " + gameObject);
        if(!_objectToControl) Debug.Log("ObjectToControl is not initialized. The associated functions are disabled. Please Set the OwnerShip. Location : " + gameObject);
        if(!_consoleUI) Debug.Log("ConsoleUI is not initialized. The associated functions are disabled. Location : " + gameObject);
        if(!this.GetComponent<BoxCollider>().isTrigger) Debug.Log("BoxCollider's trigger is missing. Please add it in the editor. Location : " + gameObject);
        
    }
    protected void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            _triggeredControllers.Add(other.GetComponent<PlayerBase>().PlayerController);
            other.GetComponent<PlayerBase>().PlayerController.TriggerObject = this.gameObject;
        }
    }
    protected void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")){
            _triggeredControllers.Remove(other.GetComponent<PlayerBase>().PlayerController);
            other.GetComponent<PlayerBase>().PlayerController.TriggerObject = null;
        }
    }
    public GameObject GetUI(){
        if(_consoleUI) return _consoleUI;
        else return null;
    }

    // Start is called before the first frame update
    protected virtual void Start(){
        if(GameManager.Instance().IsDebugMode){
            OnDebugMode();
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}

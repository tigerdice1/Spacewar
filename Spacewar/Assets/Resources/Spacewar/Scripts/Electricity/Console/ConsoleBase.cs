using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleBase : MonoBehaviour
{
    [SerializeField]
    [Tooltip("해당 오브젝트의 소유함선을 지정합니다.")]
    protected MainShip _ownerShip;
    [SerializeField]
    [Tooltip("해당 오브젝트에 상호작용 시 컨트롤 할 오브젝트를 지정합니다. 따로 없을경우 지정하지 않아도 됩니다.")]
    protected GameObject _objectToControl;

    [Tooltip("해당 오브젝트를 사용중인 오브젝트입니다. 자동으로 지정됩니다.")]
    protected List<PlayerBase> _handlingPlayers;

    [Tooltip("접근 가능구역에 들어온 오브젝트입니다. 트리거 안에 오브젝트가 들어오면 자동으로 지정됩니다.")]
    protected List<PlayerController> _triggeredController;
    // 해당 콘솔에 접근 가능한지 판별해주는 변수. 보통의 경우 누군가 사용중일 때 다른 플레이어가 상호작용하지 못하도록 합니다.
    protected bool _isInteractive = true;
    protected bool _soloUseOnly = false;
    [SerializeField]
    [Tooltip("콘솔 접근 시 사용할 UI를 지정합니다. 따로 없을경우 지정하지 않아도 됩니다.")]
    protected GameObject _consoleUI;

    protected virtual void Initalize(){
        _handlingPlayers = new List<PlayerBase>();
        _triggeredController = new List<PlayerController>();
    }

    protected virtual void OnDebugMode(){
        if(!_ownerShip) Debug.Log("OwnerShip is not initialized. The associated functions are disabled. Please Set the OwnerShip. Location : " + gameObject);
        if(!_objectToControl) Debug.Log("ObjectToControl is not initialized. The associated functions are disabled. Please Set the OwnerShip. Location : " + gameObject);
        if(!_consoleUI) Debug.Log("ConsoleUI is not initialized. The associated functions are disabled. Location : " + gameObject);
        if(!this.GetComponent<BoxCollider>().isTrigger) Debug.Log("BoxCollider's trigger is missing. Please add it in the editor. Location : " + gameObject);
        
    }
    protected void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            _triggeredController.Add(other.GetComponent<PlayerBase>().PlayerController);
            other.GetComponent<PlayerBase>().PlayerController.TriggerObject = this.gameObject;
        }
    }
    protected void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")){
            _triggeredController.Remove(other.GetComponent<PlayerBase>().PlayerController);
            other.GetComponent<PlayerBase>().PlayerController.TriggerObject = null;
        }
    }
    public GameObject GetUI(){
        if(_consoleUI) return _consoleUI;
        else return null;
    }

    // Start is called before the first frame update
    protected virtual void Start(){
        if(GameManager.Instance().IsDebugMode()){
            OnDebugMode();
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}

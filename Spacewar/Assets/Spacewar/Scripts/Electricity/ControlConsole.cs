using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlConsole : MonoBehaviour
{
    [SerializeField]
    private GameObject _ownerShip;
    private bool _isOwnerShipLoaded;
    // 컨트롤 할 오브젝트
    [SerializeField]
    private GameObject _objectToControl;
    // 현재 엑세스 중인 오브젝트
    private GameObject _handlingObject;
    private bool _isObjectToControlLoaded;
    private PlayerController _triggeredController;
    private bool _isInterative = true;

    [SerializeField]
    [Tooltip("UI to use if someone interacts")]
    private GameObject _consoleUI;
    private bool _isConsoleUILoaded;
    private bool _hasElectricity;


    private void Initalize(){
        if(!_ownerShip){
            Debug.Log("OwnerShip is not initialized. The associated functions are disabled. Please Set the OwnerShip. Location : " + gameObject);
            _isOwnerShipLoaded = false;
        }
        else{
            _isOwnerShipLoaded = true;
        }
        if(!_objectToControl){
            Debug.Log("ObjectToControl is not initialized. The associated functions are disabled. Please Set the OwnerShip. Location : " + gameObject);
            _isObjectToControlLoaded = false;
        }
        else{
            _isObjectToControlLoaded = true;
        }
        if(!_consoleUI){
            Debug.Log("ConsoleUI is not initialized. The associated functions are disabled. Location : " + gameObject);
            _isConsoleUILoaded = false;
        }
        else{
            _isConsoleUILoaded = true;
        }
        if(!gameObject.GetComponent<BoxCollider>().isTrigger){
            Debug.Log("BoxCollider's trigger is missing. Please add it in the editor. Location : " + gameObject);
        }
        if(!gameObject.GetComponent<Electricity>()){
            Debug.Log("Electricity is not Loaded. Please add Electricity Module. Location : " + gameObject);
            _hasElectricity = false;
        }
        else{
            _hasElectricity = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") && _triggeredController == null){
            _triggeredController = other.GetComponent<PlayerHuman>().PlayerController;
            _triggeredController.TriggerObject = gameObject;
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player") && _triggeredController != null){
            _triggeredController.TriggerObject = null;
            _triggeredController = null;
        }
    }

    public void SwapContorlObject(){
        if(_hasElectricity){
            if(gameObject.GetComponent<Electricity>().IsPowered){
                if(_isInterative){
                    _handlingObject = _triggeredController.ControlObject;
                    _triggeredController.ControlObject = _objectToControl;
                    _triggeredController.gameObject.GetComponent<CameraController>().SetFollowTarget(_objectToControl);
                    _isInterative = false;
                }
                else if(!_isInterative){
                    _triggeredController.ControlObject = _handlingObject;
                    _triggeredController.gameObject.GetComponent<CameraController>().SetFollowTarget(_handlingObject);
                    _handlingObject = null;
                    _isInterative = true;
                }
            }
        }
    }
    public GameObject GetUI(){
        if(_isConsoleUILoaded){
            return _consoleUI;
        }
        else return null;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Initalize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

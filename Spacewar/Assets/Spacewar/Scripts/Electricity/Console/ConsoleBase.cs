using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleBase : MonoBehaviour
{
    [SerializeField]
    protected MainShip _ownerShip;
    [SerializeField]
    protected GameObject _objectToControl;
    [SerializeField]
    protected GameObject _handlingObject;
    protected PlayerController _triggeredController;
    protected bool _isInteractive = true;
    [SerializeField]
    protected GameObject _consoleUI;
    protected bool _isElectricityLoaded;

    protected void Initalize(){
        if(!_ownerShip){
            Debug.Log("OwnerShip is not initialized. The associated functions are disabled. Please Set the OwnerShip. Location : " + gameObject);
        }
        if(!_objectToControl){
            Debug.Log("ObjectToControl is not initialized. The associated functions are disabled. Please Set the OwnerShip. Location : " + gameObject);
        }
        if(!_consoleUI){
            Debug.Log("ConsoleUI is not initialized. The associated functions are disabled. Location : " + gameObject);
            }
        if(!gameObject.GetComponent<BoxCollider>().isTrigger){
            Debug.Log("BoxCollider's trigger is missing. Please add it in the editor. Location : " + gameObject);
        }
        if(!gameObject.GetComponent<Electricity>()){
            Debug.Log("Electricity is not Loaded. Please add Electricity Module. Location : " + gameObject);
            _isElectricityLoaded = false;
        }
        else{
            _isElectricityLoaded = true;
        }
    }
    protected void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") && _triggeredController == null){
            _triggeredController = other.GetComponent<PlayerHuman>().PlayerController;
            _triggeredController.TriggerObject = gameObject;
        }
    }
    protected void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player") && _triggeredController != null){
            _triggeredController.TriggerObject = null;
            _triggeredController = null;
        }
    }
    public GameObject GetUI(){
        if(_consoleUI){
            return _consoleUI;
        }
        else return null;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}

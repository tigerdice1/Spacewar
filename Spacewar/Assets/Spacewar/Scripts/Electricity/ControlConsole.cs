using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlConsole : MonoBehaviour
{
    [SerializeField]
    private GameObject _ownerShip;
    private bool _isOwnerShipLoaded;
    private PlayerController _triggeredController;
    private bool _isInterative;

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
        if(!_consoleUI){
            Debug.Log("consoleUI is not initialized. The associated functions are disabled. Location : " + gameObject);
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

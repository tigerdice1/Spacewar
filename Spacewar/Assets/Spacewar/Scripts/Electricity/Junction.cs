using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Junction : MonoBehaviour
{
    /* Essential Variables */
	// Generators connected to the junction. If not specified, it will not be executed.
    [SerializeField]
    [Tooltip("Generators connected to the junction. If not specified, it will not be executed.")]
    private Console_PowerGenerator _generatorConsole;
	// Power consuming objects connected to the junction. All objects containing the Electricity script are shown here.
    [SerializeField]
    [Tooltip("Power consuming objects connected to the junction. All objects containing the Electricity script are shown here.")]
    private List<Electricity> _connectedObjectsList;
 	// Total power consumption
	private float _totalPowerConsumption;
    
    void Initailize(){
        if(SceneManager.Instance().IsDebugMode()){
            if(!_generatorConsole){
                Debug.Log("Console_PowerGenerator is not Loaded. Please add GeneratorConsole. Location : " + gameObject);
            }
        }
    }
    /* Custom Functions */
	// Store the combined power consumption of the objects in the list.
    private void UpdatePowerConsumption(){
        _totalPowerConsumption = 0.0f;
        for(int i = 0; i < _connectedObjectsList.Count; i++){
            if(_connectedObjectsList[i] != null){
                _totalPowerConsumption += _connectedObjectsList[i].PowerConsumption;
            }
        }
    }
    

    void CheckPowerState(){
        if(_generatorConsole && _generatorConsole.IsPowered){
            foreach(Electricity connectedObject in _connectedObjectsList){
                if(connectedObject != null){
                    switch(connectedObject.GetState()){
                        case Electricity.State.OFF:
                            connectedObject.SetActiveState(Electricity.State.IDLE);
                        break;
                    }
                }
            }        
        }
            else if(!_generatorConsole.IsPowered){
                foreach(Electricity connectedObject in _connectedObjectsList){
                    if(connectedObject != null){
                    connectedObject.SetActiveState(Electricity.State.OFF);
                    }
                }
            }
        }
    
	/* 
    Calls the load adjustment function of the generator 
    if the amount of power produced is less or greater than the amount of power consumed. 
    */
    
    private void SyncPowerFromGenerator(){
        _generatorConsole.SyncPower(_totalPowerConsumption);
    }
 	// Start is called before the first frame update   
	void Start(){
        Initailize();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePowerConsumption();
        SyncPowerFromGenerator();
        CheckPowerState();
        
    }
}

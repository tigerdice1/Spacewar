using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Junction : MonoBehaviour
{
    /* Essential Variables */
	// Generators connected to the junction. If not specified, it will not be executed.
    [SerializeField]
    [Tooltip("Generators connected to the junction. If not specified, it will not be executed.")]
    private PowerGeneratorConsole _generatorConsole;
    private bool _isGeneratorConsoleLoaded;
	// Power consuming objects connected to the junction. All objects containing the Electricity script are shown here.
    [SerializeField]
    [Tooltip("Power consuming objects connected to the junction. All objects containing the Electricity script are shown here.")]
    private Electricity[] _connectedObjectsList;
 	// Total power consumption
	private float _totalPowerConsumption;
    
    void Initailize(){
        if(!_generatorConsole){
            Debug.Log("PowerGeneratorConsole is not Loaded. Please add GeneratorConsole. Location : " + gameObject);
            _isGeneratorConsoleLoaded = false;
        }
        else{
            _isGeneratorConsoleLoaded = true;
        }
    }
    /* Custom Functions */
	// Store the combined power consumption of the objects in the list.
    void UpdatePowerConsumption(){
        _totalPowerConsumption = 0.0f;
        foreach(Electricity connectedObject in _connectedObjectsList){
            if(connectedObject != null){
                _totalPowerConsumption += connectedObject.PowerConsumption;
            }
        }
    }

    void CheckPowerState(){
        if(_isGeneratorConsoleLoaded){
            if(_generatorConsole.IsPowered){
                foreach(Electricity connectedObject in _connectedObjectsList){
                    if(connectedObject != null){
                    connectedObject.SetPowerState(true);
                    }
                }
            }
            else if(!_generatorConsole.IsPowered){
                foreach(Electricity connectedObject in _connectedObjectsList){
                    if(connectedObject != null){
                    connectedObject.SetPowerState(false);
                    }
                }
            }
        }
    }
	/* 
    Calls the load adjustment function of the generator 
    if the amount of power produced is less or greater than the amount of power consumed. 
    */
    
    void SyncPowerFromGenerator(){
        _generatorConsole.SyncPower(_totalPowerConsumption);
    }
 	// Start is called before the first frame update   
	void Start()
    {
        Initailize();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePowerConsumption();
        if(_isGeneratorConsoleLoaded){
            SyncPowerFromGenerator();
            CheckPowerState();
        }
    }
}

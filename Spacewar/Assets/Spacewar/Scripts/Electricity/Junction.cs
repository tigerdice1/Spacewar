using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Junction : MonoBehaviour
{
    /* Essential Variables */
	// Generators connected to the junction. If not specified, it will not be executed.
    [SerializeField]
    [Tooltip("해당 분배기에 사용할 발전기를 연결합니다. 수동으로 지정해줘야 합니다.")]
    private Console_PowerGenerator _generatorConsole;
	// Power consuming objects connected to the junction. All objects containing the Electricity script are shown here.
    [SerializeField]
    [Tooltip("전력을 사용하는 오브젝트들을 지정하는 리스트입니다. Electricity 스크립트가 있어야 추가할 수 있습니다.")]
    private List<Electricity> _connectedObjectsList;
 	// Total power consumption
    [Tooltip("전력 사용량 총합.")]
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
    // 전력 사용량 총합을 계산하는 함수.
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
            for(int i = 0; i < _connectedObjectsList.Count; i++){
                switch(_connectedObjectsList[i].GetState()){
                    case Electricity.State.OFF:
                        _connectedObjectsList[i].SetActiveState(Electricity.State.IDLE);
                    break;
                    }
                }
            }
        else if(!_generatorConsole.IsPowered){
            for(int i = 0; i < _connectedObjectsList.Count; i++){
                _connectedObjectsList[i].SetActiveState(Electricity.State.OFF);
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
    void Update(){
        UpdatePowerConsumption();
        SyncPowerFromGenerator();
        CheckPowerState();
    }
}

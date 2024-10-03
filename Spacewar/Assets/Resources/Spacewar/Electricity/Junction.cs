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
    

    private void OnDebugMode(){
        if (_generatorConsole == null){
            Debug.LogWarning($"Console_PowerGenerator is not assigned. Please add GeneratorConsole. Location: {gameObject.name}");
        }
    }
    private void Initialize(){
        if (GameManager.Instance().IsDebugMode){
            OnDebugMode();
        }
    }
    /* Custom Functions */
	// Store the combined power consumption of the objects in the list.
    // 전력 사용량 총합을 계산하는 함수.
    private void UpdatePowerConsumption(){
        _totalPowerConsumption = 0.0f;

        if (_connectedObjectsList != null){
            foreach (var obj in _connectedObjectsList){
                if (obj != null){
                    _totalPowerConsumption += obj.PowerConsumption;
                }
            }
        }
    }

    private void CheckPowerState(){
        if (_generatorConsole != null && _generatorConsole.IsPowered){
            if (_connectedObjectsList != null){
                foreach (var obj in _connectedObjectsList){
                    if (obj != null){
                        if (obj.State == CustomTypes.ElectricState.OFF){
                            obj.SetActiveState(CustomTypes.ElectricState.IDLE);
                        }
                    }
                }
            }
        }
        else{
            if (_connectedObjectsList != null){
                foreach (var obj in _connectedObjectsList){
                    if (obj != null){
                        obj.SetActiveState(CustomTypes.ElectricState.OFF);
                    }
                }
            }
        }
    }
    
	/* 
    Calls the load adjustment function of the generator 
    if the amount of power produced is less or greater than the amount of power consumed. 
    */
    
    private void SyncPowerFromGenerator(){
        if (_generatorConsole != null){
            _generatorConsole.SyncPower(_totalPowerConsumption);
        }
    }
 	// Start is called before the first frame update   
	void Start(){
        Initialize();
    }

    // Update is called once per frame
    void Update(){
        UpdatePowerConsumption();
        SyncPowerFromGenerator();
        CheckPowerState();
    }
}

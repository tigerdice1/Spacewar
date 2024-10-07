using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Junction : MonoBehaviour
{
    [Tooltip("콘솔의 사용가능 범위 안에 들어와있는 PlayerController를 토글합니다. Trigger를 통해 자동으로 지정됩니다. Trigger 범위 안에 PlayerController 가 없는 경우 Null 을 반환하니 이 점에 유의해야합니다.")]
    private List<PlayerController> _triggeredControllers = new List<PlayerController>();
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
    [SerializeField]
    private float _durability = 100f;
    
    private Coroutine _playingCoroutine;

    protected IEnumerator FixDurabilityCoroutine(float skill){
        while (!CustomTypes.MathExt.Approximately(_durability, 100f)){
            _durability = Mathf.Lerp(_durability, 100f, Time.deltaTime * skill);
            yield return null;
        }
        _durability = 100f;        
    }

    public void FixObject(){
        _playingCoroutine = StartCoroutine(FixDurabilityCoroutine(.1f));
    }

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
    private void Aging(){
        float gain = _totalPowerConsumption / _generatorConsole.PowerOutput;
        if(_generatorConsole.PowerOutput <= 0f) gain = 0f;
        _durability = Mathf.Lerp(_durability, 0.0f, Time.deltaTime * gain * 0.002f);
        if(_durability <= 0f){
            _durability = 0f;
            _totalPowerConsumption = 0f;
        }
    }
    private void SyncPowerFromGenerator(){
        if (_generatorConsole != null){
            _generatorConsole.SyncPower(_totalPowerConsumption);
        }
    }

        protected void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")){
            PlayerBase playerBase = other.GetComponent<PlayerBase>();
            if (playerBase != null){
                PlayerController playerController = playerBase.PlayerController;
                if (playerController != null){
                    playerBase.PlayerController.TriggerObject = gameObject;
                    _triggeredControllers.Add(playerController);
                }
            }
        }
    }
    protected void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")){
            PlayerBase playerBase = other.GetComponent<PlayerBase>();
            if (playerBase != null){
                PlayerController playerController = playerBase.PlayerController;
                if (playerController != null){
                    playerBase.PlayerController.TriggerObject = gameObject;
                }
            }
        }
    }
    protected void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")){
            PlayerBase playerBase = other.GetComponent<PlayerBase>();
            if (playerBase != null){
                PlayerController playerController = playerBase.PlayerController;
                if (playerController != null){
                    _triggeredControllers.Remove(playerController);
                    playerController.TriggerObject = null;
                    if(_playingCoroutine != null){
                        StopCoroutine(_playingCoroutine);
                    }
                }
            }
        }
    }
 	// Start is called before the first frame update   
	void Start(){
        Initialize();
    }

    // Update is called once per frame
    void Update(){
        CheckPowerState();
        UpdatePowerConsumption();
        Aging();
        if(_durability > 0f){
            SyncPowerFromGenerator();
        }
    }
}

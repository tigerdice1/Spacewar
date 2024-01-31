using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console_PowerGenerator : ConsoleBase
{
    [SerializeField]
    [Tooltip("해당 발전기와 연결된 정션을 지정")]
    private Junction _connectedJunction;

    [SerializeField]
    [Tooltip("조명")]
    private Light _lightComponent;
    
     /* Generator Info */
    // It's the efficiency of the generator. The more efficient, the higher the output under less load. Range 0 ~ 100
    [SerializeField]
    [Range(0, 100)]
    [Tooltip("발전기의 효율 설정 단위 : 백분율")]
    private float _efficiency;

    // Current amount of fuel in the generator. Range 0 ~ _maxFuel
    [SerializeField]
    [Tooltip("현재 연료량")]
    private float _currentFuel;
    // Maximum amount of fuel in the generator. Range 0 ~ 
    [SerializeField]
      [Tooltip("최대 연료량")]
    private float _maxFuel;

    // The amount of power currently being output.
    [SerializeField]
    [Tooltip("현재 출력중인 전력량")]
    private float _outputPower;
    // Maximum output power.
    [SerializeField]
    [Tooltip("최대로 출력가능한 전력량")]
    private float _maxPower;

    // Current load on the generator.
    [SerializeField]
    [Range(0, 100)]
    [Tooltip("발전기의 현재 부하량")]
    private float _load;
    // Current Thermal on the generator.
    [SerializeField]
    [Tooltip("발전기의 현재 온도")]
    private float _currentThermal;

    // The critical temperature of the generator.
    [SerializeField]
    [Tooltip("발전기의 임계 온도")]
    private float _criticalThermal;
    // The time the critical temperature of the generator was maintained. Usually, the action occurs after this time.
    [SerializeField]
    [Tooltip("발전기의 임계 온도가 유지된 시간")]
    private float _criticalThermalTimer;
    private bool _isCritical;

    // A variable that indicates whether the generator is operating.
    [SerializeField]
    [Tooltip("발전기 작동여부")]
    private bool _isPowered;
    
    // Update cycle of the generator. Use to adjust the update rate for a particular action.
    [SerializeField]
    [Tooltip("업데이트 주기")]
    private float _updateCycleTime;

    /* 시간계산용 변수 */
    private float _fuelTimer;
    private float _thermalTimer;

  /* Properties */
    public bool IsPowered{
        set{_isPowered = value; }
        get{return _isPowered; }
    }
    public float PowerOutput{
        set{_outputPower = value; }
        get{return _outputPower; }
    }
    public float Load{
        set{_load = value; }
        get{return _load; }
    }
    public float CurrentFuel{
        set{_currentFuel = value; }
        get{return _currentFuel; }
    }
    public float MaxFuel{
        set{_maxFuel = value; }
        get{return _maxFuel; }
    }
    public bool GetGeneratorState(){
        return _isPowered;
    }
    public void SetGeneratorState(bool isOn){
        _isPowered = isOn;
    }

    public bool GetIsCritical(){
        return _isCritical;
    }

    /* Essential Functions */

    private void UpdateThermal(){
        if(_isPowered){
            if(_load < 50.0f){
                _currentThermal = Mathf.Lerp(_currentThermal, 2500.0f, Time.deltaTime * 0.5f);
            }
            else if(_load >= 50.0f){
                _currentThermal = Mathf.Lerp(_currentThermal, 3500.0f, Time.deltaTime * 0.1f);
            }
        }
        else if(!_isPowered){
            _currentThermal = Mathf.Lerp(_currentThermal, 0.0f, Time.deltaTime * 0.5f);
        }
    }
    public void SyncPower(float powerUsage){
        float targetLoad = (powerUsage / _maxPower) * 100.0f;
        float clamppedLoad = Mathf.Clamp(targetLoad, 0.0f, _maxPower);
        _load = Mathf.Lerp(_load, clamppedLoad, Time.deltaTime * 3.0f);
    }
    private bool CheckFuelAmount(){
        if(_currentFuel < 0.0f){
             SetGeneratorState(false);
            return false;
        }
        else{
            return true;
        }
    }
    private void CalcOutputPower(){
        _currentFuel -= _load / Mathf.Pow(_efficiency, 2.0f);
        _outputPower = _maxPower / 100.0f * _load;
    }

    // 발전기의 온도를 체크해서 임계온도 도달 시 경고음 / 이상효과 / 시간 측정
    private void CheckGeneratorThermal(){
        if(_currentThermal > _criticalThermal){
            _thermalTimer += Time.deltaTime;
            _isCritical = true;
        }
        else if(_currentThermal < _criticalThermal){
            _thermalTimer = 0.0f;
            _isCritical = false;
        }
        if(_thermalTimer >= _criticalThermalTimer){
            // 임계온도 도달 후 시간 경과 이후 액션
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

    /* Optional Functions */
    private void UpdateGeneratorLight(){
        if(_isPowered){
            _lightComponent.GetComponent<Electricity>().SetActiveState(Electricity.State.ACTIVE);
            if(_isCritical){
                _lightComponent.GetComponent<LightController>().SetLightColor(Color.red);
            }
            else{
                _lightComponent.GetComponent<LightController>().SetLightColor(Color.green);
            }
        }
        else if(!_isPowered){
            _lightComponent.gameObject.GetComponent<Electricity>().SetActiveState(Electricity.State.OFF);
        }
    }
    // Start is called before the first frame update
    protected override void Start(){
        base.Initalize();
        SetGeneratorState(false);
    }

    // Update is called once per frame
    protected override void Update()
    {
        UpdateThermal();
        if(_isPowered){
            CheckFuelAmount();
            CalcOutputPower();
            CheckGeneratorThermal();
        }
        if(_lightComponent){
            UpdateGeneratorLight();
        }
    }
}

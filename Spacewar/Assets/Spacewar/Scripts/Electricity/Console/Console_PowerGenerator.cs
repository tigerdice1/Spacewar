using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console_PowerGenerator : ConsoleBase
{
    [SerializeField]
    [Tooltip("해당 발전기와 연결된 분배기를 지정합니다.")]
    private Junction _connectedJunction;

    [SerializeField]
    [Tooltip("조명")]
    private Light _lightSource;
    
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

    // 온도 업데이트 함수
    private void UpdateThermal(){
        // 전원이 켜진 상태이고 로드율이 50% 보다 높을경우, 3500도까지 로드율에 비례해서 서서히 증가
        // 50% 보다 적을경우, 2500도까지 서서히 감소
        // 전원이 꺼질 경우 0도까지 온도 감소
        if(_isPowered){
            if(_load < 50.0f){
                _currentThermal = Mathf.Lerp(_currentThermal, 2500.0f, Time.deltaTime * (_load * 0.01f));
            }
            else if(_load >= 50.0f){
                _currentThermal = Mathf.Lerp(_currentThermal, 3500.0f, Time.deltaTime * (_load * 0.01f));
            }
        }
        else if(!_isPowered){
            _currentThermal = Mathf.Lerp(_currentThermal, 0.0f, Time.deltaTime * 0.5f);
        }
    }
    // 전력출력 동기화 함수 
    // 필요한 전력량에 맞춰 로드율을 자동으로 계산해서 조정합니다. Junction 스크립트에서만 호출합니다.
    public void SyncPower(float powerUsage){
        // 목표로드율을 구합니다.
        // 목표로드율은 0% 보다 낮거나 100% 보다 클 수 없으므로 Clamp로 범위를 제한합니다.
        // 현재로드율을 목표로드율까지 서서히 조정합니다.
        float targetLoad = (powerUsage / _maxPower) * 100.0f;
        float clamppedLoad = Mathf.Clamp(targetLoad, 0.0f, 100.0f);
        _load = Mathf.Lerp(_load, clamppedLoad, Time.deltaTime * 3.0f);
    }

    // 연료량을 체크하는 함수
    private void CheckFuelAmount(){
        // 현재 연료량이 0 이하라면 발전기의 전원을 끕니다.
        if(_currentFuel < 0.0f){
             SetGeneratorState(false);
        }
    }
    
    // 발전기의 출력을 계산하는 함수
    private void CalcOutputPower(){
        // 현재로드율 / (효율^2) 만큼 현재 연료량에서 뺍니다.
        // 출력은 최대출력 / 100 * 현재로드율
        _currentFuel -= _load / Mathf.Pow(_efficiency, 2.0f);
        _outputPower = _maxPower / 100.0f * _load;
    }

    // 발전기의 온도를 체크해서 임계온도 도달 시 경고음 / 이상효과 / 시간 측정 하는 함수
    private void CheckGeneratorThermal(){
        // 현재 온도가 임계온도보다 높을 경우 임계온도 타이머 작동
        // 현재 온도가 임계온도보다 낮아지면 임계온도 타이머 초기화
        // 임계온도 타이머가 지정된 시간만큼 경과시 액션
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

    /* Optional Functions */
    // 발전기에 연결된 광원을 동기화하는 함수
    private void UpdateGeneratorLight(){
        // 전원이 켜진 상태라면 광원의 Electricity 스크립트에서 작동상태 변경
        // 임계온도시 광원의 색상을 변경
        // 임계온도 이하로 떨어지면 기존 색상으로 변경
        // 전원이 꺼진 상태라면 광원의 Electricity 스크립트에서 작동상태 변경
        if(_isPowered){
            _lightSource.GetComponent<Electricity>().SetActiveState(Electricity.State.ACTIVE);
            if(_isCritical){
                _lightSource.GetComponent<LightController>().SetLightColor(Color.red);
            }
            else{
                _lightSource.GetComponent<LightController>().SetLightColor(Color.green);
            }
        }
        else if(!_isPowered){
            _lightSource.gameObject.GetComponent<Electricity>().SetActiveState(Electricity.State.OFF);
        }
    }
    // Start is called before the first frame update
    protected override void Start(){
        base.Initalize();
        // 디버그용 
        SetGeneratorState(true);
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
        if(_lightSource){
            UpdateGeneratorLight();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGenerator : FixableObjects{
    public float CurrentThermal;
    public float CriticalThermal;
    public float CurrentFuel;
    public float MaxFuel;
    public float MaxPower;
    [Range(0, 120)]
    public float Load;
    [SerializeField]
    private Junction _connectedJunction;
    [SerializeField]
    [Range(0, 100)]
    private float _efficiency;
    [SerializeField]
    private float _outputPower;
    
    [SerializeField]
    private float _criticalThermalTimer;
    [SerializeField]
    private bool _isCritical;
    public bool IsPowered;

    /* 시간계산용 변수 */
    private float _fuelTimer;
    private float _thermalTimer;

    public void SetGeneratorState(bool isOn){
        IsPowered = isOn;
    }
    public void FillFuel(){
        CurrentFuel = MaxFuel;
    }
    public void SyncPower(float powerUsage){
        // 목표로드율을 구합니다.
        // 목표로드율은 0% 보다 낮거나 100% 보다 클 수 없으므로 Clamp로 범위를 제한합니다.
        // 현재로드율을 목표로드율까지 서서히 조정합니다.
        float targetLoad = (powerUsage / MaxPower) * 100.0f;
        float clamppedLoad = Mathf.Clamp(targetLoad, 0.0f, 120.0f);
        Load = Mathf.Lerp(Load, clamppedLoad, Time.deltaTime * 2.0f);
    }
    protected override void Aging(){
        base.Aging();
        if(_durability <= 0f){
            _durability = 0f;
            IsPowered = false;
        }
    }
    private void UpdateThermal(){
        // 전원이 켜진 상태이고 로드율이 50% 보다 높을경우, 5000도까지 로드율에 비례해서 서서히 증가
        // 50% 보다 적을경우, 2500도까지 서서히 감소
        // 전원이 꺼질 경우 0도까지 온도 감소
        if (IsPowered){
            float targetThermal = Load < 50.0f ? 2500.0f : 5000.0f * 1.2f;
            float thermalChangeRate = Load * 0.01f;
            CurrentThermal = Mathf.Lerp(CurrentThermal, targetThermal, Time.deltaTime * thermalChangeRate);
        }
        else{
            CurrentThermal = Mathf.Lerp(CurrentThermal, 0.0f, Time.deltaTime * 0.5f);
        }
    }
    private void CheckGeneratorThermal(){
        // 현재 온도가 임계온도보다 높을 경우 임계온도 타이머 작동
        // 현재 온도가 임계온도보다 낮아지면 임계온도 타이머 초기화
        // 임계온도 타이머가 지정된 시간만큼 경과시 액션
        if (CurrentThermal > CriticalThermal){
            _thermalTimer += Time.deltaTime;
            _isCritical = true;

            if (_thermalTimer >= _criticalThermalTimer){
                // 임계온도 도달 후 시간 경과 이후 액션
                // 예: 발전기 손상, 폭발 등
                HandleCriticalState();
            }
        }
        else{
            _thermalTimer = 0.0f;
            _isCritical = false;
        }
    }
    private void HandleCriticalState(){
        // 임계 상태에 대한 처리 로직 구현
        Debug.LogWarning("발전기가 임계 상태에 도달했습니다!");
        // 추가 액션 구현 필요
    }

    private void CheckFuelAmount(){
        // 현재 연료량이 0 이하라면 발전기의 전원을 끕니다.
        if (CurrentFuel <= 0.0f){
            CurrentFuel = 0.0f;
            IsPowered = false;
        }
    }
    private void CalculateOutputPower(){
        // 현재로드율 / (효율^2) 만큼 현재 연료량에서 뺍니다.
        // 출력은 최대출력 / 100 * 현재로드율
        CurrentFuel -= Load / Mathf.Pow(_efficiency, 2.0f);
        _outputPower = MaxPower / 100.0f * Load;
    }
    protected void Start(){
        Initialize();
    }
    protected override void Update(){
        UpdateThermal();
        if (IsPowered){
            CheckFuelAmount();
            CalculateOutputPower();
            CheckGeneratorThermal();
            Aging();
        }
    }
}

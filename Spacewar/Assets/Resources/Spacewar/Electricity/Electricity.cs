using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity : MonoBehaviour
{

    public int Priority;
    /* Power consumption refers to the total power consumption of an object using electricity */
    // 현재 전력 소모량을 저장하는 변수입니다.
    [SerializeField]
    [Tooltip("현재 전력 소모량을 저장하는 변수입니다.")]
    private float _powerConsumption;

    // 전원은 켜져있지만 사용하지 않는 상태일때의 전력 소모 최대치를 지정하는 변수입니다.
    [SerializeField]
    [Tooltip("전원은 켜져있지만 사용하지 않는 상태일때의 전력 소모 최대치를 지정하는 변수입니다.")]
    private float _powerIdle;

    // 전원이 켜져있고 사용중일 때 전력 소모 최대치를 지정하는 변수입니다.
    [SerializeField]
    [Tooltip("전원이 켜져있고 사용중일 때 전력 소모 최대치를 지정하는 변수입니다.")]
    private float _powerActive;

    // 전원이 켜져있고 사용중일 때 전력 소모 최대치를 지정하는 변수입니다.
    [SerializeField]
    [Tooltip("전원이 켜져있는지 확인하는 변수입니다.")]
    private bool _isPowered;

    // 현재 전자기계의 상태를 저장하는 변수입니다.
    private CustomTypes.ElectricState _state;

    private Coroutine _playingCoroutine;

    /* Properties */
    public bool IsPowered{
        set => _isPowered = value;
        get => _isPowered;
    }
    public float PowerConsumption{
        set => _powerConsumption = value;
        get => _powerConsumption;
    }
    public float PowerIdle{
        set => _powerIdle = value;
        get => _powerIdle;
    }
    public float PowerActive{
        set => _powerActive = value;
        get => _powerActive;
    }

    public CustomTypes.ElectricState State{
        get => _state;
        private set => _state = value;
    }

    IEnumerator UpdatePowerConsumptionCoroutine(float targetValue){
        while (!CustomTypes.MathExt.Approximately(_powerConsumption, targetValue)){
            _powerConsumption = Mathf.Lerp(_powerConsumption, targetValue, Time.deltaTime);
            yield return null;
        }
        _powerConsumption = targetValue;
    }

    // 전원의 켜고 끔을 지정하는 함수입니다. 외부에서 호출되지 않습니다.
    private void SetPowerConsumption(bool isOn){
        // 전원이 켜지면 전력 소모량을 대기전력 소모량으로 변경합니다. 
        
        if(isOn){
            if (_playingCoroutine != null){
                StopCoroutine(_playingCoroutine);
            }
            _playingCoroutine = StartCoroutine(UpdatePowerConsumptionCoroutine(_powerIdle));
        }
        else{
            if (_playingCoroutine != null){
                StopCoroutine(_playingCoroutine);
            }
            _playingCoroutine = StartCoroutine(UpdatePowerConsumptionCoroutine(0f));
        }
    }

    // 전원 상태를 지정하는 함수입니다. 외부에서 호출되는 함수입니다.
    public void SetActiveState(CustomTypes.ElectricState newState){
        if (State == newState) return;
        State = newState;

        switch (State){
            case CustomTypes.ElectricState.OFF:
                SetPowerConsumption(false);
                break;
            case CustomTypes.ElectricState.IDLE:
                SetPowerConsumption(true);
                break;
            case CustomTypes.ElectricState.ACTIVE:
                if (_playingCoroutine != null){
                    StopCoroutine(_playingCoroutine);
                }
                    _playingCoroutine = StartCoroutine(UpdatePowerConsumptionCoroutine(_powerActive));
                break;
        }
    }

    public void CheckPowerState(){
        if(_powerConsumption >= _powerIdle * 0.9f){
            _isPowered = true;
        }
        else{
            _isPowered = false;
        }
    }
    private void Initialize(){
        SetActiveState(CustomTypes.ElectricState.OFF);
    }
    // Start is called before the first frame update
    void Start(){
        Initialize();
    }

    // Update is called once per frame
    void Update(){
        SetActiveState(_state);
        CheckPowerState();
    }
}

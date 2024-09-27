using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity : MonoBehaviour
{
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

    public CustomTypes.ElectricState GetState{
        get => _state;
    }

    // 전원의 켜고 끔을 지정하는 함수입니다. 외부에서 호출되지 않습니다.
    private void SetPowerState(bool isOn){
        // 전원이 켜지면 전력 소모량을 대기전력 소모량으로 변경합니다.
        _isPowered = isOn; 
        _powerConsumption = _isPowered ? _powerIdle : 0.0f;
    }

    // 전원 상태를 지정하는 함수입니다. 외부에서 호출되는 함수입니다.
    public void SetActiveState(CustomTypes.ElectricState state){
        if(_state == state) return;
        _state = state;
        switch(_state){
            case CustomTypes.ElectricState.OFF:
                SetPowerState(false);
            break;
            case CustomTypes.ElectricState.IDLE:
                SetPowerState(true);
            break;
            case CustomTypes.ElectricState.ACTIVE:
                SetPowerState(true);
                _powerConsumption = _powerActive;
            break;
        }
    }

    private void CheckActiveState(){
        SetActiveState(_state);
    }

    private void Initalize(){
       _state = CustomTypes.ElectricState.OFF;
    }
    // Start is called before the first frame update
    void Start(){
        Initalize();
    }

    // Update is called once per frame
    void Update(){
        CheckActiveState();
    }
}

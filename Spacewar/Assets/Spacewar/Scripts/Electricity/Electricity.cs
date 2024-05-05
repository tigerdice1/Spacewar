using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity : MonoBehaviour
{
    // 전기기계의 작동상태를 지정하는 enum 입니다.
    public enum State{
        OFF,
        IDLE,
        ACTIVE
    }
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
    private State _state = State.OFF;

    /* Properties */
    public bool IsPowered{
        get { return _isPowered; }
        set { _isPowered = value; }
    }
    public float PowerConsumption{
        get { return _powerConsumption; }
        set { _powerConsumption = value; }
    }
    public float PowerIdle{
        get { return _powerIdle; }
        set { _powerIdle = value; }
    }
    public float PowerActive{
        get { return _powerActive; }
        set { _powerActive = value; }
    }

    public State GetState(){
        return _state;
    }

    // 전원의 켜고 끔을 지정하는 함수입니다. 외부에서 호출되지 않습니다.
    private void SetPowerState(bool isOn){
        // 전원이 켜지면 전력 소모량을 대기전력 소모량으로 변경합니다.
        _isPowered = isOn; 
        _powerConsumption = _isPowered ? _powerIdle : 0.0f;
    }

    // 전원 상태를 지정하는 함수입니다. 외부에서 호출되는 함수입니다.
    public void SetActiveState(State state){
        if(_state == state) return;
        _state = state;
        switch(_state){
            case State.OFF:
                SetPowerState(false);
            break;
            case State.IDLE:
                SetPowerState(true);
            break;
            case State.ACTIVE:
                SetPowerState(true);
                _powerConsumption = _powerActive;
            break;
        }
    }

    private void CheckActiveState(){
        SetActiveState(_state);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate(){

    }
    // Update is called once per frame
    void Update()
    {
        CheckActiveState();
    }
}

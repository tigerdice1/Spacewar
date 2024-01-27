using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity : MonoBehaviour
{
    public enum State{
        OFF,
        IDLE,
        ACTIVE
    }
    /* Power consumption refers to the total power consumption of an object using electricity */
    [SerializeField]
    private float _powerConsumption;
    [SerializeField]
    private float _powerIdle;
    [SerializeField]
    private float _powerActive;
    [SerializeField]
    private bool _isPowered;

    private State _state;

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

    private void SetPowerState(bool isOn){
        _isPowered = isOn; 
        _powerConsumption = _isPowered ? _powerIdle : 0.0f;
    }
    public void SetActiveState(State state){
        _state = state;
        if(_state == State.OFF){
            SetPowerState(false);
        }
        else if(_state == State.IDLE){
            SetPowerState(true);
        }
        else if(_state == State.ACTIVE){
            SetPowerState(true);
            _powerConsumption = _powerActive;
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

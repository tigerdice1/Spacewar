using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField]
    private Light _lightComponent;


/*
    public enum LightState{
        Off,
        On,
        Critical
    };
    
    private LightState _lightStateColor;
    public void SetLightState(bool isOn)
    {
        if (_lightComponent != null){
            _lightComponent.enabled = isOn;
            gameObject.GetComponent<Electricity>().SetPowerState(isOn);

            if (_lightStateColor == LightState.On){
                LightStateColor = LightState.On;
            }
            else if (_lightStateColor == LightState.Critical){
                LightStateColor = LightState.Critical;
            }
        }
    }
    public LightState LightStateColor
    {
        get => _lightStateColor;
        set
        {
            switch(value)
            {
                case LightState.Off:
                    break;
                case LightState.On:
                    _lightComponent.color = Color.green;
                    Debug.Log("Yes");
                    break;
                case LightState.Critical:
                    _lightComponent.color = Color.red;
                    Debug.Log("NO");
                    break;
            }
            _lightStateColor = value;
        }
    }
    */
    /* Properties */
    public void SetLightColor(Color color){
        _lightComponent.color = color;
    }
    
    void CheckIsOn(){
        _lightComponent.enabled = gameObject.GetComponent<Electricity>().IsPowered;
    }
    // Start is called before the first frame update
    void Start()
    {
        //SetLightState(false);
        //LightStateColor = LightState.On;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIsOn();
    }
}

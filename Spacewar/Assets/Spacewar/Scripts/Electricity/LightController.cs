using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField]
    private Light _lightComponent;
    private bool _isLightComponentLoaded;

    private bool _hasElectricity;

    /* Properties */
    public void SetLightColor(Color color){
        if(_isLightComponentLoaded){
            _lightComponent.color = color;
        }
    }

    public void SetHexColor(string hexCode)
        {
            if(_isLightComponentLoaded){
                Color color;
                if ( ColorUtility.TryParseHtmlString(hexCode, out color)){
                    _lightComponent.color = color;
                }
            }
        }
    
    private void Initalize(){
        if(!_lightComponent){
            Debug.Log("LightComponent is not Loaded. The associated functions are disabled. Please Set the LightComponent. Location : " + gameObject);
            _isLightComponentLoaded = false;
        }
        else{
            _isLightComponentLoaded = true;
        }
        if(!gameObject.GetComponent<Electricity>()){
            Debug.Log("Electricity is not Loaded. Please add Electricity Module. Location : " + gameObject);
            _hasElectricity = false;
        }
        else{
            _hasElectricity = true;
        }
    }
    private void SyncOnState(){
        _lightComponent.enabled = gameObject.GetComponent<Electricity>().IsPowered;
    }
    // Start is called before the first frame update
    void Start()
    {
        Initalize();
        //SetLightState(false);
        //LightStateColor = LightState.On;
    }

    // Update is called once per frame
    void Update()
    {
        if(_isLightComponentLoaded && _hasElectricity){
            SyncOnState();
        }
    }

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
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField]
    private Light[] _lightComponent;
    private bool _isLightComponentLoaded;

    private bool _hasElectricity;

    /* Properties */
    public void SetLightColor(Color color){
        if(_isLightComponentLoaded){
            foreach(var elem in _lightComponent){
                elem.color = color;
            }
        }
    }

    public void SetHexColor(string hexCode)
        {
            if(_isLightComponentLoaded){
                Color color;
                if (ColorUtility.TryParseHtmlString(hexCode, out color)){
                    foreach(var elem in _lightComponent){
                        elem.color = color;
                    }
                }
            }
        }
    
    private void Initalize(){
        _lightComponent = gameObject.GetComponentsInChildren<Light>();
        if(!gameObject.GetComponent<Electricity>()){
            Debug.Log("Electricity is not Loaded. Please add Electricity Module. Location : " + gameObject);
            _hasElectricity = false;
        }
        else{
            _hasElectricity = true;
        }
    }
    private void SyncOnState(){
        foreach(var elem in _lightComponent){
            elem.enabled = gameObject.GetComponent<Electricity>().IsPowered;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Initalize();
    }

    // Update is called once per frame
    void Update()
    {
        if(_isLightComponentLoaded && _hasElectricity){
            SyncOnState();
        }
    }
}

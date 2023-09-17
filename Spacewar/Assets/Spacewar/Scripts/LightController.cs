using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField]
    private Light _lightComponent;

    [SerializeField]
    private bool _isLightOn;

    private bool _usingElectricity;

    void ChcekElectricity(){
        if(_usingElectricity){
            
        }
    }
    public void SetLightState(bool isOn)
    {
        if (_lightComponent != null){
            _lightComponent.enabled = isOn;
            _isLightOn = isOn;
            if(_usingElectricity){
                gameObject.GetComponent<Electricity>().SetPowerState(isOn);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
        SetLightState(_isLightOn);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

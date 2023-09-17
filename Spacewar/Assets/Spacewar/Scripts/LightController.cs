using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField]
    private Light _lightComponent;

    [SerializeField]
    private bool _isLightOn;

    public void SetLightState(bool isOn)
    {
        if (_lightComponent != null){
            _lightComponent.enabled = isOn;
            _
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

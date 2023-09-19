using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField]
    private Light _lightComponent;

    public void SetLightState(bool isOn)
    {
        if (_lightComponent != null){
            _lightComponent.enabled = isOn;
            gameObject.GetComponent<Electricity>().SetPowerState(isOn);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //SetLightState(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

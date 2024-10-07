using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Radial_Temperture : UI_RadialBase
{
    // Start is called before the first frame update
    [SerializeField]
    protected PowerGenerator _powerGenerator; // PowerGenerator 스크립트 참조
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        if (_powerGenerator != null){
            UpdateRotation(_powerGenerator.CurrentThermal / _powerGenerator.CriticalThermal * 100f, 0f, _powerGenerator.CriticalThermal, true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Radial_Temperture : UI_RadialBase
{
    // Start is called before the first frame update
    public PowerGenerator PowerGenerator; // PowerGenerator 스크립트 참조
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        if (PowerGenerator != null){
            UpdateRotation(PowerGenerator.CurrentThermal / PowerGenerator.CriticalThermal * 100f, 0f, PowerGenerator.CriticalThermal, true);
        }
    }
}

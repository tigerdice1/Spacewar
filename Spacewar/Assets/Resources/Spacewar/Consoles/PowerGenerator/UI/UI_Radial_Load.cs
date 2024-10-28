using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Radial_Load : UI_RadialBase
{
    
    // PowerGenerator에서 값을 받는 변수
    public PowerGenerator PowerGenerator; // PowerGenerator 스크립트 참조
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        if (PowerGenerator != null){
            UpdateRotation(PowerGenerator.Load, 0f, PowerGenerator.MaxPower * 1.2f, false);
        }
    }
}

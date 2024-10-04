using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Radial_Load : UI_RadialBase
{
    
    // PowerGenerator에서 값을 받는 변수
    [SerializeField]
    protected Console_PowerGenerator _powerGenerator; // PowerGenerator 스크립트 참조
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        if (_powerGenerator != null){
            UpdateRotation(_powerGenerator.Load, 0f, _powerGenerator.MaxPower * 1.2f, false);
        }
    }
}

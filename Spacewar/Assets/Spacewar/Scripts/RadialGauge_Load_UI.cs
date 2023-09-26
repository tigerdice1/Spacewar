using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialGauge_Load_UI : RadialGauge_UI
{
    protected override void Update(){
        base.Update();
        DynamicRotationBySlerp(_rotationAngle, 1.0f);
    }
}

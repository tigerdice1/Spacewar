using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialGauge_Load_UI : RadialGauge_UI
{
    protected override void Update(){
        base.Update();
        _rotationAngle = _parentUI.GetComponent<PowerGeneratorUI>().GetPowerGenerator().Load * (_maximumRotation - _minimumRotation) / 100.0f + _minimumRotation;
        SyncRotationBySlerp(_rotationAngle, 1.0f);
    }
}

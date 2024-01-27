        using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar_Fuel_UI : ProgressBar_UI
{
    protected override void Update()
    {
        base.Update();
        float fuelPercent = _parentUI.GetComponent<PowerGeneratorUI>().GetPowerGeneratorConsole().CurrentFuel / _parentUI.GetComponent<PowerGeneratorUI>().GetPowerGeneratorConsole().MaxFuel;
        SyncProgressBarBySlerp(fuelPercent, 1.0f);
    }
}

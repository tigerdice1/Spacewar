using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_FuelBar : MonoBehaviour
{
    public void UseItem(Console_PowerGenerator targetPowerGenerator){
        targetPowerGenerator.FillFuel();
    }
}

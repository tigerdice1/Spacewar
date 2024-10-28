using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FuelBar : UI_ProgressBarBase
{
    [SerializeField]
    public PowerGenerator PowerGenerator;

    private void Initialize(){

    }

    // Start is called before the first frame update
    protected override void Start(){
        base.Initialize();
        Initialize();
    }

    // Update is called once per frame
     protected override void Update(){
        if(PowerGenerator!= null){
            SyncProgressBar(PowerGenerator.CurrentFuel / PowerGenerator.MaxFuel, 5f, false);
        }
    }
}

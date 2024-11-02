using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Junction : FixableObjects
{
    public float TotalPowerConsumption = 0f;
    [SerializeField]
    private PowerGenerator _generatorConsole;
    [SerializeField]
    private List<Electricity> _connectedObjectsList;
    private void UpdatePowerConsumption(){
        TotalPowerConsumption = 0f;
        if (_connectedObjectsList != null){
            foreach (var obj in _connectedObjectsList){
                if (obj != null){
                    TotalPowerConsumption += obj.PowerConsumption;
                }
            }
        }
    }
    private void CheckPowerState(){
        if (_generatorConsole != null && _generatorConsole.IsPowered){
            if (_connectedObjectsList != null){
                foreach (var obj in _connectedObjectsList){
                    if (obj != null){
                        if (obj.State == CustomTypes.ElectricState.OFF){
                            obj.SetActiveState(CustomTypes.ElectricState.IDLE);
                        }
                    }
                }
            }
        }
        else{
            if (_connectedObjectsList != null){
                foreach (var obj in _connectedObjectsList){
                    if (obj != null){
                        obj.SetActiveState(CustomTypes.ElectricState.OFF);
                    }
                }
            }
        }
    }
    private void SyncPowerFromGenerator(){
        if (_generatorConsole != null){
            _generatorConsole.SyncPower(TotalPowerConsumption);
        }
    }

    // Update is called once per frame
    protected override void Update(){
        base.Update();
        CheckPowerState();
        UpdatePowerConsumption();
        if(_durability > 0f){
            SyncPowerFromGenerator();
        }
    }
}

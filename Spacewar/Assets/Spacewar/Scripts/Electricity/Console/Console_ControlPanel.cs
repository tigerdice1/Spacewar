using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console_ControlPanel : ConsoleBase
{
    public void SwapContorlObject(){
        if(_isElectricityLoaded){
            if(gameObject.GetComponent<Electricity>().IsPowered){
                if(_isInteractive){
                    gameObject.GetComponent<Electricity>().SetActiveState(Electricity.State.ACTIVE);
                    _handlingObject = _triggeredController.ControlObject;
                    _triggeredController.ControlObject = _objectToControl;
                    _triggeredController.gameObject.GetComponent<CameraController>().SetFollowTarget(_objectToControl);
                    _isInteractive = false;
                }
                else if(!_isInteractive){
                    gameObject.GetComponent<Electricity>().SetActiveState(Electricity.State.IDLE);
                    _triggeredController.ControlObject = _handlingObject;
                    _triggeredController.gameObject.GetComponent<CameraController>().SetFollowTarget(_handlingObject);
                    _handlingObject = null;
                    _isInteractive = true;
                }
            }
        }
    }
    protected override void Start(){
        base.Initalize();
    }
}

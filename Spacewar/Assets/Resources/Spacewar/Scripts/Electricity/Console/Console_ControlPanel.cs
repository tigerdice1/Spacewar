using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console_ControlPanel : ConsoleBase
{
    // 컨트롤 할 오브젝트를 변경하는 함수
    // ConsoleBase 로부터 상속받아 작성됨
    public void SwapContorlObject(){
        if(this.GetComponent<Electricity>() && this.GetComponent<Electricity>().IsPowered){
            if(_isInteractive){
                this.GetComponent<Electricity>().SetActiveState(Electricity.State.ACTIVE);
                _handlingObject = _triggeredController.ControlObject.GetComponent<PlayerBase>();
                _triggeredController.ControlObject = _objectToControl;
                _triggeredController.gameObject.GetComponent<CameraController>().SetFollowTarget(_objectToControl);
                _isInteractive = false;
            }
            else if(!_isInteractive){
                this.GetComponent<Electricity>().SetActiveState(Electricity.State.IDLE);
                _triggeredController.ControlObject = _handlingObject.transform.gameObject;
                _triggeredController.gameObject.GetComponent<CameraController>().SetFollowTarget(_handlingObject.transform.gameObject);
                _handlingObject = null;
                _isInteractive = true;
            }    
        }
    }
    protected override void Start(){
        base.Initalize();
    }
}

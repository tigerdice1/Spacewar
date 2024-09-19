using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console_ControlPanel : ConsoleBase
{
    // 컨트롤 할 오브젝트를 변경하는 함수
    // ConsoleBase 로부터 상속받아 작성됨

    private Electricity _electricity;
    protected override void Initalize(){
        base.Initalize();
        _electricity = this.GetComponent<Electricity>();
    }
    protected override void OnDebugMode(){
        if(!this.GetComponent<Electricity>()) Debug.Log("Electricity is not Loaded. Please add Electricity Module. Location : " + gameObject);
    }
    public void SwapContorlObject(PlayerController activatedPlayerController){
        if(_electricity.IsPowered){ // 전력이 들어와 있을 경우
            if(_isInteractive){ // 현재 콘솔이 사용가능한 상태일 때
                if(_triggeredControllers.Contains(activatedPlayerController)){
                    _electricity.SetActiveState(CustomTypes.ElectricState.ACTIVE);
                    _handlingPlayers.Add(activatedPlayerController.DefaultControlObject.GetComponent<PlayerBase>());
                    activatedPlayerController.ControlObject = _objectToControl;
                    activatedPlayerController.gameObject.GetComponent<CameraController>().SetFollowTarget(_objectToControl);
                }
                if(_soloUseOnly){
                    _isInteractive = false;
                }
            }
            else if(!_isInteractive){ // 현재 콘솔이 사용 불가능한 상태일 때
                if(_triggeredControllers.Contains(activatedPlayerController)){
                    _electricity.SetActiveState(CustomTypes.ElectricState.IDLE);
                    activatedPlayerController.ControlObject = activatedPlayerController.DefaultControlObject;
                    activatedPlayerController.gameObject.GetComponent<CameraController>().SetFollowTarget(activatedPlayerController.DefaultControlObject);
                    _handlingPlayers.Remove(activatedPlayerController.ControlObject.GetComponent<PlayerBase>());
                }
                if(_soloUseOnly){
                    _isInteractive = true;
                }
            }    
        }
        else if(!_electricity.IsPowered){ // 전력이 부족하거나 없을 경우

        }
    }
    protected override void Start(){
        base.Start();
        Initalize();
        if(GameManager.Instance().IsDebugMode){
            OnDebugMode();
        }
    }
}

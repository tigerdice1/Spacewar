using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : FixableObjects
{
    private Electricity _electricity;
    protected override void Initialize(){
        base.Initialize();
        _electricity = this.GetComponent<Electricity>();
    }
    protected void OnDebugMode(){
        if(!this.GetComponent<Electricity>()) Debug.Log("Electricity is not Loaded. Please add Electricity Module. Location : " + gameObject);
    }
    public void SwapControlObject(PlayerController activatedPlayerController){
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
    protected void Start(){
        Initialize();
        if(GameManager.Instance().IsDebugMode){
            OnDebugMode();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : FixableObjects
{
    #region Private Variables
    private Electricity _electricity;
    #endregion Private Variables

    #region Protected Methods
    protected override void Initialize(){
        base.Initialize();
        _electricity = GetComponent<Electricity>();
    }
    protected void OnDebugMode(){
        if(!GetComponent<Electricity>()) Debug.Log("Electricity is not Loaded. Please add Electricity Module. Location : " + gameObject);
    }
    #endregion Protected Methods

    #region Public Methods
    public bool SwapControlObject(PlayerController playerController){
        if(!_electricity.IsPowered){
            // 전력이 부족하거나 없을 경우 액션

            return false;
        }
        else{
            if(_isInteractive){
                // 콘솔이 사용가능할 때 액션
                if(playerController.ControlObject ==_objectToControl){
                    playerController.ControlObject = playerController.DefaultControlObject;
                }
                else{
                    playerController.ControlObject = _objectToControl;
                }
                if(_soloUseOnly) _isInteractive = false;
            }
            else{
                // 콘솔이 사용불가할 때 액션
                playerController.ControlObject = playerController.DefaultControlObject;
                if(_soloUseOnly) _isInteractive = true;
            }
        }
        playerController.gameObject.GetComponent<CameraController>().SetFollowTarget(playerController.ControlObject);
        return true;
        /*
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
            return true;
        }
        else if(!_electricity.IsPowered){ // 전력이 부족하거나 없을 경우
            return false;
        }
        return false;
        */
    }
    #endregion Public Methods

    protected void Start(){
        Initialize();
        if(GameManager.Instance().IsDebugMode){
            OnDebugMode();
        }
    }
}

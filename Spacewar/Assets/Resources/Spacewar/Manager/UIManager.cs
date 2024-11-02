using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class UIManager : MonoBehaviourPunCallbacks
{
    public bool IsOtherUIVisible;
    private UI_Base _playerUI;
    [SerializeField]
    private UI_Base _otherUI;
    private CanvasGroup _playerUICanvasGroup;
    private CanvasGroup _otherUICanvasGroup;

    public void SetPlayerUI(UI_Base playerUI){
        _playerUI = playerUI;
        _playerUICanvasGroup = _playerUI.GetComponent<CanvasGroup>();
    }

    public void SetOtherUI(UI_Base otherUI){
        _otherUI = otherUI;
        if(_otherUI == null){
            _otherUICanvasGroup = null;
            return;
        }
        else{
            _otherUICanvasGroup = _otherUI.GetComponent<CanvasGroup>();
        }
    }

    public UI_Base GetPlayerUI(){
        return _playerUI;
    }

    public UI_Base GetOtherUI(){
        return _otherUI;
    }


    public void ShowPlayerUI(bool state){
        if(_playerUICanvasGroup != null){
            _playerUICanvasGroup.interactable = state;
            _playerUICanvasGroup.blocksRaycasts = state;
            _playerUICanvasGroup.alpha = state ? 1f : 0f;
        }
    }

    public void HideObjectUI(){
        if(IsOtherUIVisible){
            _otherUICanvasGroup.interactable = false;
            _otherUICanvasGroup.alpha = 0.0f;
            _otherUICanvasGroup.blocksRaycasts= false;
            IsOtherUIVisible = false;
        }
    }
    public void SetUIVisible(bool state){
        _otherUICanvasGroup.interactable = state;
        _otherUICanvasGroup.blocksRaycasts = state;
        _otherUICanvasGroup.alpha = state ? 1f : 0f;
        IsOtherUIVisible = state;
    }

    private void Initalize(){
        UI_Player uiPlayer = _playerUI.gameObject.GetComponent<UI_Player>();
        Transform slotPosition = uiPlayer.InventorySlotList[0].transform;
        uiPlayer.InventoryPicker.transform.position = slotPosition.position;
    }
    // Start is called before the first frame update
    void Start(){
        if(photonView.IsMine){
            Initalize();
        }
    }
}

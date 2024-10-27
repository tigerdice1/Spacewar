using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public bool IsOtherUIVisible;
    public CanvasGroup PlayerUI;
    private CanvasGroup _otherUI;


    public void ShowPlayerUI(bool state){
        if(PlayerUI != null){
            PlayerUI.interactable = state;
            PlayerUI.blocksRaycasts = state;
            PlayerUI.alpha = state ? 1f : 0f;
        }
    }

    public void HideObjectUI(){
        if(IsOtherUIVisible){
            _otherUI.interactable = false;
            _otherUI.alpha = 0.0f;
            _otherUI.blocksRaycasts= false;
            _otherUI = null;
            IsOtherUIVisible = false;
        }
    }
    public void SetUIVisible(UI_Base targetUI, bool state){
        if(_otherUI = targetUI.UICanvasGroup){
            _otherUI.interactable = state;
            _otherUI.blocksRaycasts = state;
            _otherUI.alpha = state ? 1f : 0f;
            IsOtherUIVisible = state;
            _otherUI = state ? _otherUI : null;
        }
        
    }
    public void MoveInventoryPicker(int number){
        UI_Player uiPlayer = PlayerUI.gameObject.GetComponent<UI_Player>();
        // 0 을 10번째 인덱스로 보정하기 위한 계산식
        int slotIndex = (number + 9) % 10;
        Transform slotPosition = uiPlayer.InventorySlotList[slotIndex].transform;
        uiPlayer.InventoryPicker.transform.position = slotPosition.position;
    }
    private void Initalize(){
        PlayerUI = gameObject.GetComponent<PlayerController>().PlayerUI.GetComponent<CanvasGroup>();
        UI_Player uiPlayer = PlayerUI.gameObject.GetComponent<UI_Player>();
        Transform slotPosition = uiPlayer.InventorySlotList[0].transform;
        uiPlayer.InventoryPicker.transform.position = slotPosition.position;
    }
    // Start is called before the first frame update
    void Start(){
        Initalize();
    }
}

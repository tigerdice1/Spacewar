using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _playerUI;
    private CanvasGroup _otherUI;
    private bool _isUIActivated;
    // Start is called before the first frame update
    void Start(){
        _playerUI = gameObject.GetComponent<PlayerController>().PlayerUI.GetComponent<CanvasGroup>();
    }

    public void SetPlayerUIState(bool state){
        if(_playerUI && state){
            _playerUI.interactable = state;
            _playerUI.blocksRaycasts = state;
            _playerUI.alpha = 1.0f;
        }
        else if(_playerUI && !state){
            _playerUI.interactable = state;
            _playerUI.blocksRaycasts = state;
            _playerUI.alpha = 0.0f;
        }
    }

    public bool GetUIActivated(){
        return _isUIActivated;
    }

    public void ReleaseUI(){
        if(_isUIActivated){
            _otherUI.interactable = false;
            _otherUI.alpha = 0.0f;
            _isUIActivated = false;
            _otherUI.blocksRaycasts= false;
            _otherUI = null;
        }
    }
    public void SetUIState(GameObject ui, bool state){
        if(_otherUI = ui.GetComponent<CanvasGroup>()){
            if(state){
                _otherUI.interactable = true;
                _otherUI.blocksRaycasts = true;
                _otherUI.alpha = 1.0f;
                _isUIActivated = true;
            }
            else if(!state){
                _otherUI.interactable = false;
                _otherUI.blocksRaycasts = false;
                _otherUI.alpha = 0.0f;
                _isUIActivated = false;
                _otherUI = null;
            }
        }
        
    }
    public void MoveInventoryPicker(int number){
        UI_Player uiPlayer = _playerUI.gameObject.GetComponent<UI_Player>();
        int slotIndex = (number + 9) % 10;
        Transform slotPosition = uiPlayer.InventorySlotList[slotIndex].transform;
        uiPlayer.InventoryPicker.transform.position = slotPosition.position;
    }
}

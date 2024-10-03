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
        Transform slotPosition = _playerUI.gameObject.GetComponent<UI_Player>().InventorySlotList[9].transform;
        switch(number){
            case 0:
                slotPosition = _playerUI.gameObject.GetComponent<UI_Player>().InventorySlotList[9].transform;
            break;
            case 1:
                slotPosition = _playerUI.gameObject.GetComponent<UI_Player>().InventorySlotList[0].transform;
            break;
            case 2:
                slotPosition = _playerUI.gameObject.GetComponent<UI_Player>().InventorySlotList[1].transform;
            break;
            case 3:
                slotPosition = _playerUI.gameObject.GetComponent<UI_Player>().InventorySlotList[2].transform;
            break;
            case 4:
                slotPosition = _playerUI.gameObject.GetComponent<UI_Player>().InventorySlotList[3].transform;
            break;
            case 5:
                slotPosition = _playerUI.gameObject.GetComponent<UI_Player>().InventorySlotList[4].transform;
            break;
            case 6:
                slotPosition = _playerUI.gameObject.GetComponent<UI_Player>().InventorySlotList[5].transform;
            break;
            case 7:
                slotPosition = _playerUI.gameObject.GetComponent<UI_Player>().InventorySlotList[6].transform;
            break;
            case 8:
                slotPosition = _playerUI.gameObject.GetComponent<UI_Player>().InventorySlotList[7].transform;
            break;
            case 9:
                slotPosition = _playerUI.gameObject.GetComponent<UI_Player>().InventorySlotList[8].transform;
            break;
        }
        _playerUI.gameObject.GetComponent<UI_Player>().InventoryPicker.transform.position = slotPosition.position;
    }
}

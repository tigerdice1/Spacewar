using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _playerUI;
    private CanvasGroup _canvasGroup;
    private bool _isUIActivated;
    // Start is called before the first frame update
    void Start(){
        Transform playerUI;
        if(playerUI = transform.Find("PlayerUI")){
            _playerUI = playerUI.GetComponent<CanvasGroup>();
        }
    }

    public void SetPlayerUIState(bool state){
        if(_playerUI && state){
            _playerUI.interactable = state;
            _playerUI.alpha = 1.0f;
        }
        else if(_playerUI && !state){
            _playerUI.interactable = state;
            _playerUI.alpha = 0.0f;
        }
    }
    public void SetUIState(GameObject ui, bool state){
        if(_canvasGroup = ui.GetComponent<CanvasGroup>()){
            if(state){
                _canvasGroup.interactable = true;
                _canvasGroup.alpha = 1.0f;
                _isUIActivated = true;
            }
            else if(!state){
                _canvasGroup.interactable = false;
                _canvasGroup.alpha = 0.0f;
                _isUIActivated = false;
            }
        }
        
    }
}

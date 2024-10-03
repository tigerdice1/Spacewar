using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Player : MonoBehaviour
{
    public PlayerController OwnController;
    public Transform InventoryPicker;
    public List<UI_InventorySlot> InventorySlotList = new List<UI_InventorySlot>();
    private PlayerBase _ownPlayer;
    private Transform _playerUI;
    private Transform _inventory;

    public void MoveInventoryPicker(Transform position){
         
    }

    // Start is called before the first frame update
    void Start(){
        _ownPlayer = OwnController.DefaultControlObject.GetComponent<PlayerBase>();
        _playerUI = transform.Find("PlayerUI");
        _inventory = _playerUI.transform.Find("Inventory");
        InventoryPicker = _playerUI.transform.Find("InventoryPicker");
        int childCount = _inventory.childCount;

        for (int i = 0; i < childCount; i++){
            Transform child = _inventory.GetChild(i);
            InventorySlotList.Add(child.GetComponent<UI_InventorySlot>());
        }
    }

    // Update is called once per frame
    void Update(){
        if(_ownPlayer.Inventory.Count != 0){
            for(int i = 0; i < _ownPlayer.Inventory.Count; i++){
                InventorySlotList[i].itemData = _ownPlayer.Inventory[i];
            }
        }
    }

}

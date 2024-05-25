using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private InventoryPage _inventoryUI;

    [SerializeField]
    private Inventory _inventoryData;

    private void Start(){
        PrepareUI();
        //_inventoryData.Initialize();
    }

    private void PrepareUI()
    {
        _inventoryUI.Initialize(_inventoryData.GetSize);
        this._inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
        this._inventoryUI.OnSwapItems += HandleSwapItems;
        this._inventoryUI.OnStartDragging += HandleDragging;
        this._inventoryUI.OnItemActionRequested += HandleItemActionRequest;
    }
    private void HandleItemActionRequest(int itemIndex){

    }

    private void HandleDragging(int itemIndex){

    }

    private void HandleSwapItems(int itemIndex_1, int itemIndex_2){

    }

    private void HandleDescriptionRequest(int itemIndex){
        ItemSlot _itemSlot = _inventoryData.GetItemAt(itemIndex);
        if(_itemSlot.GetIsEmpty)
        {
            _inventoryUI.ResetSelection();
            return;
        }
            
        Item _item = _itemSlot.Item;
        _inventoryUI.UpdateDescription(itemIndex,_item.ItemImage,_item.Name,_item.Description);
    }

    private void Update(){ //작동 테스트
        if(Input.GetKeyDown(KeyCode.I)){
            ToggleInventory();
        }
    }
    public void ToggleInventory()
    {
        if (!_inventoryUI.isActiveAndEnabled)
        {
            _inventoryUI.Show();
            foreach(var item in _inventoryData.GetCurrentInventoryState()){
                _inventoryUI.UpdateData(item.Key,
                    item.Value.Item.ItemImage,
                    item.Value.Quantity);
            }
        }
        else
        {
            _inventoryUI.Hide();
        }
    }
}


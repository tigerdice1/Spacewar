using PlayerInven.Model;
using PlayerInven.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace PlayerInven
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField]
        private InventoryPage _inventoryUI;

        [SerializeField]
        private Inventory _inventoryData;

        [SerializeField]
        private List<ItemSlot> _initialSlots = new List<ItemSlot>();

        private void Start(){
            PrepareUI();
            PrepareInvenData();
        }

        private void PrepareInvenData(){
            _inventoryData.Initialize();
            _inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach(var item in _initialSlots)
            {
                if(item.GetIsEmpty)
                    continue;
                _inventoryData.AddItem(item);
            }
        }
        private void UpdateInventoryUI(Dictionary<int, ItemSlot>inventoryState){
            _inventoryUI.ResetAllItems();
            foreach(var item in inventoryState)
            {
                _inventoryUI.UpdateData(item.Key,
                item.Value.Item.ItemImage,
                item.Value.Quantity);
            }
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
            ItemSlot _itemSlot = _inventoryData.GetItemAt(itemIndex);
            if(_itemSlot.GetIsEmpty){
                return;
            }
            IItemAction itemAction = _itemSlot.Item as IItemAction;
            if(itemAction != null){
                itemAction.PerformAction(gameObject,null);
            }
            IDestroyableItem destroyableItem = _itemSlot.Item as IDestroyableItem;
            if(destroyableItem != null){
                _inventoryData.RemoveItem(itemIndex,1);
            }
        }

        private void HandleDragging(int itemIndex){
            ItemSlot _itemSlot = _inventoryData.GetItemAt(itemIndex);
            if(_itemSlot.GetIsEmpty)
                return;
            _inventoryUI.CreateDraggedItem(_itemSlot.Item.ItemImage,_itemSlot.Quantity);
        }

        private void HandleSwapItems(int itemIndex_1, int itemIndex_2){
            _inventoryData.SwapItems(itemIndex_1,itemIndex_2);
        }

        private void HandleDescriptionRequest(int itemIndex){
            ItemSlot _itemSlot = _inventoryData.GetItemAt(itemIndex);
            if(_itemSlot.GetIsEmpty)
            {
                _inventoryUI.ResetSelection();
                return;
            }
                
            Item _item = _itemSlot.Item;
            string _description = PrepareDescription(_itemSlot);
            _inventoryUI.UpdateDescription(itemIndex,_item.ItemImage,_item.Name,_description);
        }

        private string PrepareDescription(ItemSlot itemSlot){
            StringBuilder sb =new StringBuilder();
            sb.Append(itemSlot.Item.Description);
            sb.AppendLine();
            for(int i =0;i < itemSlot.ItemState.Count;i++){
                sb.Append($"{itemSlot.ItemState[i].ItemParameter.ParamName}"+
                $":{itemSlot.ItemState[i].Value}/" + 
                $"{itemSlot.Item.DefaultParameterList[i].Value}");
                sb.AppendLine();
            }
            return sb.ToString();
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
}

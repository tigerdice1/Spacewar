using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerInven.UI
{
    public class InventoryPage : MonoBehaviour
    {
        [SerializeField]
        private InventoryItem _itemPrefab;

        [SerializeField]
        private RectTransform _contentPanel;

        [SerializeField]
        private InventoryDescription _itemDescription;

        [SerializeField]
        private MouseFollower _mouseFollower;

        List<InventoryItem> _listOfItems = new List<InventoryItem>();

        private int _currentlyDraggedItemIndex = -1;

        public event Action<int> OnDescriptionRequested,
                OnItemActionRequested,
                OnStartDragging;

        public event Action<int,int> OnSwapItems;

        [SerializeField]
        private ItemActionPanel _actionPanel;

        private void Awake(){
            Hide();
            _mouseFollower.Toggle(false);
            _itemDescription.ResetDescription();
        }

        public void Initialize(int inventorysize){
            for(int i =0; i < inventorysize; i++)
            {
                InventoryItem _item = Instantiate(_itemPrefab,Vector3.zero,Quaternion.identity);
                _item.transform.SetParent(_contentPanel);
                _listOfItems.Add(_item);
                _item.OnItemClicked += HandleItemSelection;
                _item.OnItemBeginDrag += HandleBeginDrag;
                _item.OnItemDroppedOn += HandleSwap;
                _item.OnItemEndDrag += HandleEndDrag;
                _item.OnRightMouseBtnClick += HandleShowItemActions;
            }
        }

        public void ResetAllItems(){
            foreach(var item in _listOfItems)
            {
                item.ResetData();
                item.Deselect();
            }
        }

        public void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description){
            _itemDescription.SetDescription(itemImage,name,description);
            DeselectAllItems();
            _listOfItems[itemIndex].Select();
        }

        public void UpdateData(int itemIndex,Sprite itemImage, int itemQuantity){
            if(_listOfItems.Count > itemIndex)
            {
                _listOfItems[itemIndex].SetData(itemImage,itemQuantity);
            }
        }

        private void HandleShowItemActions(InventoryItem inventoryItemUI){
            int _index = _listOfItems.IndexOf(inventoryItemUI);
            if(_index == -1){
                return;
            }
            OnItemActionRequested?.Invoke(_index);
        }

        private void HandleEndDrag(InventoryItem inventoryItemUI){
            ResetDraggedItem();
        }

        private void HandleSwap(InventoryItem inventoryItemUI){
            int _index = _listOfItems.IndexOf(inventoryItemUI);
            if(_index == -1){
                return;
            }
            OnSwapItems?.Invoke(_currentlyDraggedItemIndex,_index);
            HandleItemSelection(inventoryItemUI);
        }

        public void ResetDraggedItem(){
            _mouseFollower.Toggle(false);
            _currentlyDraggedItemIndex = -1;
        }

        private void HandleBeginDrag(InventoryItem inventoryItemUI){
            int _index = _listOfItems.IndexOf(inventoryItemUI);
            if(_index == -1){
                return;
            }
            _currentlyDraggedItemIndex = _index;
            HandleItemSelection(inventoryItemUI);
            OnStartDragging?.Invoke(_index);
        }

        public void CreateDraggedItem(Sprite sprite, int quantity){
            _mouseFollower.Toggle(true);
            _mouseFollower.SetData(sprite,quantity);
        }

        private void HandleItemSelection(InventoryItem inventoryItemUI){
            int _index = _listOfItems.IndexOf(inventoryItemUI);
            if (_index == -1)
                    return;
            OnDescriptionRequested?.Invoke(_index);
        }
        //인벤토리창 on/off
        public void Show()
        {
            gameObject.SetActive(true);
            ResetSelection();
        }

        public void ResetSelection(){
            _itemDescription.ResetDescription();
            DeselectAllItems();
        }

        public void AddAction(string actionName, Action performAction){
            _actionPanel.AddBtn(actionName,performAction);
        }

        public void ShowItemAction(int itemIndex){
            _actionPanel.Toggle(true);
            _actionPanel.transform.position = _listOfItems[itemIndex].transform.position;
        }

        private void DeselectAllItems(){
            foreach(InventoryItem item in _listOfItems){
                item.Deselect();
            }
            _actionPanel.Toggle(false);
        }
        public void Hide()
        {
            gameObject.SetActive(false);
            ResetDraggedItem();
        }
    }
}
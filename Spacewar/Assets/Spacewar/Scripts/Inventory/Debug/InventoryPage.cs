using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void UpdateData(int itemIndex,Sprite itemImage, int itemQuantity){
        if(_listOfItems.Count > itemIndex)
        {
            _listOfItems[itemIndex].SetData(itemImage,itemQuantity);
        }
    }

    private void HandleShowItemActions(InventoryItem inventoryItemUI){

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

    private void ResetSelection(){
        _itemDescription.ResetDescription();
        DeselectAllItems();
    }
    private void DeselectAllItems(){
        foreach(InventoryItem item in _listOfItems){
            item.Deselect();
        }
    }
    public void Hide()
    {
        gameObject.SetActive(false);
        ResetDraggedItem();
    }
}

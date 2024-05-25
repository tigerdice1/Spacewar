using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    [SerializeField]
    private List<ItemSlot> _inventoryItems;
    [SerializeField]
    private int _size = 4;

    public int GetSize{
        get => _size;
    }

    public void Initialize(){
        _inventoryItems = new List<ItemSlot>();
        for (int i =0;i<_size;i++)
        {
            _inventoryItems.Add(ItemSlot.GetEmptyItem());
        }
    }

    public void AddItem(Item item,int quantity){
        for (int i =0;i<_inventoryItems.Count;i++){
            if(_inventoryItems[i].GetIsEmpty){
                _inventoryItems[i] = new ItemSlot{
                    Item = item,
                    Quantity= quantity
                };
            }
        }
    }

    public Dictionary<int, ItemSlot> GetCurrentInventoryState(){
        Dictionary<int, ItemSlot> returnValue =
            new Dictionary<int, ItemSlot>();
        for(int i  =0; i<_inventoryItems.Count;i++){
            if(_inventoryItems[i].GetIsEmpty)
                continue;
            returnValue[i] = _inventoryItems[i];
        }
        return returnValue;
    }
    public ItemSlot GetItemAt(int itemIndex){
        return _inventoryItems[itemIndex];
    }
}
[Serializable]
public struct ItemSlot
{
    [SerializeField]
    private int _quantity;
    [SerializeField]
    private Item _item;
    private bool _isEmpty => _item == null;

    public int Quantity{
        get => _quantity;
        set => _quantity = value;
    }
    public Item Item{
        get => _item;
        set => _item =value;
    }
    public bool GetIsEmpty{
        get =>_isEmpty;
    }


    public ItemSlot ChangeQuantity(int newQuantity){
        return new ItemSlot{
            _item = this._item,
            _quantity = newQuantity,
        };
    }
    
    public static ItemSlot GetEmptyItem()
        => new ItemSlot
        {
            _item = null,
            _quantity = 0,
        };
}

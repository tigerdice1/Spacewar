using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlayerInven.Model
{
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

        public event Action<Dictionary<int,ItemSlot>> OnInventoryUpdated;

        public void Initialize(){
            _inventoryItems = new List<ItemSlot>();
            for (int i =0;i<_size;i++)
            {
                _inventoryItems.Add(ItemSlot.GetEmptyItem());
            }
        }

        public int AddItem(Item item,int quantity){
            if(!item.GetIsStackable)
            {
                for (int i =0;i<_inventoryItems.Count;i++){
                    while(quantity > 0 && !IsInventoryFull()){
                        quantity -= AddItemToFirstFreeSlot(item,1);
                    }
                    InformAboutChange();
                    return quantity;
                }
            }
            quantity = AddStackableItem(item,quantity);
            InformAboutChange();
            return quantity;
        }

        private int AddItemToFirstFreeSlot(Item item,int quantity){
            ItemSlot newItem = new ItemSlot{
                Item = item,
                Quantity= quantity
            };
            
            for(int i =0;i<_inventoryItems.Count;i++){
                if(_inventoryItems[i].GetIsEmpty){
                    _inventoryItems[i] = newItem;
                    return quantity;
                }
            }
            return 0;
        }

        private bool IsInventoryFull()
            => !_inventoryItems.Where(Item =>Item.GetIsEmpty).Any();

        private int AddStackableItem(Item item, int quantity){
            for(int i =0;i<_inventoryItems.Count;i++){
                if(_inventoryItems[i].GetIsEmpty){
                    continue;
                }
                if(_inventoryItems[i].Item.GetID == item.GetID){
                    int amountPossibleToTake =
                    _inventoryItems[i].Item.MaxStackSize - _inventoryItems[i].Quantity;
                    if(quantity>amountPossibleToTake){
                        _inventoryItems[i] = _inventoryItems[i]
                            .ChangeQuantity(_inventoryItems[i].Item.MaxStackSize);
                        quantity -= amountPossibleToTake;
                    }
                    else{
                        _inventoryItems[i] = _inventoryItems[i]
                            .ChangeQuantity(_inventoryItems[i].Quantity + quantity);
                        InformAboutChange();
                        return 0;
                    }
                }
            }
            while(quantity>0 && !IsInventoryFull()){
                int newQuantity = Mathf.Clamp(quantity,0,item.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(item,newQuantity);
            }
            return quantity;
        }

        public void AddItem(ItemSlot item)
        {
            AddItem(item.Item,item.Quantity);
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

        public void SwapItems(int itemIndex_1,int itemIndex_2){
            ItemSlot item1 = _inventoryItems[itemIndex_1];
            _inventoryItems[itemIndex_1] = _inventoryItems[itemIndex_2];
            _inventoryItems[itemIndex_2] = item1;
            InformAboutChange(); // 변경됐다는것을 알리는 함수
        }

        private void InformAboutChange(){
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
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
}
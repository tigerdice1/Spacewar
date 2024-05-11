using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InventorySystem : MonoBehaviour
{
    [System.Serializable]
    public class InventorySlot{
        private int _id = -1;
        private Item _item;

        //아이템의 양
        private int _itemAmount;

        //인벤토리슬롯이 생성될때 일부 값을 설 정하는 생성자
        public InventorySlot(int id, Item item, int amount){
            this._id = id;
            this._item = item;
            this._itemAmount = amount;
            
        }
        public int ID{
            set {_id = _id == -1 ? value : _id; }
            get {if(_id != -1) return _id; else return -1; }
        }

        public Item GetItem{
            get => _item;
        }

        public int GetItemAmount{
            get => _itemAmount;
        }

        public void AddAmount(int value){
            this._itemAmount += value; 
        }
    }
}

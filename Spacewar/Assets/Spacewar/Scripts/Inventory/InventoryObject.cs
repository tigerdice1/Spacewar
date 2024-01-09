using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject{
   public List<InventorySlot> Container = new List<InventorySlot>();
    //인벤토리에 항목 추가 함수
    public void AddItem(ItemObject item, int amount){
        bool hasItem = false;
        for (int i = 0; i < Container.Count; i++){
            if (Container[i]._item == item){
                Container[i].AddAmount(amount);
                hasItem = true;
                break;
            }
        }
        if (!hasItem){
            Container.Add(new InventorySlot(item, amount));
        }
    }
}

[System.Serializable]
public class InventorySlot{
    public ItemObject _item;

    //아이템의 양
    public int _itemAmount;

    //인벤토리슬롯이 생성될때 일부 값을 설정하는 생성자
    public InventorySlot(ItemObject item, int amount){
        _item = item;
        _itemAmount = amount;
    }
    public void AddAmount(int value){
        _itemAmount += value; 
    }
}


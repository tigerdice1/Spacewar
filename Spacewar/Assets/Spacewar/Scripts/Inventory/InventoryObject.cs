using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string _savePath;
    public ItemDatabaseObject _database;
    public Inventory _container;

    //인벤토리에 항목 추가 함수
    public void AddItem(Item item, int amount){
        // 이미 인벤토리에 같은 아이템이 있다면, 그 아이템의 수량만 증가
        for (int i = 0; i < _container._items.Count; i++){
            if (_container._items[i]._item == item){
                _container._items[i].AddAmount(amount);
                return;
            }
        }
        //같은 아이템이 인벤토리에 없다면, 새로운 슬롯을 생성하여 추가
            _container._items.Add(new InventorySlot(item._id,item, amount));
    }
    [ContextMenu("Save")]
    public void Save()
    {
        //string saveData = JsonUtility.ToJson(this, true);
        //BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Create(string.Concat(Application.persistentDataPath, _savePath));
        //bf.Serialize(file, saveData);
        //file.Close();

        IFormatter formatter = new BinaryFormatter();
        Stream _stream = new FileStream(string.Concat(Application.persistentDataPath, _savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(_stream, _container);
        _stream.Close();
    }
    [ContextMenu("Load")]
    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, _savePath))){
            //BinaryFormatter bf  = new BinaryFormatter();
            //FileStream file = File.Open(string.Concat(Application.persistentDataPath, _savePath), System.IO.FileMode.Open);
            //JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            //file.Close();

            IFormatter formatter = new BinaryFormatter();
            Stream _stream = new FileStream(string.Concat(Application.persistentDataPath, _savePath), FileMode.Open, FileAccess.Read);
            _container = (Inventory)formatter.Deserialize(_stream);
            _stream.Close();
        }
    }
    [ContextMenu("Clear")]
    public void clear()
    {
        _container = new Inventory();
    }

    // 역직렬화 후에 호출되는 메서드. 각 슬롯에 저장된 아이템 ID를 이용하여 아이템 오브젝트를 찾아 저장


}
[System.Serializable]
public class Inventory
{
    public List<InventorySlot> _items = new List<InventorySlot>();
}
[System.Serializable]
public class InventorySlot{

    public int _id;
    public Item _item;

    //아이템의 양
    public int _itemAmount;

    //인벤토리슬롯이 생성될때 일부 값을 설정하는 생성자
    public InventorySlot(int id,Item item, int amount){
        _id = id;
        _item = item;
        _itemAmount = amount;
        
    }
    public void AddAmount(int value){
        _itemAmount += value; 
    }
}


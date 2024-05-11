using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;


[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject{
    public string _savePath;
    [SerializeField]
    private ItemDB _db;
    [SerializeField]
    private List<InventorySlot> _inventorySlotList = new List<InventorySlot>();

    public ItemDB DB{
        set => _db = value;
        get => _db;
    }
    public List<InventorySlot> InventorySlotList{
        set => _inventorySlotList = value;
        get => _inventorySlotList;
    }

    //인벤토리에 항목 추가 함수
    public void AddItem(Item item, int amount){
        //버프가 있다면 수량증가 시키지않음 (EpicItem)
        //if(item._buffs.Count > 0){
        if(item._buffs.Length > 0){
            _container._items.Add(new InventorySlot(item._id, item, amount));
            return;
        }

        // 이미 인벤토리에 같은 아이템이 있다면, 그 아이템의 수량만 증가
        for (int i = 0; i < _container._items.Count; i++){
            if (_container._items[i].GetItem._id == item._id){
                _container._items[i].AddAmount(amount);
                return;
            }
        }
        //같은 아이템이 인벤토리에 없다면, 새로운 슬롯을 생성하여 추가
        _container._items.Add(new InventorySlot(item._id, item, amount));
    }
    [ContextMenu("Save")]
    public void Save(){
        //저장하면 AppData\LocalLow\DefaultCompany\Spacewar에 저장됨.
        IFormatter formatter = new BinaryFormatter();
        Stream _stream = new FileStream(string.Concat(Application.persistentDataPath, _savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(_stream, _container);
        _stream.Close();
    }
    [ContextMenu("Load")]
    public void Load(){
        if (File.Exists(string.Concat(Application.persistentDataPath, _savePath))){
            IFormatter formatter = new BinaryFormatter();
            Stream _stream = new FileStream(string.Concat(Application.persistentDataPath, _savePath), FileMode.Open, FileAccess.Read);
            _container = (Inventory)formatter.Deserialize(_stream);
            _stream.Close();
        }
    }
    [ContextMenu("Clear")]
    public void clear(){
        _container = new Inventory();
    }

    // 역직렬화 후에 호출되는 메서드. 각 슬롯에 저장된 아이템 ID를 이용하여 아이템 오브젝트를 찾아 저장


} 



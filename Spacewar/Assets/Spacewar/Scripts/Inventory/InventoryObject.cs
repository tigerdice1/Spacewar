using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
    public string savePath;
    private ItemDatabaseObject database;
    public List<InventorySlot> Container = new List<InventorySlot>();

    private void OnEnable()
    {
#if UNITY_EDITOR
        database = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/SpaceWar/Resorces/Database.asset", typeof(ItemDatabaseObject));
#else
        database = Resources.Load<ItemDatabaseObject>("Database");
#endif
    }
    //인벤토리에 항목 추가 함수
    public void AddItem(ItemObject item, int amount){
        // 이미 인벤토리에 같은 아이템이 있다면, 그 아이템의 수량만 증가
        for (int i = 0; i < Container.Count; i++){
            if (Container[i]._item == item){
                Container[i].AddAmount(amount);
                return;
            }
        }
        //같은 아이템이 인벤토리에 없다면, 새로운 슬롯을 생성하여 추가
            Container.Add(new InventorySlot(database.GetId[item],item, amount));
    }
    public void Save()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();
    }
    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath))){
            BinaryFormatter bf  = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), System.IO.FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
        }
    }


    // 역직렬화 후에 호출되는 메서드. 각 슬롯에 저장된 아이템 ID를 이용하여 아이템 오브젝트를 찾아 저장
    public void OnAfterDeserialize()
    {
       for(int i = 0;i < Container.Count; i++)
        {
            Container[i]._item = database.GetItem[Container[i]._id];
        }
    }

    public void OnBeforeSerialize()
    {
       
    }
}

[System.Serializable]
public class InventorySlot{

    public int _id;
    public ItemObject _item;

    //아이템의 양
    public int _itemAmount;

    //인벤토리슬롯이 생성될때 일부 값을 설정하는 생성자
    public InventorySlot(int id,ItemObject item, int amount){
        _id = id;
        _item = item;
        _itemAmount = amount;
        
    }
    public void AddAmount(int value){
        _itemAmount += value; 
    }
}


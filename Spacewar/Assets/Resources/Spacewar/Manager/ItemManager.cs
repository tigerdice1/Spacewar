using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<GameObject> ItemList = new List<GameObject>();
    private static ItemManager _instance;
    public static ItemManager Instance(){
        return _instance;
    }

    public GameObject FindItem(int itemType){
        foreach(GameObject item in ItemList){
            int targetItem = item.GetComponent<PickableItem>().Item.ItemType;
            if(targetItem == itemType) return item;
        }
        return null;
    }
    public void DropItem(int index, PlayerBase itemUser){
        CustomTypes.ItemData item = itemUser.Inventory[index];
        if(item.ItemType != 0){
            GameObject spawnedItem = Instantiate(FindItem(item.ItemType), itemUser.transform.position, itemUser.transform.rotation);
            item.ClearItemData();
        }
    }
    public void UseItem(int index, GameObject targetObject, PlayerBase itemUser){
        if(targetObject == null) return;
        CustomTypes.ItemData item = itemUser.Inventory[index];
        var powerGenerator = targetObject.GetComponent<PowerGenerator>();
        var controlPanel = targetObject.GetComponent<ControlPanel>();
        var junction = targetObject.GetComponent<Junction>();
        if(item.ItemType == 1 && powerGenerator != null){
            powerGenerator.FixObject(100f);
        }
        if(item.ItemType == 2 && controlPanel != null){
            controlPanel.FixObject(100f);
        }
        if(item.ItemType == 2 && junction != null){
            junction.FixObject(100f);
        }
        if(item.ItemType == 3 && powerGenerator != null){
            powerGenerator.FillFuel();
            item.ClearItemData();
        }
    }
    void Awake(){
    if(_instance == null){
        _instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

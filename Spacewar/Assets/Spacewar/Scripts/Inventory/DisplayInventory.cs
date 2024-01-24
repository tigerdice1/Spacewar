using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class DisplayInventory : MonoBehaviour
{
    //인벤토리를 찾기 위해 플레이어에 연결 할 필요 없고 편집기 내에서
    //각각의 인벤토리에 직접 연결 할 수 있다.
    public InventoryObject inventory;

    public int X_START; //아이템 시작위치
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM; //아이템 사이의 간격
    public int NUMBER_OF_COLUMN;
    public int Y_SPACE_BETWEEN_ITEMS;
    Dictionary<InventorySlot,GameObject> itemsDisplayed = new Dictionary<InventorySlot,GameObject>(); // 아이템추가 시

    // Start is called before the first frame update
    void Start(){
        CreateDisplay();
    }

    // Update is called once per frame
    void Update(){
        UpdateDisplay();
    }
    public void UpdateDisplay(){
        for (int i = 0; i< inventory.Container.Count; i++){
            if (itemsDisplayed.ContainsKey(inventory.Container[i])){
                itemsDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i]._itemAmount.ToString("n0");
            }
            else{
                var obj = Instantiate(inventory.Container[i]._item._prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i]._itemAmount.ToString("n0");
                itemsDisplayed.Add(inventory.Container[i], obj);
            }
        }
    }
    public void CreateDisplay(){ 
        for (int i = 0;i<inventory.Container.Count; i++){
            var obj = Instantiate(inventory.Container[i]._item._prefab,Vector3.zero,Quaternion.identity,transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i]._itemAmount.ToString("n0");
            itemsDisplayed.Add(inventory.Container[i], obj);
        }
    }
    public Vector3 GetPosition(int i){
        return new Vector3(X_START +(X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START+(-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMN)), 0f);
    }
}

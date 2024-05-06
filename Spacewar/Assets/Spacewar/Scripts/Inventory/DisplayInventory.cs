using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour{
    [SerializeField]
    private GameObject _inventoryPrefab;
    //인벤토리를 찾기 위해 플레이어에 연결 할 필요 없고 편집기 내에서
    //각각의 인벤토리에 직접 연결 할 수 있다.
    [SerializeField]
    private InventoryObject _inventory;

    private int _xStart; // 아이템 시작 위치
    private int _yStart;
    private int _xSpaceBetweenItem; // 아이템 사이의 간격
    private int _numberOfColumn;
    private int _ySpaceBetweenItems;

    Dictionary<InventorySlot,GameObject> _itemsDisplayed = new Dictionary<InventorySlot,GameObject>(); // 아이템추가 시

    // Start is called before the first frame update
    void Start(){
        CreateDisplay();
    }

    // Update is called once per frame
    void Update(){
        UpdateDisplay();
    }
    void UpdateDisplay(){
        for (int i = 0; i < _inventory.Container._items.Count; i++){
            InventorySlot slot = _inventory.Container._items[i];
            if (_itemsDisplayed.ContainsKey(slot)){
                _itemsDisplayed[slot].GetComponentInChildren<TextMeshProUGUI>().text = slot._itemAmount.ToString("n0");
            }
            else{
                var obj = Instantiate(_inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
                //Debug.Log(_inventory._database._getItem[slot._item._id]._uiDisplay != null ? "Valid display sprite" : "Display sprite is null");
                obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _inventory.Database._getItem[slot._item._id]._uiDisplay;
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = slot._itemAmount.ToString("n0");
                _itemsDisplayed.Add(slot, obj);
            }
        }
    }
    void CreateDisplay(){
        for (int i = 0; i < _inventory.Container._items.Count; i++){
            InventorySlot slot = _inventory.Container._items[i];
            var obj = Instantiate(_inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _inventory.Database._getItem[slot._item._id]._uiDisplay;
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = slot._itemAmount.ToString("n0");

            _itemsDisplayed.Add(slot, obj);
        }
    }
    private Vector3 GetPosition(int i){
        _xStart = -300;
        _yStart = 0;
        _xSpaceBetweenItem = 100;
        _ySpaceBetweenItems = 0;
        _numberOfColumn = 4; //줄당 아이템 수
        return new Vector3(_xStart + (_xSpaceBetweenItem * (i % _numberOfColumn)), _yStart + (-_ySpaceBetweenItems * (i / _numberOfColumn)), 0f);
    }
}

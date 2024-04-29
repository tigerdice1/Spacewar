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

    public GameObject InventoryPrefab{
        get {return _inventoryPrefab;}
        set {_inventoryPrefab = value;}
    }
    public InventoryObject Inventory{
        get {return _inventory;}
        set {_inventory = value;}
    }

    class ItemInventorySlot{
    private int _xStart; // 아이템 시작 위치
    private int _yStart;
    private int _xSpaceBetweenItem; // 아이템 사이의 간격
    private int _numberOfColumn;
    private int _ySpaceBetweenItems;

    public int XStart{
        get { return _xStart; }
        set { _xStart = value; }
    }

    public int YStart{
        get { return _yStart; }
        set { _yStart = value; }
    }

    public int XSpaceBetweenItem{
        get { return _xSpaceBetweenItem; }
        set { _xSpaceBetweenItem = value; }
    }

    public int NumberOfColumn{
        get { return _numberOfColumn; }
        set { _numberOfColumn = value; }
    }

    public int YSpaceBetweenItems{
        get { return _ySpaceBetweenItems; }
        set { _ySpaceBetweenItems = value; }
    }
}
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
        for (int i = 0; i < Inventory.Container._items.Count; i++){
            InventorySlot slot = Inventory.Container._items[i];
            if (_itemsDisplayed.ContainsKey(slot)){
                _itemsDisplayed[slot].GetComponentInChildren<TextMeshProUGUI>().text = slot._itemAmount.ToString("n0");
            }
            else{
                var obj = Instantiate(InventoryPrefab, Vector3.zero, Quaternion.identity, transform);
                //Debug.Log(_inventory._database._getItem[slot._item._id]._uiDisplay != null ? "Valid display sprite" : "Display sprite is null");
                obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = Inventory.Database._getItem[slot._item._id]._uiDisplay;
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = slot._itemAmount.ToString("n0");
                _itemsDisplayed.Add(slot, obj);
            }
        }
    }
    void CreateDisplay(){
        for (int i = 0; i < Inventory.Container._items.Count; i++){
            InventorySlot slot = Inventory.Container._items[i];
            var obj = Instantiate(InventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = Inventory.Database._getItem[slot._item._id]._uiDisplay;
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = slot._itemAmount.ToString("n0");

            _itemsDisplayed.Add(slot, obj);
        }
    }
    private Vector3 GetPosition(int i){
        ItemInventorySlot _slot = new ItemInventorySlot();
        _slot.XStart = -300;
        _slot.YStart = 0;
        _slot.XSpaceBetweenItem = 100;
        _slot.YSpaceBetweenItems = 0;
        _slot.NumberOfColumn = 4; //줄당 아이템 수
        return new Vector3(_slot.XStart + (_slot.XSpaceBetweenItem * (i % _slot.NumberOfColumn)), _slot.YStart + (-_slot.YSpaceBetweenItems * (i / _slot.NumberOfColumn)), 0f);
    }
}

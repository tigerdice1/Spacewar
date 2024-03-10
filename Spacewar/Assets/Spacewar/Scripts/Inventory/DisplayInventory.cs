using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    public GameObject _inventoryPrefab;
    //인벤토리를 찾기 위해 플레이어에 연결 할 필요 없고 편집기 내에서
    //각각의 인벤토리에 직접 연결 할 수 있다.
    public InventoryObject _inventory;

    public int _X_START; //아이템 시작위치
    public int _Y_START;
    public int _X_SPACE_BETWEEN_ITEM; //아이템 사이의 간격
    public int _NUMBER_OF_COLUMN;
    public int _Y_SPACE_BETWEEN_ITEMS;
    Dictionary<InventorySlot,GameObject> _itemsDisplayed = new Dictionary<InventorySlot,GameObject>(); // 아이템추가 시

    // Start is called before the first frame update
    void Start()
    {
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }
    public void UpdateDisplay()
    {
        for (int i = 0; i < _inventory._container._items.Count; i++)
        {
            InventorySlot slot = _inventory._container._items[i];
            if (_itemsDisplayed.ContainsKey(slot))
            {
                _itemsDisplayed[slot].GetComponentInChildren<TextMeshProUGUI>().text = slot._itemAmount.ToString("n0");
            }
            else
            {
                var obj = Instantiate(_inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
                Debug.Log(_inventory._database._getItem[slot._item._id]._uiDisplay != null ? "Valid display sprite" : "Display sprite is null");
                obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _inventory._database._getItem[slot._item._id]._uiDisplay;
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = slot._itemAmount.ToString("n0");
                _itemsDisplayed.Add(slot, obj);
            }
        }
    }
    public void CreateDisplay()
    {
        for (int i = 0; i < _inventory._container._items.Count; i++)
        {
            InventorySlot slot = _inventory._container._items[i];
            var obj = Instantiate(_inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _inventory._database._getItem[slot._item._id]._uiDisplay;
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = slot._itemAmount.ToString("n0");

            _itemsDisplayed.Add(slot, obj);
        }
    }
    public Vector3 GetPosition(int i)
    {
        return new Vector3(_X_START + (_X_SPACE_BETWEEN_ITEM * (i % _NUMBER_OF_COLUMN)), _Y_START + (-_Y_SPACE_BETWEEN_ITEMS * (i / _NUMBER_OF_COLUMN)), 0f);
    }
}

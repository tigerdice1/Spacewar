using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPage : MonoBehaviour
{
    [SerializeField]
    private InventoryItem _itemPrefab;
    [SerializeField]
    private RectTransform _contentPanel;

    List<InventoryItem> _listOfItems = new List<InventoryItem>();

    public void InitializeInventory(int inventorysize)
    {
        for(int i =0;i<inventorysize; i++)
        {
            InventoryItem _item = Instantiate(_itemPrefab,Vector3.zero,Quaternion.identity);
            _item.transform.SetParent(_contentPanel);
            _listOfItems.Add(_item);
        }
    }
    //인벤토리창 on/off
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}

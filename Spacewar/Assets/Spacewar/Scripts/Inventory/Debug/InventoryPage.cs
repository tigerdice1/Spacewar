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
            _item.OnItemClicked += HandleItemSelection;
            _item.OnItemBeginDrag += HandleBeginDrag;
            _item.OnItemDroppedOn += HandleSwap;
            _item.OnItemEndDrag += HandleEndDrag;
            _item.OnRightMouseBtnClick += HandleShowItemActions;
        }
    }
    private void HandleShowItemActions(InventoryItem obj){

    }
    private void HandleEndDrag(InventoryItem obj){

    }
    private void HandleSwap(InventoryItem obj){

    }
    private void HandleBeginDrag(InventoryItem obj){

    }
    private void HandleItemSelection(InventoryItem obj){

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

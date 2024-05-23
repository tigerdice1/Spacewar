using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPage : MonoBehaviour
{
    [SerializeField]
    private InventoryItem _itemPrefab;

    [SerializeField]
    private RectTransform _contentPanel;

    [SerializeField]
    private InventoryDescription _itemDescription;

    [SerializeField]
    private MouseFollower _mouseFollower;

    List<InventoryItem> _listOfItems = new List<InventoryItem>();

    //테스트 용 코드 1
    public Sprite _image;
    public int _quantity;
    public string _title, _description;

    private void Awake(){
        Hide();
        _mouseFollower.Toggle(false);
        _itemDescription.ResetDescription();
    }

    public void Initialize(int inventorysize){
        for(int i =0; i < inventorysize; i++)
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
        _mouseFollower.Toggle(false);
    }
    private void HandleSwap(InventoryItem obj){

    }
    private void HandleBeginDrag(InventoryItem obj){
        _mouseFollower.Toggle(true);
        _mouseFollower.SetData(_image,_quantity);
    }
    private void HandleItemSelection(InventoryItem obj){
        _itemDescription.SetDescription(_image,_title,_description); //test 1
        _listOfItems[0].Select();
    }
    //인벤토리창 on/off
    public void Show()
    {
        gameObject.SetActive(true);
        _itemDescription.ResetDescription();

        _listOfItems[0].SetData(_image,_quantity); // test 1
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour
{
    [SerializeField]
    private Image _itemImage;
    [SerializeField]
    private TMP_Text _quantityTxt;

    [SerializeField]
    private Image _borderImage;

    public event Action<InventoryItem> OnItemClicked,
        OnItemDroppedOn,OnItemBeginDrag,OnItemEndDrag,
        OnRightMouseBtnClick;

    private bool _empty = true;

    public void Awake(){
        ResetData();
        Deselect();
    }

    public void ResetData(){
        this._itemImage.gameObject.SetActive(false);
        _empty = true;
    }

    public void Deselect(){
        _borderImage.enabled = false;
    }

    public void SetData(Sprite sprite, int quantity){ //이미지와 수량 값 전달
        this._itemImage.gameObject.SetActive(true);
        this._itemImage.sprite = sprite;
        this._quantityTxt.text = quantity + "";
        _empty = false;
    }

    public void Select()
    {
        _borderImage.enabled = true;
    }

    public void OnBeginDrag()
    {
        if (_empty)
            return;
        OnItemBeginDrag?.Invoke(this); //null check
    }
    public void OnDrop()
    {
        OnItemDroppedOn?.Invoke(this);
    }

    public void OnEndDrag()
    {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnPointerClick(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;
        if(pointerData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseBtnClick?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }
}

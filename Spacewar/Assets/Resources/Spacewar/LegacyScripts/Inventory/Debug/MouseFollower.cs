using PlayerInven.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
   [SerializeField]
   private Canvas _canvas;

   [SerializeField]
   private InventoryItem _item;

   private void Awake()
   {
        _canvas = transform.root.GetComponent<Canvas>();
        _item = GetComponentInChildren<InventoryItem>();
   }

   public void SetData(Sprite sprite, int quantity)
   {
        _item.SetData(sprite,quantity);
   }

   private void Update() //스크린좌표로 변환
   {
        Vector2 _position;
         RectTransformUtility.ScreenPointToLocalPointInRectangle(
           (RectTransform)_canvas.transform,
           Input.mousePosition,
           _canvas.worldCamera,
           out _position
         );
         transform.position =  _canvas.transform.TransformPoint(_position);
   }
 
   public void Toggle(bool val)
   {
        Debug.Log($"Item toggled {val}");
        gameObject.SetActive(val);
   }
}

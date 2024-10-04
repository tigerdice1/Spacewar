using PlayerInven.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
   [SerializeField]
   private Inventory _inventoryData;

   private void OnTriggerEnter(Collider collision){
        FieldItem item = collision.GetComponent<FieldItem>();
        if(item != null)
        {
            int reminder = _inventoryData.AddItem(item.InvenItem,item.Quantity);
            if(reminder == 0){
                item.DestroyItem();
            }
            else{
                item.Quantity = reminder;
            }
        }    
   }
}

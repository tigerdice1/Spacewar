using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{

    public CustomTypes.ItemData Item;
    private PlayerBase _triggeredPlayer;
    public void PickupItem(int index){
        if(_triggeredPlayer.Inventory[index].ItemType == 0){
            _triggeredPlayer.Inventory[index] = Item;
            Destroy(gameObject);
        }
    }
    public void UseItem(GameObject targetObject){
        if(Item.ItemType == 3){
            targetObject.GetComponent<Console_PowerGenerator>().FillFuel();
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")){
            _triggeredPlayer = other.GetComponent<PlayerBase>();
            _triggeredPlayer.PlayerController.TriggerObject = this.gameObject;
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")){
            _triggeredPlayer.GetComponent<PlayerBase>().PlayerController.TriggerObject = null;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

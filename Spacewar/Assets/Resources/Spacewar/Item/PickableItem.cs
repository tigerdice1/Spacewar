using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{

    public CustomTypes.ItemData Item;
    public bool IsAttached = false;
    private PlayerBase _triggeredPlayer;
    private Rigidbody _rigidbody;
    public void PickupItem(int index){
        if(_triggeredPlayer.Inventory[index].ItemType == 0 && !IsAttached){
            _triggeredPlayer.Inventory[index] = Item;
            Destroy(gameObject);
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
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        if(IsAttached){
            Destroy(_rigidbody);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

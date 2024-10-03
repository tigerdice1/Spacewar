using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{

    public CustomTypes.ItemData Item;
    private PlayerBase _triggeredPlayer;
    public void PickupItem(){
        _triggeredPlayer.Inventory.Add(Item);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")){
            _triggeredPlayer = other.GetComponent<PlayerBase>();
            _triggeredPlayer.PlayerController.TriggerObject = this.gameObject;
        }
    }
    private void OnTriggerExit(Collider other) {
        _triggeredPlayer.PlayerController.TriggerObject = null;
        _triggeredPlayer = null;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{

    public CustomTypes.ItemData Item;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")){
            other.GetComponent<PlayerBase>().Inventory.Add(Item);
            Destroy(gameObject);
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

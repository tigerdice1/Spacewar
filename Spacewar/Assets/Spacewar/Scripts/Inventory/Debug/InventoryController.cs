using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private InventoryPage _inventoryUI;

    public int inventorySize = 4; // 임시

    private void Start(){
        _inventoryUI.InitializeInventory(inventorySize);
    }
    public void Update(){
        if(Input.GetKeyDown(KeyCode.I)){
            if(_inventoryUI.isActiveAndEnabled == false){
                _inventoryUI.Show();
            }
            else{
                _inventoryUI.Hide();
            }
        }
    }
}

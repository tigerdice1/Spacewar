using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private InventoryPage _inventoryUI;

    public int inventorySize = 4; // 임시

    private void Start(){
        _inventoryUI.Initialize(inventorySize);
    }
    private void Update(){ //작동 테스트
        if(Input.GetKeyDown(KeyCode.I)){
            ToggleInventory();
        }
    }
    public void ToggleInventory()
    {
        if (!_inventoryUI.isActiveAndEnabled)
        {
            _inventoryUI.Show();
        }
        else
        {
            _inventoryUI.Hide();
        }
    }
}


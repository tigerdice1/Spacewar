using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryTest : MonoBehaviour
{
    public InventoryObject _inventory;
    //아이템이 닿으면 InventoryObject에 추가 및 배치 된 아이템 삭제
    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();
        if(item)
        {
            _inventory.AddItem(item._item, 1);
            Destroy(other.gameObject);
        }
    }
    //게임을 끄면 인벤토리 내 아이템 모두 정리.
    private void OnApplicationQuit()
    {
        _inventory.Container.Clear();
    }
}

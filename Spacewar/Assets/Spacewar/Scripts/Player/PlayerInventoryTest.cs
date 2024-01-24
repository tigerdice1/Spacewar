using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryTest : MonoBehaviour
{
    public InventoryObject _inventory;
    //�������� ������ InventoryObject�� �߰� �� ��ġ �� ������ ����
    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();
        if(item)
        {
            _inventory.AddItem(item._item, 1);
            Destroy(other.gameObject);
        }
    }
    //������ ���� �κ��丮 �� ������ ��� ����.
    private void OnApplicationQuit()
    {
        _inventory.Container.Clear();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventoryTest : MonoBehaviour
{
    public InventoryObject _inventory;

    public TMP_Text showItemName;

    private bool hasPressedE = false;

    [SerializeField]
    [Tooltip("플레이어 컨트롤러")]
    private PlayerController _playerController;

    public PlayerController PlayerController
    {
        get { return _playerController; }
        set { _playerController = value; }
    }

    //아이템이 닿으면 InventoryObject에 추가 및 배치 된 아이템 삭제
    public void OnTriggerStay(Collider other){
        if (hasPressedE || !Input.GetKey(KeyCode.E)) return;

        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            showItemName.gameObject.SetActive(false);
            _inventory.AddItem(new Item(item._item), 1);
            Destroy(other.gameObject);

            hasPressedE = true;
        } 
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _inventory.Save();
        }
        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            _inventory.Load();
        }
        //중복 방지 예외처리
        if (Input.GetKeyUp(KeyCode.E))
        {
            hasPressedE = false;
        }
    }
    //게임을 끄면 인벤토리 내 아이템 모두 정리.
    private void OnApplicationQuit()
    {
       _inventory._container._items.Clear();
    }
}

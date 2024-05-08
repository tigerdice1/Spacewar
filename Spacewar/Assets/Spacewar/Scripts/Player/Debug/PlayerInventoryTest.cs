using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventoryTest : MonoBehaviour
{
    [SerializeField]
    [Tooltip("인벤토리")]
    private InventoryObject _inventory;

    public TMP_Text _showItemName;

    private bool hasPressedE = false;

    [SerializeField]
    [Tooltip("플레이어 컨트롤러")]
    private PlayerController _playerController;

    public PlayerController PlayerController{
        set => _playerController = value; 
        get => _playerController; 
    }
    
    [SerializeField]
    [Tooltip("플레이어 회전")]
    private float _playerRotationSpeed;

    public float PlayerRotationSpeed { 
        set => _playerRotationSpeed = value;
        get => _playerRotationSpeed;
    }

    [SerializeField]
    [Tooltip("플레이어 속도")]
    private float _playerSpeed;

    public float PlayerSpeed { 
        get{return _playerSpeed;}
        set{_playerSpeed = value;}
    }
    public InventoryObject Inventory{
        get{return _inventory;}
        set{_inventory = value;}
    }

    //아이템이 닿으면 InventoryObject에 추가 및 배치 된 아이템 삭제
    public void OnTriggerStay(Collider other){
        if (hasPressedE || !Input.GetKey(KeyCode.E)) return;

        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            _showItemName.gameObject.SetActive(false);
            Inventory.AddItem(new Item(item.Item), 1);
            Destroy(other.gameObject);

            hasPressedE = true;
        } 
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Inventory.Save();
        }
        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Inventory.Load();
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
       Inventory.Container._items.Clear();
    }
}

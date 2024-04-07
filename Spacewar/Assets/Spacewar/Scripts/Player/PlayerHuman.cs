using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHuman : MonoBehaviour
{

    public InventoryObject _inventory;

    public TMP_Text showItemName;

    private bool hasPressedE = false;

    [SerializeField]
    [Tooltip("플레이어 컨트롤러")]
    private PlayerController _playerController;
    [SerializeField]
    [Tooltip("HP")]
    private HPSystem _hpSystem;

    [SerializeField]
    [Tooltip("플레이어 속도")]
    private float _playerSpeed;

    [SerializeField]
    [Tooltip("플레이어 회전")]
    private float _playerRotationSpeed;

    [SerializeField]
    [Tooltip("플레이어 체력")]
    private float _playerMaxHP;

    [SerializeField]
    [Tooltip("플레이어 현재체력")]
    private float _playerCurrentHP;

    /* Properties */
    public PlayerController PlayerController{
        get{return _playerController;}
        set{_playerController = value;}
    }
    public float PlayerSpeed { 
        get{return _playerSpeed;}
        set{_playerSpeed = value;}
    }
    public float PlayerRotationSpeed { 
        get{return _playerRotationSpeed;}
        set{_playerRotationSpeed = value;}
    }
    public float PlayerMaxHP { 
        get{return _playerMaxHP;}
        set{_playerMaxHP = value;}
    }
    public float PlayerCurrentHP { 
        get{return _playerCurrentHP;}
        set{_playerCurrentHP = value;}
    }

    public void OnTriggerStay(Collider other){
        if (hasPressedE || !Input.GetKey(KeyCode.E)) return;

        var item = other.GetComponent<GroundItem>();
        if (item){
            showItemName.gameObject.SetActive(false);
            _inventory.AddItem(new Item(item._item), 1);
            Destroy(other.gameObject);

            hasPressedE = true;
        } 
    }

    void Initalize(){
        _playerMaxHP = 100.0f;
        _playerCurrentHP = _playerMaxHP;
        _hpSystem.SetMaxHP(_playerMaxHP);
    }
    // Start is called before the first frame update
    void Start(){
        Initalize();
    }

    // Update is called once per frame
    void Update()
    {
        //DEBUG_CODE
        if(Input.GetKeyDown(KeyCode.Space)){
            TakeDamage(20.0f);
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            _inventory.Save();
        }
        if(Input.GetKeyDown(KeyCode.KeypadEnter)){
            _inventory.Load();
        }
        //중복 방지 예외처리
        if (Input.GetKeyUp(KeyCode.E)){
            hasPressedE = false;
        }
    }

    void TakeDamage(float damage){
        _playerCurrentHP -= damage;
        _hpSystem.SetHP(_playerCurrentHP);
    }

    //게임을 끄면 인벤토리 내 아이템 모두 정리.
    private void OnApplicationQuit(){
       _inventory._container._items.Clear();
    }
}

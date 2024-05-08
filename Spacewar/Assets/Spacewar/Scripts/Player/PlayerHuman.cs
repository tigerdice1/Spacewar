using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHuman : MonoBehaviour
{
    [Tooltip("아이템 주울 때 예외처리")]
    private bool hasPressedE = false;

    [SerializeField]
    [Tooltip("플레이어 컨트롤러")]
    private PlayerController _playerController;

    [SerializeField]
    [Tooltip("인벤토리")]
    private InventoryObject _inventory;

    [SerializeField]
    [Tooltip("아이템표시 기능 & 아이템 비활성화 시 예외처리에 사용 될 변수")]
    private TMP_Text _showItemName;

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
        set => _playerController = value;
        get => _playerController;
    }
    public float PlayerSpeed { 
        set => _playerSpeed = value;
        get => _playerSpeed;
    }
    public float PlayerRotationSpeed { 
        set => _playerRotationSpeed = value;
        get => _playerRotationSpeed;
    }
    public float PlayerMaxHP { 
        set => _playerMaxHP = value;
        get => _playerMaxHP;
    }
    public float PlayerCurrentHP { 
        set => _playerCurrentHP = value;
        get => _playerCurrentHP;
    }

    public InventoryObject Inventory{
        set => _inventory = value;
        get => _inventory;
    }

    public TMP_Text ShowItemName{
        set => _showItemName = value;
        get => _showItemName;
    }

    void OnTriggerStay(Collider other){
        if (hasPressedE || !Input.GetKey(KeyCode.E)) return;

        var item = other.GetComponent<GroundItem>();
        if (item){
            ShowItemName.gameObject.SetActive(false);
            Inventory.AddItem(new Item(item.Item), 1);
            Destroy(other.gameObject);

            hasPressedE = true;
        } 
    }

    void Initalize(){
        _hpSystem = this.GetComponent<HPSystem>();
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
        //아이템 습득 중복 방지 예외처리
        if (Input.GetKeyUp(KeyCode.E)){
            hasPressedE = false;
        }
    }

    void TakeDamage(float damage){
        _playerCurrentHP -= damage;
        _hpSystem.SetHP(_playerCurrentHP);
    }

    //게임을 끄면 인벤토리 내 아이템 모두 정리.
    void OnApplicationQuit(){
       _inventory.Container._items.Clear();
    }
}

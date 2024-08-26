using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Human : PlayerBase{
    [SerializeField]
    private Animator _animator;
    [Tooltip("아이템 주울 때 예외처리")]
    private bool hasPressedE = false;

    // [SerializeField]
    // [Tooltip("인벤토리")]
    // private InventoryObject _inventory;


    // public InventoryObject Inventory{
    //     set => _inventory = value;
    //     get => _inventory;
    // }

    // void OnTriggerStay(Collider other){
    //     if (hasPressedE || !Input.GetKey(KeyCode.E)) return;

    //     var item = other.GetComponent<GroundItem>();
    //     if (item){
    //         ShowItemName.gameObject.SetActive(false);
    //         Inventory.AddItem(new Item(item.Item), 1);
    //         Destroy(other.gameObject);

    //         hasPressedE = true;
    //     } 
    // }

    // Start is called before the first frame update
    void Start(){
        Debug.Log("start");
        //Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        //_animator.SetBool("isWalking", true);
         //DEBUG_CODE
        if(Input.GetKeyDown(KeyCode.Space)){
            TakeDamage(20.0f);
        }
        // if(Input.GetKeyDown(KeyCode.Space)){
        //     _inventory.Save();
        // }
        // if(Input.GetKeyDown(KeyCode.KeypadEnter)){
        //     _inventory.Load();
        // }
        // //아이템 습득 중복 방지 예외처리
        // if (Input.GetKeyUp(KeyCode.E)){
        //     hasPressedE = false;
        // }
    }

    void TakeDamage(float damage){
        _playerCurrentHP -= damage;
        _hpSystem.SetHP(_playerCurrentHP);
    }

    public void AddHealth(int val){
        if(_playerCurrentHP>=_playerMaxHP)
            return;
        _playerCurrentHP += (float)val;
        _hpSystem.SetHP(_playerCurrentHP);
    }
    public void Initialize(){
        Debug.Log("초기화");
        _playerMaxHP = 100.0f;
        _playerCurrentHP = _playerMaxHP;
        _hpSystem.SetMaxHP(_playerMaxHP);
    }

    //게임을 끄면 인벤토리 내 아이템 모두 정리.
    // void OnApplicationQuit(){
    //    _inventory.Container._items.Clear();
    // }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHuman : MonoBehaviour
{
    public HPSystem _hpSystem;

    [SerializeField]
    [Tooltip("플레이어 컨트롤러")]
    private PlayerController _playerController;

    [SerializeField]
    [Tooltip("플레이어 속도")]
    private float _playerSpeed;

    [SerializeField]
    [Tooltip("플레이어 체력")]
    private float _playerMaxHealthPoint;

    [SerializeField]
    [Tooltip("플레이어 현재체력")]
    private float _playerCurrentHealthPoint;

    /* Properties */
    public PlayerController PlayerController{
        get{return _playerController;}
        set{_playerController = value;}
    }
    public float PlayerSpeed { 
        get{return _playerSpeed;}
        set{_playerSpeed = value;}
    }
    public float PlayerMaxHealthPoint { 
        get{return _playerMaxHealthPoint;}
        set{_playerMaxHealthPoint = value;}
    }
    public float PlayerCurrentHealthPoint { 
        get{return _playerCurrentHealthPoint;}
        set{_playerCurrentHealthPoint = value;}
    }


    // Start is called before the first frame update
    void Start()
    {
        PlayerMaxHealthPoint = 100.0f;
        PlayerCurrentHealthPoint = PlayerMaxHealthPoint;
        _hpSystem.SetMaxHealth(PlayerMaxHealthPoint);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            TakeDamage(20.0f);
        }
    }

    void TakeDamage(float damage)
    {
        PlayerCurrentHealthPoint -= damage;

        _hpSystem.SetHealth(_playerCurrentHealthPoint);
    }
}

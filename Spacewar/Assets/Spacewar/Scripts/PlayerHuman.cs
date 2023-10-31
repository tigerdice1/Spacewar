using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHuman : MonoBehaviour
{
    [SerializeField]
    [Tooltip("플레이어 컨트롤러")]
    private PlayerController _playerController;

    [SerializeField]
    [Tooltip("플레이어 속도")]
    private float _playerSpeed;

    [SerializeField]
    [Tooltip("플레이어 체력")]
    private float _playerHealthPoint;

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
    public float PlayerHealthPoint { 
        get{return _playerHealthPoint;}
        set{_playerHealthPoint = value;}
    }
    public float PlayerCurrentHealthPoint { 
        get{return _playerCurrentHealthPoint;}
        set{_playerCurrentHealthPoint = value;}
    }

    
    // Start is called before the first frame update
    void Start()
    {
        PlayerHealthPoint = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

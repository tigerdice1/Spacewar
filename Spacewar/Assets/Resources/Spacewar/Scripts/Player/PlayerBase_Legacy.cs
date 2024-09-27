using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBase_Legacy : MonoBehaviour
{
    [SerializeField]
    protected PlayerController _playerController;

    [SerializeField]
    [Tooltip("플레이어 속도")]
    protected float _playerSpeed;

    [SerializeField]
    [Tooltip("플레이어 회전")]
    protected float _playerRotationSpeed;

    [SerializeField]
    [Tooltip("플레이어 체력")]
    protected float _playerMaxHP;

    [SerializeField]
    [Tooltip("플레이어 현재체력")]
    protected float _playerCurrentHP;
    [SerializeField]
    protected Animator _animator;
    protected Rigidbody _rigidbody;

    [SerializeField]
    [Tooltip("아이템표시 기능 & 아이템 비활성화 시 예외처리에 사용 될 변수")]
    protected TMP_Text _showItemName;

    [SerializeField]
    protected HPSystem _hpSystem;

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
    
    public TMP_Text ShowItemName{
        set => _showItemName = value;
        get => _showItemName;
    }
    
    public void UpdateWalkingState(float forwardSpeed, float lateralSpeed){
        if(_animator != null){
            _animator.SetFloat("walkingForwardSpeed", forwardSpeed);
            _animator.SetFloat("walkingLateralSpeed", lateralSpeed);
        }
    }

    // Start is called before the first frame update
    protected virtual void Start(){
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    protected virtual void Update(){
        
    }
}

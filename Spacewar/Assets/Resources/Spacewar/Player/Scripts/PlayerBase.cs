using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBase : MonoBehaviour, IControllable
{
    public float PlayerSpeed;
    public float PlayerRotationSpeed;
    public float PlayerCurrentHP;
    public float PlayerMaxHP;
    public bool IsPickingUpItem;
    public PlayerController PlayerController;
    public List<CustomTypes.ItemData> Inventory = new List<CustomTypes.ItemData>();
    
    public Transform HandBone;
    public Transform AttachedItem;
    private Animator _animator;
    private Rigidbody _rigidbody;

    protected virtual void Initialize(){
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        CustomTypes.ItemData blankItem = new CustomTypes.ItemData(null, 0, null);
        for(int i = 0; i < 10; i++){
            Inventory.Add(blankItem);
        }
    }
    public virtual void DropItemAnimation(int invIndex){
        _animator.SetTrigger("DropItem");
        if(AttachedItem != null){
            Destroy(AttachedItem.gameObject);
        }
    }
    public virtual void EquipItemAnimation(int invIndex){
        _animator.SetTrigger("EquipItem");
        if(AttachedItem != null){
            Destroy(AttachedItem.gameObject);
        }
        AttachedItem = Instantiate(ItemManager.Instance().FindItem(Inventory[invIndex].ItemType),HandBone.position, HandBone.rotation * Quaternion.Euler(0.0f, -90f, 0.0f)).transform;
        AttachedItem.GetComponent<PickableItem>().IsAttached = true;
        AttachedItem.SetParent(HandBone);
    }
    protected virtual void Die(){
        Debug.Log("Died");
    }
    protected virtual void Awake(){
        Initialize();
    }

    public float RotationSpeed => PlayerRotationSpeed;

    public void Move(PlayerController controller){
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float magnitude = Mathf.Sqrt(moveHorizontal * moveHorizontal + moveVertical * moveVertical);
        if (magnitude > 1) {
            moveHorizontal /= magnitude;
            moveVertical /= magnitude;
        }
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        Vector3 newVelocity = new Vector3(movement.x * PlayerSpeed, _rigidbody.velocity.y, movement.z * PlayerSpeed);

        _rigidbody.velocity = newVelocity;
        
    }

    public void Look(PlayerController controller, float maxRotationSpeed, bool useSlerp){
        controller.LookAtCursor(maxRotationSpeed, useSlerp);
    }

    public void HandleMouseClick(PlayerController controller){
        // 필요 시 구현
    }

    public void UpdateAnimation(PlayerController controller){
        Vector3 localVelocity = transform.InverseTransformDirection(_rigidbody.velocity);
        float forwardSpeed = localVelocity.z;
        float lateralSpeed = localVelocity.x;
        _animator.SetFloat("ForwardSpeed", forwardSpeed);
        _animator.SetFloat("LateralSpeed", lateralSpeed);
    }
}

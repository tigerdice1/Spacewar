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
    protected virtual void Die(){
        Debug.Log("Died");
    }
    protected virtual void Awake(){
        Initialize();
    }

    public float RotationSpeed => PlayerRotationSpeed;

    public void DropItem(int index){
        if(Inventory[index].ItemType != 0){
            GameObject spawnedItem = Instantiate(Inventory[index].Prefab, gameObject.transform.position, gameObject.transform.rotation);
            spawnedItem.GetComponent<PickableItem>().Item.Prefab = Inventory[index].Prefab;
            Inventory[index].ClearItemData();
        }
    }
    public void UseItem(int index, GameObject targetObject){
        if(Inventory[index].ItemType != 0){
            Inventory[index].Prefab.GetComponent<PickableItem>().UseItem(targetObject);
            Inventory[index].ClearItemData();
        }
    }
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

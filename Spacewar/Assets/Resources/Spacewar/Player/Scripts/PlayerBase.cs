using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using Photon.Pun;
using System;

public class PlayerBase : MonoBehaviourPunCallbacks, IControllable, IPunObservable
{
    #region Public Variables

    public float PlayerSpeed;
    public float PlayerRotationSpeed;
    public float PlayerCurrentHP;
    public float PlayerMaxHP;

    public float FixSkill = 100f;

    
    public bool IsPickingUpItem;
    public PlayerController PlayerController;
    public List<CustomTypes.ItemData> Inventory = new List<CustomTypes.ItemData>();

    public Transform HandBone;
    public Transform AttachedItem;

    public static event Action<Collider> OnObjectEnterTrigger;
    public static event Action<Collider> OnObjectStayTrigger;
    public static event Action<Collider> OnObjectExitTrigger;
    #endregion Public Variables
    protected Vector3 _networkPosition;
    protected Quaternion _networkRotation;

    #region Private Variables

    private Animator _animator;
    private Rigidbody _rigidbody;

    #endregion Private Variables

    #region Private Methods
    private void OnTriggerEnter(Collider other){
        OnObjectEnterTrigger?.Invoke(other);
    }
    private void OnTriggerStay(Collider other){
        OnObjectStayTrigger?.Invoke(other);
    }
    private void OnTriggerExit(Collider other){
        OnObjectExitTrigger?.Invoke(other);
    }

    #endregion Private Methods

    #region Protected Methods
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

    #endregion Protected Methods

    #region Public Methods
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
        if ( stream.IsWriting )
        {
            stream.SendNext(_rigidbody.position);
            stream.SendNext(_rigidbody.rotation);
            stream.SendNext(_rigidbody.velocity);
        }
        else if ( stream.IsReading )
        {
            _networkPosition = (Vector3) stream.ReceiveNext();
            _networkRotation = (Quaternion) stream.ReceiveNext();
            _rigidbody.velocity = (Vector3) stream.ReceiveNext();

            float lag = Mathf.Abs((float) (PhotonNetwork.Time - info.timestamp));
            _rigidbody.position += _rigidbody.velocity * lag;
        }
    }
    public virtual void DropItemAnimation(){
        _animator.SetTrigger("DropItem");
        if(AttachedItem != null){
            AttachedItem.GetComponent<PickableItem>().DestroyItem();
            AttachedItem = null;
        }
    }
    public virtual void EquipItemAnimation(int invIndex){
        _animator.SetTrigger("EquipItem");
        if(AttachedItem != null){
            AttachedItem.GetComponent<PickableItem>().DestroyItem();
        }
        if(Inventory[invIndex].ID == 0){
            return;
        }
        AttachedItem = ItemManager.Instance().InstantiateItem(Inventory[invIndex].ID, HandBone.position, HandBone.rotation * Quaternion.Euler(0.0f, -90f, 0.0f)).transform;


        photonView.RPC("SetItemAttachment", RpcTarget.AllBuffered, AttachedItem.GetComponent<PhotonView>().ViewID);
    
    }


    [PunRPC]
    public void SetItemAttachment(int itemPhotonViewID){
    // 전송받은 ID를 사용해 해당 아이템을 찾음
    PhotonView itemPhotonView = PhotonView.Find(itemPhotonViewID);

    if (itemPhotonView != null){
        AttachedItem = itemPhotonView.transform;
        var pickableItem = AttachedItem.GetComponent<PickableItem>();

        if (pickableItem != null){
            pickableItem.IsAttached = true;
        }

        AttachedItem.SetParent(HandBone); // 부모 설정도 동기화
    }
    else{
        Debug.LogWarning("AttachedItem을 찾을 수 없습니다. 아이템이 아직 동기화되지 않았을 수 있습니다.");
    }
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

    #endregion Public Methods

    private void FixedUpdate(){
        /*
        if(_rigidbody == null) return;
        if (!photonView.IsMine){
            _rigidbody.position = Vector3.MoveTowards(_rigidbody.position, _networkPosition, Time.fixedDeltaTime);
            _rigidbody.rotation = Quaternion.RotateTowards(_rigidbody.rotation, _networkRotation, Time.fixedDeltaTime * 100.0f);
        }
        */
    }
}

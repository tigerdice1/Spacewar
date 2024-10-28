using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PickableItem : MonoBehaviourPunCallbacks
{
    /* 새로운 아이템을 추가할 때 필히 ItemManager에 프리펩 경로를 추가해주세요.*/
    #region Public Variables

    public CustomTypes.ItemData ItemInfo;
    public bool IsAttached = false;

    #endregion Public Variables

    #region Protected Variables
    [SerializeField]
    protected Rigidbody _rigidbody;
    protected BoxCollider _boxCollider;
    protected Vector3 _networkPosition;
    protected Quaternion _networkRotation;

    #endregion Protected Variables


    #region Protected Methods
    
    protected virtual void Initalize(){
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _boxCollider = gameObject.GetComponent<BoxCollider>();
    }

    #endregion Protected Methods
    
    #region Public Methods
    public void PickupItem(PlayerBase targetPlayer, int targetInventoryIndex){
        if(targetPlayer.Inventory[targetInventoryIndex].ID == 0 && !IsAttached){
            targetPlayer.Inventory[targetInventoryIndex] = ItemInfo;
            PhotonView targetPhotonView = gameObject.GetComponent<PhotonView>();
            if (targetPhotonView != null){
                // 해당 오브젝트의 소유자에게 "DestroyItem" RPC 호출
                targetPhotonView.RPC("DestroyItem", RpcTarget.AllBuffered);
            }
        }
    }

    public virtual void UseItem(Transform ownPlayer, Transform targetTransform){

    }
    public virtual void DropItem(Transform transform){
        ItemManager.Instance().InstantiateItem(ItemInfo.ID, transform.position, transform.rotation);
        DestroyItem();
    }

    [PunRPC]
    public void DestroyItem(){
        if(photonView.IsMine){
            PhotonNetwork.Destroy(gameObject);
        }
    }
    [PunRPC]
    public void RemovePhysics(){
        _rigidbody.isKinematic = true;
        _rigidbody.useGravity = false;
        Destroy(_boxCollider);
    }
    #endregion Public Methods

    protected virtual void Awake(){
        Initalize();
    }
    // Start is called before the first frame update
    protected virtual void Start(){
        if(IsAttached) {
            photonView.RPC("RemovePhysics", RpcTarget.AllBuffered);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void FixedUpdate(){

    }
}

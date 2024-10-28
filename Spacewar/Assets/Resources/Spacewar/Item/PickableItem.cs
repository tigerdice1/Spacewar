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
    protected Rigidbody _rigidbody;

    #endregion Protected Variables

    #region Protected Methods
    protected virtual void Initalize(){

    }
    /* 플레이어에게 부착된 상태면 불필요한 물리를 없애기 위해 리지드바디 삭제 */
    protected void DestroyRigidbody(){
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        Destroy(_rigidbody);
        
    }
    
    #endregion Protected Methods
    
    #region Public Methods
    public void PickupItem(PlayerBase targetPlayer, int targetInventoryIndex){
        if(targetPlayer.Inventory[targetInventoryIndex].ID == 0 && !IsAttached){
            targetPlayer.Inventory[targetInventoryIndex] = ItemInfo;
            DestroyItem();
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
        else{
            RequestDestroyItem();
        }
    }
    public void RequestDestroyItem(){
        PhotonView targetPhotonView = gameObject.GetComponent<PhotonView>();
        if (targetPhotonView != null){
            // 해당 오브젝트의 소유자에게 "DestroyItem" RPC 호출
            targetPhotonView.RPC("DestroyItem", RpcTarget.AllBuffered);
        }
    }

    #endregion Public Methods
    // Start is called before the first frame update
    protected virtual void Start(){
        if(IsAttached) DestroyRigidbody();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

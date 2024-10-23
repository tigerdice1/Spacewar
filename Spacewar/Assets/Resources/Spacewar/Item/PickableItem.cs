using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PickableItem : MonoBehaviourPunCallbacks
{

    public CustomTypes.ItemData Item;
    public bool IsAttached = false;
    private PlayerBase _triggeredPlayer;
    private Rigidbody _rigidbody;
    public void PickupItem(int index){
        if(_triggeredPlayer.Inventory[index].ItemType == 0 && !IsAttached){
            _triggeredPlayer.Inventory[index] = Item;
            DestroyItem();
        }
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
            targetPhotonView.RPC("DestroyItem", RpcTarget.MasterClient);
        }
    }
        private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")){
            _triggeredPlayer = other.GetComponent<PlayerBase>();
            _triggeredPlayer.PlayerController.TriggerObject = this.gameObject;
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")){
            _triggeredPlayer.GetComponent<PlayerBase>().PlayerController.TriggerObject = null;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        if(IsAttached){
            Destroy(_rigidbody);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

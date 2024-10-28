using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Projectile : MonoBehaviourPunCallbacks
{
    [SerializeField]
    protected ShipBase _ownerShip;
    [SerializeField]
    protected float _projectileVelocity;
    [SerializeField]
    protected float _projectileDamage;
    protected bool _isLaunched;

    [SerializeField]
    protected float _destoryTimer;

    protected float _timer;

    protected Rigidbody _rigidbody;

    public ShipBase OwnerShip{
        set =>  value = _ownerShip;
        get => _ownerShip;
    }

    public float ProjectileDamage{
        set =>  value = _projectileDamage;
        get => _projectileDamage;
    }
    protected void Initailze(){
        this.transform.SetParent(null);
        _rigidbody= this.GetComponent<Rigidbody>();
        _rigidbody.AddRelativeForce(Vector3.forward * _projectileVelocity * _rigidbody.mass * 10f);
        
    }
    private void OnCollisionEnter(Collision other) {

    }
    [PunRPC]
    protected void DestroyProjectile(){
        if(photonView.IsMine){
            PhotonNetwork.Destroy(gameObject);
        }
        else{
            RequestDestroyProjectile();
        }
    }

    protected void RequestDestroyProjectile(){
        PhotonView targetPhotonView = gameObject.GetComponent<PhotonView>();
        if (targetPhotonView != null){
            // 해당 오브젝트의 소유자에게 "DestroyProjectile" RPC 호출
            targetPhotonView.RPC("DestroyProjectile", RpcTarget.AllBuffered);
        }
    }
    // Start is called before the first frame update
    protected virtual void Start(){
        Initailze(); 
    }

    // Update is called once per frame
    protected void Update(){
        _timer += Time.deltaTime;
        if(_timer >= _destoryTimer){
            DestroyProjectile();
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class ItemManager : MonoBehaviour
{
    public List<GameObject> ItemList = new List<GameObject>();
    public List<string> ItemPrefabPathList = new List<string>();
    private static ItemManager _instance;
    public static ItemManager Instance(){
        return _instance;
    }

    public void InitalizeItemList(){
        ItemPrefabPathList.Add(PrefabPath.DriverPrefabPath);
        ItemPrefabPathList.Add(PrefabPath.WrenchPrefabPath);
    }
    public GameObject FindItemByID(int id){
        foreach(string path in ItemPrefabPathList){
            GameObject itemPreload = Resources.Load<GameObject>(path);
            if(id == itemPreload.GetComponent<PickableItem>().ItemInfo.ID) return itemPreload;
        }
        return null;
    }
    public string FindItemPathByID(int id){
        foreach(string path in ItemPrefabPathList){
            GameObject itemPreload = Resources.Load<GameObject>(path);
            if(id == itemPreload.GetComponent<PickableItem>().ItemInfo.ID) return path;
        }
        Debug.Log("couldn't find item");
        return null;
    }
    public Transform InstantiateItem(int id, Vector3 position, Quaternion rotation){
        return PhotonNetwork.Instantiate(FindItemPathByID(id), position, rotation).transform;
    }
    /*
    public void RequestInstantiateItem(int id, Vector3 position, Quaternion rotation){
        PhotonView targetPhotonView = gameObject.GetComponent<PhotonView>();
        if (targetPhotonView != null){
            // 해당 오브젝트의 소유자에게 "InstantiateItem" RPC 호출
            targetPhotonView.RPC("InstantiateItem", RpcTarget.MasterClient, id, position, rotation);
        }
    }
    */
    public void DropItem(Transform transform, int inventoryIndex, PlayerBase itemUser){
        var player = transform.gameObject.GetComponent<PlayerBase>();
        if(player != null){
            CustomTypes.ItemData itemWillDrop = player.Inventory[inventoryIndex];
            if(itemWillDrop.ID != 0){
                PhotonNetwork.Instantiate(FindItemPathByID(itemWillDrop.ID), transform.position, transform.rotation);
                itemWillDrop.ClearItemData();
            }
        }
        
    }

    void Awake(){
    if(_instance == null){
        _instance = this;
        }
    }
    // Start is called before the first frame update
    void Start(){
        InitalizeItemList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

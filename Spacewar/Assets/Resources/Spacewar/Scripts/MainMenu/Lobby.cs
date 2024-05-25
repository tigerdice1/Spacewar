using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using TMPro;

public class Lobby : MonoBehaviourPunCallbacks
{
    [SerializeField]
    InputField _inputServerName;
    [SerializeField]
    GameObject _serverListItem;

    public Transform rtContent;

    Dictionary<string, RoomInfo> _dicRoomInfo = new Dictionary<string, RoomInfo>();
    // Start is called before the first frame update
    public override void OnRoomListUpdate(List<RoomInfo> roomList){
        base.OnRoomListUpdate(roomList);
        DeleteRoomListItem();
        UpdateRoomListItem(roomList);
        CreateRoomListItem();
    }

    void DeleteRoomListItem(){
            Destroy(rtContent.gameObject);
    }

    void UpdateRoomListItem(List<RoomInfo> roomList){
        for(int i = 0; i < roomList.Count; i++){
            if(_dicRoomInfo.ContainsKey(roomList[i].Name)){
                if (roomList[i].RemovedFromList){
                    _dicRoomInfo.Remove(roomList[i].Name);
                    continue;
                }
            }
            _dicRoomInfo[roomList[i].Name] = roomList[i];
        }
    }

    void CreateRoomListItem(){
        foreach(RoomInfo roomInfo in _dicRoomInfo.Values){
            GameObject go = Instantiate(_serverListItem, rtContent);
            RoomListItem item = go.GetComponent<RoomListItem>();
            item.SetInfo(roomInfo.Name, roomInfo.PlayerCount, roomInfo.MaxPlayers);
            //item.onDelegate = SelectRoomItem;
        }

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

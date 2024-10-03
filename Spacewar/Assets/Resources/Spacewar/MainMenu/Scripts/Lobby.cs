using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using TMPro;

public class Lobby : MonoBehaviourPunCallbacks
{
    public Transform RtContent;
    
    [SerializeField]
    private InputField _inputServerName;
    [SerializeField]
    private GameObject _serverListItem;


    Dictionary<string, RoomInfo> _dicRoomInfo = new Dictionary<string, RoomInfo>();

    
    // Start is called before the first frame update
    public override void OnRoomListUpdate(List<RoomInfo> roomList){
        base.OnRoomListUpdate(roomList);
        DeleteRoomListItem();
        UpdateRoomListItem(roomList);
        CreateRoomListItem();
    }

    void DeleteRoomListItem(){
        foreach(Transform child in RtContent){
            Destroy(child.gameObject);
        }
    }

    void UpdateRoomListItem(List<RoomInfo> roomList){
        Dictionary<string, RoomInfo> newDicRoomInfo = new Dictionary<string, RoomInfo>();
        foreach (RoomInfo roomInfo in roomList){
            if (roomInfo.RemovedFromList){
                // 방이 목록에서 제거된 경우
                if (_dicRoomInfo.ContainsKey(roomInfo.Name)){
                    _dicRoomInfo.Remove(roomInfo.Name);
                }
            }
            else{
                // 방 정보를 업데이트
                newDicRoomInfo[roomInfo.Name] = roomInfo;
            }
        }
    // 기존 _dicRoomInfo를 새로운 사전으로 교체
    _dicRoomInfo = newDicRoomInfo;
        /*
        for(int i = 0; i < roomList.Count; i++){
            if(_dicRoomInfo.ContainsKey(roomList[i].Name)){
                if (roomList[i].RemovedFromList){
                    _dicRoomInfo.Remove(roomList[i].Name);
                    continue;
                }
            }
            _dicRoomInfo[roomList[i].Name] = roomList[i];
        }
        */
    }

    void CreateRoomListItem(){
        foreach(RoomInfo roomInfo in _dicRoomInfo.Values){
            GameObject go = Instantiate(_serverListItem, RtContent);
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

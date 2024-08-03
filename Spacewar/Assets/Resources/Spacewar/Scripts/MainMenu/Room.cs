using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Room : MonoBehaviourPunCallbacks
{
    public Transform[] _teamListContents;
    public GameObject _playerListItem;


    public void UpdatePlayerList(){
        foreach(Transform child in _teamListContents[0]){
            Destroy(child.gameObject);
        }
        foreach(Transform child in _teamListContents[1]){
            Destroy(child.gameObject);
        }

        Player[] players = PhotonNetwork.PlayerList;
        foreach(Player player in players){
            if(_teamListContents[0].childCount <= _teamListContents[1].childCount){
                GameObject go = Instantiate(_playerListItem, _teamListContents[0]);
                PlayerListItem item = go.GetComponent<PlayerListItem>();
                item.SetPlayerInfo(player.NickName);
            }
            else{
                GameObject go = Instantiate(_playerListItem, _teamListContents[1]);
                PlayerListItem item = go.GetComponent<PlayerListItem>();
                item.SetPlayerInfo(player.NickName);
            }
        }
    }


    public override void OnPlayerEnteredRoom(Player newPlayer){
        UpdatePlayerList();
    }   

    public override void OnPlayerLeftRoom(Player otherPlayer){
        UpdatePlayerList();
    }
    // Start is called before the first frame update
    void Start(){
        UpdatePlayerList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

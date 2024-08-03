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
        Player[] players = PhotonNetwork.PlayerList;
        foreach(Player player in players){
            AddPlayerToTeam(player);
        }
    }
    

    public void AddPlayerToTeam(Player player){
        foreach (Transform team in _teamListContents){
            foreach (Transform child in team){
                PlayerListItem item = child.GetComponent<PlayerListItem>();
                if (item != null && item.Player == player){
                    // 플레이어가 이미 목록에 있으면 업데이트하지 않음
                    return;
                }
            }
        }
        Transform teamToAdd = _teamListContents[0].childCount <= _teamListContents[1].childCount ? _teamListContents[0] : _teamListContents[1];
        GameObject go = Instantiate(_playerListItem, teamToAdd);
        PlayerListItem newitem = go.GetComponent<PlayerListItem>();
        newitem.Player = player;
    }

    public void RemovePlayerFromTeam(Player player){
        foreach (Transform team in _teamListContents){
            foreach (Transform child in team){
                PlayerListItem item = child.GetComponent<PlayerListItem>();
                if (item != null && item.Player == player){
                    Destroy(child.gameObject);
                    return;
                }
            }
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer){
        UpdatePlayerList();
    }   

    public override void OnPlayerLeftRoom(Player otherPlayer){
        RemovePlayerFromTeam(otherPlayer);
    }
    // Start is called before the first frame update
    void Start(){
        UpdatePlayerList();
    }

    // Update is called once per frame
    void Update(){
        
    }
}

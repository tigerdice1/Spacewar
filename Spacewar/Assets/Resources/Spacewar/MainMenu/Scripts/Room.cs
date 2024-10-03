using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class Room : MonoBehaviourPunCallbacks
{
    public Transform[] TeamListContents;
    public GameObject PlayerListItem;
    private void UpdatePlayerProperties(Transform team, Player targetPlayer){
        ExitGames.Client.Photon.Hashtable customPropertise = 
        team == TeamListContents[0] ? 
        new ExitGames.Client.Photon.Hashtable{
            {"Team", 0}
        } : new ExitGames.Client.Photon.Hashtable{
            {"Team", 1}
        };
        targetPlayer.SetCustomProperties(customPropertise);
    }

    private void UpdatePlayerList(){
        Player[] players = PhotonNetwork.PlayerList;
        foreach(Player player in players){
            AddPlayerToTeam(player);
        }
    }

    private void AddPlayerToTeam(Player player){
        foreach (Transform team in TeamListContents){
            foreach (Transform child in team){
                PlayerListItem item = child.GetComponent<PlayerListItem>();
                if (item != null && item.Player == player){
                    // 플레이어가 이미 목록에 있으면 업데이트하지 않음
                    return;
                }
            }
        }
        Transform teamToAdd = TeamListContents[0].childCount <= TeamListContents[1].childCount ? TeamListContents[0] : TeamListContents[1];
        GameObject go = Instantiate(PlayerListItem, teamToAdd);
        PlayerListItem newitem = go.GetComponent<PlayerListItem>();
        UpdatePlayerProperties(teamToAdd, player);
        newitem.Player = player;
    }

    private void AddPlayerToTeam(Player player, Transform teamToAdd){
        GameObject go = Instantiate(PlayerListItem, teamToAdd);
        PlayerListItem newitem = go.GetComponent<PlayerListItem>();
        UpdatePlayerProperties(teamToAdd, player);
        newitem.Player = player;
    }

    private Transform RemovePlayerFromTeam(Player player){
        foreach (Transform team in TeamListContents){
            foreach (Transform child in team){
                PlayerListItem item = child.GetComponent<PlayerListItem>();
                if (item != null && item.Player == player){
                    Destroy(child.gameObject);
                    Transform teamToAdd = TeamListContents[0] == team ? TeamListContents[1] : TeamListContents[0];
                    return teamToAdd;
                }
            }
        }
        return null;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer){
        UpdatePlayerList();
    }   

    public override void OnPlayerLeftRoom(Player otherPlayer){
        RemovePlayerFromTeam(otherPlayer);
    }

    public override void OnJoinedRoom(){
        UpdatePlayerList();
    }

    public void SwitchTeam(){
        Player[] players = PhotonNetwork.PlayerList;
         foreach(Player player in players){
            if(player.IsLocal){
                Transform teamToAdd = RemovePlayerFromTeam(player);
                AddPlayerToTeam(player, teamToAdd);
            }
         }
    }

    public void StartBtnClicked(){
        SceneLoader.Instance().LoadPhotonNetworkScene("MP_TestingChamber");
    }
    // Start is called before the first frame update
    void Start(){
        UpdatePlayerList();
    }

    // Update is called once per frame
    void Update(){
        
    }
}

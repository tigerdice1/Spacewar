using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.Linq;

public class Room : MonoBehaviourPunCallbacks
{
    public List<Transform> TeamListContents = new List<Transform>();
    private GameObject _playerListItem;
    private string _playerListItemPath = "Spacewar/MainMenu/PlayerListItem";
    private Button _startButton;
    private Button _switchTeamButton;
    private Button _quitButton;


    private void UpdatePlayerList(){
        Player[] players = PhotonNetwork.PlayerList;
        foreach(Player player in players){
            AddPlayerToTeam(player);
        }
    }
    private void UpdatePlayerProperties(Transform team, Player targetPlayer){
        if (PhotonNetwork.IsConnectedAndReady && PhotonNetwork.InRoom){
            ExitGames.Client.Photon.Hashtable customPropertise = 
            team == TeamListContents[0] ? 
            new ExitGames.Client.Photon.Hashtable{
                {"Team", 0}
            } : new ExitGames.Client.Photon.Hashtable{
                {"Team", 1}
            };
            targetPlayer.SetCustomProperties(customPropertise);
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
        GameObject go = Instantiate(_playerListItem, teamToAdd);
        PlayerListItem newitem = go.GetComponent<PlayerListItem>();
        UpdatePlayerProperties(teamToAdd, player);
        newitem.Player = player;
    }

    private void AddPlayerToTeam(Player player, Transform teamToAdd){
        GameObject go = Instantiate(_playerListItem, teamToAdd);
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
        if(!NetworkManager.Instance().CheckMasterClient()){
            _startButton.gameObject.SetActive(false);
        }
        _startButton.onClick.AddListener(StartBtnClicked);
        _quitButton.onClick.AddListener(NetworkManager.Instance().LeaveRoom);
        _switchTeamButton.onClick.AddListener(SwitchTeam);
        UpdatePlayerList();
    }

    void Awake(){
        Transform[] allTransforms = Resources.FindObjectsOfTypeAll<Transform>();
        TeamListContents.Add(allTransforms.FirstOrDefault(t => t.gameObject.name == "Team1 Content"));
        TeamListContents.Add(allTransforms.FirstOrDefault(t => t.gameObject.name == "Team2 Content"));
        _playerListItem = Resources.Load<GameObject>(_playerListItemPath);
        _startButton = allTransforms.FirstOrDefault(t => t.gameObject.name == "HostGameStart_Btn").GetComponent<Button>();
        _switchTeamButton = allTransforms.FirstOrDefault(t => t.gameObject.name == "SwitchTeam_Btn").GetComponent<Button>();
        _quitButton = allTransforms.FirstOrDefault(t => t.gameObject.name == "HostGameQuit_Btn").GetComponent<Button>();
        
    }
    // Update is called once per frame
    void Update(){
        
    }
}

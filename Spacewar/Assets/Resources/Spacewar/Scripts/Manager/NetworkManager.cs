using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class NetworkManager : MonoBehaviourPunCallbacks
{
    private static NetworkManager _instance;
    private string _playerName;
    private string _serverName;

    private byte _maxPlayers;

    private bool _isLoggedIn = false;
    private bool _isHost = false;
    private Text _connectionStatus;

    public static NetworkManager Instance(){
        return _instance;
    }

    public bool IsHost{
        get => _isHost;
    }

    public void SetPlayerName(string playerName){
        this._playerName = playerName;
    }

    public void SetServerName(string serverName){
        this._serverName = serverName;
    }

    public void SetMaxPlayer(string maxPlayers){
        byte mp;
        byte.TryParse(maxPlayers, out mp);
        this._maxPlayers = mp;
    }
    public override void OnConnectedToMaster(){
        base.OnConnectedToMaster();
        print("서버 접속 완료");
        PhotonNetwork.ConnectUsingSettings();
        //PhotonNetwork.LocalPlayer.NickName = _playerName;
        //PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions { MaxPlayers = 6}, null);
    }

    public override void OnConnected(){
        CreateRoom();
    }

    public override void OnJoinedLobby(){
        print("로비 접속 완료");
    }

    public void OnLoginButtonClicked(){
        if(!_playerName.Equals("")){
            PhotonNetwork.LocalPlayer.NickName = _playerName;
            
            _isLoggedIn = true;
        }
        else{
            Debug.LogError("Player Name is invalid.");
        }
    }
    public void OnCreatedRoom(){
        _isHost = true;
    }
    public void CreateRoom(){
        _serverName = (_serverName.Equals(string.Empty)) ? "Room " + Random.Range(1000, 10000) : _serverName;
        _maxPlayers = (byte) Mathf.Clamp(_maxPlayers, 2, 10);
        RoomOptions options = new RoomOptions {MaxPlayers = _maxPlayers, PlayerTtl = 10000 };
        PhotonNetwork.CreateRoom(_serverName, options, null);
        
        //
        
    }
    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnJoinedRoom(){
        //UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    void Awake(){
        if(_instance == null){
            _instance = this;
        }
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 60;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        //_connectionStatus.text = PhotonNetwork.NetworkClientState.ToString();
        
        
    }
}

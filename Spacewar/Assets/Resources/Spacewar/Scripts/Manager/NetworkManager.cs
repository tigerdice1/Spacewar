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

    private string _onClickedRoomName;

    public static NetworkManager Instance(){
        return _instance;
    }

    public bool IsHost{
        get => _isHost;
    }

    public bool IsLoggedIn{
        get => _isLoggedIn;
    }

    public string OnClickedRoomName{
        set => _onClickedRoomName = value;
    }

    public void ConnectToLobby(){
        PhotonNetwork.JoinLobby();
    }
    public void SetPlayerName(string playerName){
        this._playerName = playerName;
    }

    public void SetServerName(string serverName){
        this._serverName = serverName;
    }

    public string GetServerName{
        get => _serverName;
    }

    public void SetMaxPlayer(string maxPlayers){
        byte mp;
        byte.TryParse(maxPlayers, out mp);
        this._maxPlayers = mp;
    }
    public override void OnConnectedToMaster(){
        base.OnConnectedToMaster();
        print("마스터 서버 접속 완료");
    }

    public void Connect(){
        PhotonNetwork.ConnectUsingSettings();
        print("서버 접속 중...");
    }
    public override void OnConnected(){
        print("접속 완료");
    }

    public override void OnJoinedLobby(){
        base.OnJoinedLobby();
        print("로비 접속 완료");
    }

    public void OnLoginButtonClicked(){
        if(_playerName == null){
            Debug.LogError("Player Name is Empty!.");
            return;
        }
        if(!_playerName.Equals("")){
            PhotonNetwork.LocalPlayer.NickName = _playerName;
            
            _isLoggedIn = true;
            Connect();
        }
        else{
            Debug.LogError("Player Name is invalid.");
        }
    }
    public override void OnCreatedRoom(){
        print("호스트 서버 생성 완료");
        _isHost = true;
    }
    public void CreateRoom(){
        print("호스트 서버 생성 중");
        _serverName = (_serverName.Equals(string.Empty)) ? "Room " + Random.Range(1000, 10000) : _serverName;
        _maxPlayers = (byte) Mathf.Clamp(_maxPlayers, 2, 10);
        RoomOptions options = new RoomOptions {MaxPlayers = _maxPlayers, PlayerTtl = 10000, IsVisible = true, IsOpen = true};
        PhotonNetwork.CreateRoom(_serverName, options, null);
        
        
        
    }

    public void ClearClickedRoomName(){
        _onClickedRoomName = " ";
    }

    public void JoinRoom(){
        if(!_onClickedRoomName.Equals(" ")){
            PhotonNetwork.JoinRoom(_onClickedRoomName);
        }
    }

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

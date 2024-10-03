using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class NetworkManager : MonoBehaviourPunCallbacks
{
    /*Variables*/
    private static NetworkManager _instance;
    private string _playerName;
    private string _serverName;

    private byte _maxPlayers;

    private bool _isLoggedIn;
    private Text _connectionStatus;

    public string _onClickedRoomName;

    public MainMenu _mainMenu;
    //Variables//

    /*Properties*/
    public static NetworkManager Instance(){
        return _instance;
    }

    public bool IsLoggedIn{
        get => _isLoggedIn;
    }

    public string OnClickedRoomName{
        set => _onClickedRoomName = value;
        get => _onClickedRoomName;
    }

    public void ConnectToLobby(){
        _mainMenu.UpdateLoadingPanel("Joining Lobby..");
        PhotonNetwork.JoinLobby();
    }
    public string PlayerName{
        set => _playerName = value;
        get => _playerName;
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
    //Properties//
    /* Connect to MasterServer Function*/
    public void Connect(){
        PhotonNetwork.LocalPlayer.NickName = this._playerName;
        PhotonNetwork.SendRate = 120;
        PhotonNetwork.SerializationRate = 120;
        PhotonNetwork.ConnectUsingSettings();
        _mainMenu.UpdateLoadingPanel("Connecting to Server");
        StartCoroutine(WaitForConnectionCoroutine());
    }

    public override void OnConnected(){
        _mainMenu.UpdateLoadingPanel("Server Connected");
    }

    public override void OnConnectedToMaster(){
        base.OnConnectedToMaster();
        _mainMenu.UpdateLoadingPanel("Master Server Connected");
        _mainMenu.SetMainBtnGroupPanelActive(true);
        _mainMenu.SetLoadingPanelActive(false);
    }
    //Connect to MasterServer Function//
    /* Connect to Lobby Function. Must ConnectedToMaster to be loaded*/
    public override void OnJoinedLobby(){
        base.OnJoinedLobby();
        _mainMenu.UpdateLoadingPanel("Lobby Joined.");
        _mainMenu.SetLoadingPanelActive(false);
        _mainMenu.SetJoinGamePanelActive(true);
    }
    //Connect to Lobby Function. Must ConnectedToMaster to be loaded//

    /* CreateRoom Function. Must ConnectedToMaster to be loaded*/
    public void CreateRoom(){
        _mainMenu.UpdateLoadingPanel("Creating Host Server...");
        _serverName = (_serverName.Equals(string.Empty)) ? "Room " + Random.Range(1000, 10000) : _serverName;
        _maxPlayers = (byte) Mathf.Clamp(_maxPlayers, 2, 12);
        RoomOptions options = new RoomOptions {MaxPlayers = _maxPlayers, PlayerTtl = 10000, IsVisible = true, IsOpen = true};
        PhotonNetwork.CreateRoom(_serverName, options, null);
    }

    public override void OnCreatedRoom(){
        _mainMenu.UpdateLoadingPanel("Host Server Created.");
        SceneLoader.Instance().LoadLocalScene("Room");
    }
    // CreateRoom Function. Must ConnectedToMaster to be loaded//
    /* JoinRoom Function. Must ConnectedToMaster to be loaded*/
    public void ClearClickedRoom(){
        _onClickedRoomName = " ";
    }

    public void JoinRoom(string serverName){
        if(!serverName.Equals(" ")){
            PhotonNetwork.JoinRoom(serverName);
            _mainMenu.UpdateLoadingPanel("Connecting to room...");
        }
    }

    public override void OnJoinedRoom(){
        SceneLoader.Instance().LoadLocalScene("Room");
    }
    // JoinRoom Function. Must ConnectedToMaster to be loaded//

    public override void OnLeftRoom(){
        PhotonNetwork.LeaveRoom();
        SceneLoader.Instance().LoadLocalScene("MainMenu");
    }
    
    public void CloseRoomAndKickPlayers()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom != null)
        {
            // 모든 플레이어 강제 퇴장
            foreach (Player player in PhotonNetwork.PlayerListOthers)
            {
                PhotonNetwork.CloseConnection(player);  // 해당 플레이어 퇴장
            }

            // 자신도 방에서 나가기
            PhotonNetwork.LeaveRoom();
        }
    }

    public void DebugServerConnect(){
        PhotonNetwork.ConnectUsingSettings();
    }

        // 접속이 완료될 때까지 기다리는 코루틴
    private System.Collections.IEnumerator WaitForConnectionCoroutine()
    {
        Debug.Log("Waiting for connection...");

        // 연결될 때까지 반복해서 체크
        while (!PhotonNetwork.IsConnectedAndReady)
        {
            yield return null; // 한 프레임 대기
        }

        Debug.Log("Connection established!");

        // 접속이 완료된 후 필요한 작업 수행
        _isLoggedIn = true;
    }

    void Awake(){
        if(_instance == null){
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start(){
        if(!PhotonNetwork.IsConnectedAndReady){
            _mainMenu.SetMainBtnGroupPanelActive(false);
            _mainMenu.SetLoginPanelActive(true);
            }
        }
    

    // Update is called once per frame
    void Update(){
        //_connectionStatus.text = PhotonNetwork.NetworkClientState.ToString();
   
    }
}

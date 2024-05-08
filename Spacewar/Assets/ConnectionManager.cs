using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    private static ConnectionManager _instance;
    [SerializeField]
    private Button _connectButton;
    private Text _connectionStatus;

    public static ConnectionManager Instance(){
        return _instance;
    }

    public override void OnConnectedToMaster(){
        print("서버 접속 완료");
        PhotonNetwork.LocalPlayer.NickName = "sdd";
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby(){
        print("로비 접속 완료");
    }

    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    void Awake(){
        if(_instance == null){
            _instance = this;
        }
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

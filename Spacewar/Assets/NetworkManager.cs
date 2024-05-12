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
    private string _nickName;
    private Text _connectionStatus;

    public static NetworkManager Instance(){
        return _instance;
    }

    public void SetNickName(string nickname){
        this._nickName = nickname;
    }
    public override void OnConnectedToMaster(){
        print("서버 접속 완료");
        PhotonNetwork.LocalPlayer.NickName = _nickName;
        PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions { MaxPlayers = 6}, null);
    }

    public override void OnJoinedLobby(){
        print("로비 접속 완료");
    }

    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnJoinedRoom(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
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

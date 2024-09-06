using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Photon.Pun;

public class SceneLoader : MonoBehaviourPunCallbacks{
    private static SceneLoader _instance;

    public static SceneLoader Instance(){
        return _instance;
    }

    public void LoadPhotonNetworkScene(string sceneName){
        if (PhotonNetwork.IsConnected){
            PhotonNetwork.LoadLevel(sceneName);
        }
    }
    public void LoadLocalScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
    void Awake(){
        if(_instance == null){  
            _instance = this;
            PhotonNetwork.AutomaticallySyncScene = true;
        }
    }
}
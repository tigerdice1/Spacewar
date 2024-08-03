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

    public void LoadScene(string sceneName){
        if (PhotonNetwork.IsConnected){
            PhotonNetwork.LoadLevel(sceneName);
        }
        else{
            SceneManager.LoadScene(sceneName);
        }
    }

    void Start(){
        _instance = this;
    }
}

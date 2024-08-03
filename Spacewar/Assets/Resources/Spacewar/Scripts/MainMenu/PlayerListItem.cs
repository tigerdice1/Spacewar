using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI _playerName;
    private Player player;
    public Player Player{
        set => player = value;
        get => this.player;
    }

    public void SetPlayerInfo(string playerName){
        _playerName.text = playerName;
    }
    // Start is called before the first frame update
    void Start(){
        _playerName.text = player.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

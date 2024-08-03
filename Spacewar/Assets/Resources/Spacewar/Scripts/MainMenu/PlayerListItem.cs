using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerListItem : MonoBehaviour
{
    public TextMeshProUGUI _playerName;

    public void SetPlayerInfo(string playerName){
        _playerName.text = playerName;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

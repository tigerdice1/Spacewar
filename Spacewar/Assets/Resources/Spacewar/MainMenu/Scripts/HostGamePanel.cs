using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HostGamePanel : MonoBehaviour
{
    private Button _startServerButton;
    private Button _backButton;
    private TMP_InputField _serverNameInputField;
    private TMP_InputField _serverMaxPlayersInputField;
    // Start is called before the first frame update
    void Start()
    {
        _startServerButton.onClick.AddListener(MainMenuController.Instance().OnHostStartButtonClicked);
        _backButton.onClick.AddListener(MainMenuController.Instance().OnHostBackButtonClicked);
        _serverNameInputField.onEndEdit.AddListener(NetworkManager.Instance().SetServerName);
        _serverMaxPlayersInputField.onEndEdit.AddListener(NetworkManager.Instance().SetMaxPlayer);
    }
    void Awake(){
        for (int i = 0; i < transform.childCount; i++){
            if(transform.GetChild(i).gameObject.name == "HostGamebtn_Start"){
                _startServerButton = transform.GetChild(i).gameObject.GetComponent<Button>();
            }
            if(transform.GetChild(i).gameObject.name == "HostGamebtn_Back"){
                _backButton = transform.GetChild(i).gameObject.GetComponent<Button>();
            }
            if(transform.GetChild(i).gameObject.name == "ServerNameInputField"){
                _serverNameInputField = transform.GetChild(i).gameObject.GetComponent<TMP_InputField>();
            }
            if(transform.GetChild(i).gameObject.name == "MaxPlayerInputField"){
                _serverMaxPlayersInputField = transform.GetChild(i).gameObject.GetComponent<TMP_InputField>();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

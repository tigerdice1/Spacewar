using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuBtnGroupPanel : MonoBehaviour
{
    private Button _startButton;
    private Button _hostButton;
    private Button _joinButton;
    private Button _settingsButton;
    private Button _exitButton;
    // Start is called before the first frame update
    void Start()
    {
        //_startButton.onClick.AddListener();
        _hostButton.onClick.AddListener(MainMenuController.Instance().OnHostButtonClicked);
        _joinButton.onClick.AddListener(MainMenuController.Instance().OnJoinButtonClicked);
        //_settingsButton.onClick.AddListener();
        _exitButton.onClick.AddListener(MainMenuController.Instance().OnExitButtonClicked);
    }
    void Awake(){
        for (int i = 0; i < transform.childCount; i++){
            if(transform.GetChild(i).gameObject.name == "Start_Btn"){
                _startButton = transform.GetChild(i).gameObject.GetComponent<Button>();
            }
            if(transform.GetChild(i).gameObject.name == "Host_Btn"){
                _hostButton = transform.GetChild(i).gameObject.GetComponent<Button>();
            }
            if(transform.GetChild(i).gameObject.name == "Join_Btn"){
                _joinButton = transform.GetChild(i).gameObject.GetComponent<Button>();
            }
            if(transform.GetChild(i).gameObject.name == "Settings_Btn"){
                _settingsButton = transform.GetChild(i).gameObject.GetComponent<Button>();
            }
            if(transform.GetChild(i).gameObject.name == "Exit_Btn"){
                _exitButton = transform.GetChild(i).gameObject.GetComponent<Button>();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

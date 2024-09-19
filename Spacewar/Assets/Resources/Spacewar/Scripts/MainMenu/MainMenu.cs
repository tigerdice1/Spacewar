using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class MainMenu : MonoBehaviour{

    [SerializeField]
    private NetworkManager _netManager;
    [SerializeField]
    private GameObject _mainBtnGroupPanel;
    [SerializeField]
    private GameObject _loginPanel;
    public TextMeshProUGUI _loginErrorText;
    [SerializeField]
    private GameObject _hostGamePanel;
    
    [SerializeField]
    private GameObject _joinGamePanel;

    [SerializeField]
    private GameObject _loadingPanel;
    public TextMeshProUGUI _loadingPanelText;

    [Header("Levels To Load")]
    public string _newGameLevel;
    //private string levelToLoad;
    //[SerializeField] private GameObject noSavedGameDialog = null;

    public void OnLoginButtonClicked(){
        if(_netManager.PlayerName == null){
            UpdateLoginErrorMessage("Player Name is Empty!");
            return;
        }
        if(!_netManager.PlayerName.Equals("")){
            SetLoginPanelActive(false);
            SetLoadingPanelActive(true);
            _netManager.Connect();
        }
        else{
            UpdateLoginErrorMessage("Player Name is invalid.");
        }
    }

    public void OnConnectButtonClicked(){
        if(!_netManager.OnClickedRoomName.Equals(" ")){
            _netManager.JoinRoom(_netManager.OnClickedRoomName);
            SetLoadingPanelActive(true);
        }
    }

    public void OnLobbyBackButtonClicked(){
        SetJoinGamePanelActive(false);
        SetMainBtnGroupPanelActive(true);
    }

    public void OnJoinButtonClicked(){
        SetLoadingPanelActive(true);
        SetMainBtnGroupPanelActive(false);
        _netManager.ConnectToLobby();
    }

    public void OnHostButtonClicked(){
        SetHostGamePanelActive(true);
        SetMainBtnGroupPanelActive(false);
    }

    public void OnHostStartButtonClicked(){
        SetLoadingPanelActive(true);
        _netManager.CreateRoom();
    }

    public void OnHostBackButtonClicked(){
        SetHostGamePanelActive(false);
        SetMainBtnGroupPanelActive(true);
    }

    public void SetMainBtnGroupPanelActive(bool active){
        _mainBtnGroupPanel.SetActive(active);
    }

    public void SetLoginPanelActive(bool active){
        _loginPanel.SetActive(active);
    }

    public void SetHostGamePanelActive(bool active){
        _hostGamePanel.SetActive(active);
    }

    public void SetJoinGamePanelActive(bool active){
        _joinGamePanel.SetActive(active);
    }

    public void SetLoadingPanelActive(bool active){
        _loadingPanel.SetActive(active);
    }

    public void UpdateLoadingPanel(string updateText){
        _loadingPanelText.text = updateText;
    }

    public void UpdateLoginErrorMessage(string updateText){
        _loginErrorText.text = updateText;
    }

    public void NewGameDialogYes()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_newGameLevel);
    }

    // public void LoadGameDialogYes()
    // {
    //     if(PlayerPrefs.HasKey("SavedLevel"))
    //     {
    //         levelToLoad = PlayerPrefs.GetString("SavedLevel");
    //         SceneManager.LoadScene(levelToLoad);
    //     }
    //     else
    //     {
    //         noSavedGameDialog.SetActive(true);
    //     }
    // }
    public void ExitButton()
    {
        Application.Quit();
    }

    private void Start(){
        
    }
    private void Awake(){
        
    }
    private void Update(){
        
    }
}

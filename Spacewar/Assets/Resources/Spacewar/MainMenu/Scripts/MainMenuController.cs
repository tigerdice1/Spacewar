using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;


public class MainMenuController : MonoBehaviour{

    [Header("Levels To Load")]
    public string NewGameLevel;
    //private string levelToLoad;
    //[SerializeField] private GameObject noSavedGameDialog = null;
    private static MainMenuController _instance;
    public static MainMenuController Instance(){
        return _instance;
    }

    private Transform _mainMenuBtnGroup;
    private Transform _loginPanel;
    private Transform _hostGamePanel;
    private Transform _lobbyPanel;    
    private Transform _loadingPanel;
    private TextMeshProUGUI _loadingPanelText;
    private TextMeshProUGUI _loginErrorText;

    public void ActiveMainPanel(){
        _mainMenuBtnGroup.gameObject.SetActive(true);
        _loginPanel.gameObject.SetActive(false);
        _hostGamePanel.gameObject.SetActive(false);
        _lobbyPanel.gameObject.SetActive(false);
        _loadingPanel.gameObject.SetActive(false);
    }
    public void ActiveLoginPanel(){
        _mainMenuBtnGroup.gameObject.SetActive(false);
        _loginPanel.gameObject.SetActive(true);
        _hostGamePanel.gameObject.SetActive(false);
        _lobbyPanel.gameObject.SetActive(false);
        _loadingPanel.gameObject.SetActive(false);
    }
    public void ActiveHostPanel(){
        _mainMenuBtnGroup.gameObject.SetActive(false);
        _loginPanel.gameObject.SetActive(false);
        _hostGamePanel.gameObject.SetActive(true);
        _lobbyPanel.gameObject.SetActive(false);
        _loadingPanel.gameObject.SetActive(false);
    }

    public void ActiveLobbyPanel(){
        _mainMenuBtnGroup.gameObject.SetActive(false);
        _loginPanel.gameObject.SetActive(false);
        _hostGamePanel.gameObject.SetActive(false);
        _lobbyPanel.gameObject.SetActive(true);
        _loadingPanel.gameObject.SetActive(false);
    }
    public void ActiveLoadingPanel(){
        _mainMenuBtnGroup.gameObject.SetActive(false);
        _loginPanel.gameObject.SetActive(false);
        _hostGamePanel.gameObject.SetActive(false);
        _lobbyPanel.gameObject.SetActive(false);
        _loadingPanel.gameObject.SetActive(true);
    }
    public void OnLoginButtonClicked(){
        if(NetworkManager.Instance().PlayerName == null){
            UpdateLoginErrorMessage("Player Name is Empty!");
            return;
        }
        if(!NetworkManager.Instance().PlayerName.Equals("")){
            ActiveLoadingPanel();
            NetworkManager.Instance().Connect();
        }
        else{
            UpdateLoginErrorMessage("Player Name is invalid.");
        }
    }

    public void OnConnectButtonClicked(){
        NetworkManager.Instance().JoinRoom();
    }

    public void OnLobbyBackButtonClicked(){
        NetworkManager.Instance().LeaveLobby();
        ActiveLoadingPanel();
    }

    public void OnJoinButtonClicked(){
        ActiveLoadingPanel();
        NetworkManager.Instance().ConnectToLobby();
    }

    public void OnHostButtonClicked(){
        ActiveHostPanel();
    }

    public void OnHostStartButtonClicked(){
        ActiveLoadingPanel();
        NetworkManager.Instance().CreateRoom();
    }

    public void OnHostBackButtonClicked(){
        ActiveMainPanel();
    }

    public void SetMainBtnGroupPanelActive(bool active){
        _mainMenuBtnGroup.gameObject.SetActive(active);
    }

    public void SetLoginPanelActive(bool active){
        _loginPanel.gameObject.SetActive(active);
    }

    public void SetHostGamePanelActive(bool active){
        _hostGamePanel.gameObject.SetActive(active);
    }

    public void SetJoinGamePanelActive(bool active){
        _lobbyPanel.gameObject.SetActive(active);
    }

    public void SetLoadingPanelActive(bool active){
        _loadingPanel.gameObject.SetActive(active);
    }

    public void UpdateLoadingPanel(string updateText){
        _loadingPanelText.text = updateText;
    }

    public void UpdateLoginErrorMessage(string updateText){
        _loginErrorText.text = updateText;
    }

    public void NewGameDialogYes()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(NewGameLevel);
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
    public void OnExitButtonClicked(){
        Application.Quit();
    }

    private void Start(){
        if(!NetworkManager.Instance().CheckConnectState()){
            ActiveLoginPanel();
        }
    }
    private void Awake(){
        if(_instance == null){
            _instance = this;
        }
        Transform[] allTransforms = Resources.FindObjectsOfTypeAll<Transform>();
        _mainMenuBtnGroup = allTransforms.FirstOrDefault(t => t.gameObject.name == "MainMenuBtnGroup");
        _loginPanel = allTransforms.FirstOrDefault(t => t.gameObject.name == "LoginPanel");
        _hostGamePanel = allTransforms.FirstOrDefault(t => t.gameObject.name == "HostGamePanel");
        _lobbyPanel = allTransforms.FirstOrDefault(t => t.gameObject.name == "LobbyPanel");
        _loadingPanel = allTransforms.FirstOrDefault(t => t.gameObject.name == "LoadingPanel");
        _loginErrorText = allTransforms.FirstOrDefault(t => t.gameObject.name == "LoginErrorText").GetComponent<TextMeshProUGUI>();
        _loadingPanelText = allTransforms.FirstOrDefault(t => t.gameObject.name == "LoadingPanelText").GetComponent<TextMeshProUGUI>();
    }
    private void Update(){
        
    }
}

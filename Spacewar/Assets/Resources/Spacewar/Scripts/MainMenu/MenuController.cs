using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour{

    [SerializeField]
    private NetworkManager _netManager;
    [SerializeField]
    private GameObject _loginPanel;
    [SerializeField]
    private GameObject _hostGamePanel;
    
    [SerializeField]
    private GameObject _hostLobbyPanel;
    [SerializeField]
    private GameObject _joinGamePanel;
    [Header("Levels To Load")]
    public string _newGameLevel;
    //private string levelToLoad;
    //[SerializeField] private GameObject noSavedGameDialog = null;

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

    private void Update(){
        if(_netManager.IsHost){
            _hostGamePanel.SetActive(false);
            _hostLobbyPanel.SetActive(true); //
        }
    }
}

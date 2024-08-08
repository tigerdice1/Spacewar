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
    [SerializeField]
    private GameObject _hostGamePanel;
    
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

    private void Start(){
        
    }
    private void Awake(){
        if(!NetworkManager.Instance().IsLoggedIn){
            _mainBtnGroupPanel.SetActive(false);
            _loginPanel.SetActive(true);
        }
    }
    private void Update(){
        
    }
}

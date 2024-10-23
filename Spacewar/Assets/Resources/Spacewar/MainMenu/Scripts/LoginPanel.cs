using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginPanel : MonoBehaviour
{
    private Button _loginButton;
    private TMP_InputField _nickNameInputField;

    void OnEndEdit(string nickname){
        NetworkManager.Instance().PlayerName = nickname;
    }
    void Awake(){
        for (int i = 0; i < transform.childCount; i++){
            if(transform.GetChild(i).gameObject.name == "Login_Btn"){
                _loginButton = transform.GetChild(i).gameObject.GetComponent<Button>();
            }
            if(transform.GetChild(i).gameObject.name == "NickNameInputField"){
                _nickNameInputField = transform.GetChild(i).gameObject.GetComponent<TMP_InputField>();
            }
        }
    }
    // Start is called before the first frame update
    void Start(){
        _loginButton.onClick.AddListener(MainMenuController.Instance().OnLoginButtonClicked);
        _nickNameInputField.onEndEdit.AddListener(OnEndEdit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

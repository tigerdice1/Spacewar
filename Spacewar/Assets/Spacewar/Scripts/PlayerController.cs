using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("플레이어가 컨트롤 할 오브젝트")]
    private GameObject _controlObject;
    
    [SerializeField]
    [Tooltip("플레이어가 접촉한 오브젝트")]
    private GameObject _triggerObject;

    [SerializeField]
    [Tooltip("유저 인터페이스")]
    private Transform _userInterface;

    [SerializeField]
    [Tooltip("플레이어의 고유 ID")]
    private int _id;    

    /* Properties */
    public GameObject TriggerObject{
        get { return _triggerObject; }
        set { _triggerObject = value; }
    }
    // Start is called before the first frame update

    void Initailize(){
        if(_userInterface.Find("MainUI")){
            _userInterface.Find("MainUI").gameObject.GetComponent<CanvasGroup>().alpha = 1.0f;
            _userInterface.Find("MainUI").gameObject.GetComponent<CanvasGroup>().interactable = true;
        };

    }
    void Start()
    {
        Initailize();
        /*
        GameObject generatorUI = _userInterface.Find("GeneratorUI").gameObject;
        generatorUI.GetComponent<CanvasGroup>().alpha = 0.0f;
        generatorUI.GetComponent<CanvasGroup>().interactable = false;
        */

    }

    void CheckTriggerObject(){
        if(_triggerObject == null){
            GameObject generatorUI = _userInterface.Find("GeneratorUI").gameObject;
            generatorUI.GetComponent<CanvasGroup>().alpha = 0.0f;
            generatorUI.GetComponent<CanvasGroup>().interactable = false;
        }
        
    }
    void CheckUserInput(){
        if(Input.GetKeyDown(KeyCode.E) && _triggerObject != null){
            if(_triggerObject.GetComponent<PowerGenerator>() && !(_userInterface.Find("GeneratorUI").gameObject.GetComponent<CanvasGroup>().interactable)){
                _userInterface.Find("GeneratorUI").gameObject.GetComponent<CanvasGroup>().alpha = 1.0f;
                _userInterface.Find("GeneratorUI").gameObject.GetComponent<CanvasGroup>().interactable = true;
            }
            else if(_triggerObject.GetComponent<PowerGenerator>() && _userInterface.Find("GeneratorUI").gameObject.GetComponent<CanvasGroup>().interactable){
                _userInterface.Find("GeneratorUI").gameObject.GetComponent<CanvasGroup>().alpha = 0.0f;
                _userInterface.Find("GeneratorUI").gameObject.GetComponent<CanvasGroup>().interactable = false;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        CheckUserInput();
        CheckTriggerObject();
    }

    void FixedUpdate(){
        Rigidbody rid = _controlObject.GetComponent<Rigidbody>();
        // 태그가 플레이어일 경우
        if(_controlObject.CompareTag("Player")){
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            Vector3 velocity = new Vector3(x, 0, z);
            velocity *= _controlObject.GetComponent<PlayerHuman>().PlayerSpeed;
            rid.velocity = velocity;
        }   
    }
}
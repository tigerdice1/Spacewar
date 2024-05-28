using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("조명 오브젝트들을 담아두는 함수입니다. 자동으로 자식오브젝트들이 할당됩니다.")]
    private List<Light> _lightComponent;
    private Electricity _lightElectricity;

    /* Properties */
    public void SetLightColor(Color color){
        for(int i = 0; i < _lightComponent.Count; i++){
            _lightComponent[i].color = color;
        }
    }

    public void SetHexColor(string hexCode){
        Color color;
        if (ColorUtility.TryParseHtmlString(hexCode, out color)){
            for(int i = 0; i < _lightComponent.Count; i++){
            _lightComponent[i].color = color;
            }
        }
    }
    
    private void Initalize(){
        if(SceneManager.Instance().IsDebugMode()){
            if(_lightComponent == null){
                Debug.Log("_lightComponent is not Loaded. Location : " + gameObject);
            }
            if(!_lightElectricity){
                Debug.Log("_lightElectricity is not Loaded. Location : " + gameObject);
            }
        }
        _lightComponent = new List<Light>(this.GetComponentsInChildren<Light>());
        _lightElectricity = this.GetComponent<Electricity>();
    }
    private void SyncOnState(){
        for(int i = 0; i < _lightComponent.Count; i++){
            _lightComponent[i].enabled = _lightElectricity.IsPowered;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Initalize();
    }

    // Update is called once per frame
    void Update(){
        SyncOnState();   
    }
}
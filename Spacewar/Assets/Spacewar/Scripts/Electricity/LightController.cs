using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("자동으로 자식 오브젝트를 할당하니 직접할당할 필요없음")]
    private Light[] _lightComponent;

    /* Properties */
    public void SetLightColor(Color color){
        foreach(var elem in _lightComponent){
            elem.color = color;
        }
    }

    public void SetHexColor(string hexCode){
        Color color;
        if (ColorUtility.TryParseHtmlString(hexCode, out color)){
            foreach(var elem in _lightComponent){
                elem.color = color;
                }
            }
        }
    
    
    private void Initalize(){
        _lightComponent = gameObject.GetComponentsInChildren<Light>();
    }
    private void SyncOnState(){
        foreach(var elem in _lightComponent){
            elem.enabled = gameObject.GetComponent<Electricity>().IsPowered;
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

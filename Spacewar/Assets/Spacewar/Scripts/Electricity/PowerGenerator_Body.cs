using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGenerator_Body : MonoBehaviour
{
    [SerializeField]
    private PowerGenerator _generatorController;
    [SerializeField]
    private GameObject _generatorInnerSpinner;
    [SerializeField]
    private GameObject _generatorInnerLight;
    // Start is called before the first frame update

    void UpdateGeneratorState(){
        if(_generatorController.GetGeneratorState()){
                _generatorInnerLight.GetComponent<LightController>().SetHexColor("#00FFFA");
            _generatorInnerLight.GetComponent<Electricity>().SetPowerState(true);
            if(_generatorController.GetIsCritical()){
                _generatorInnerLight.GetComponent<LightController>().SetHexColor("#FF0000");
            }
            else if(!_generatorController.GetIsCritical()){
                _generatorInnerLight.GetComponent<LightController>().SetHexColor("#00FFFA");
            }
        }
        else{
            _generatorInnerLight.GetComponent<Electricity>().SetPowerState(false);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGeneratorState();
    }
}

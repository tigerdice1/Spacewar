using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGenerator_Body : MonoBehaviour
{
    [SerializeField]
    private PowerGeneratorConsole _generatorConsole;
    [SerializeField]
    private GameObject _generatorInnerSpinner;
    private float _currentTorque = 0.0f;
    [SerializeField]
    private GameObject _generatorInnerLight;
    // Start is called before the first frame update

    void UpdateGeneratorActiveAnim(){
        Rigidbody generatorSpinnerRB = _generatorInnerSpinner.GetComponent<Rigidbody>();
        if(_generatorConsole.GetGeneratorState()){
            _currentTorque = Mathf.Lerp(_currentTorque, 15000.0f,Time.deltaTime * 0.2f);
            generatorSpinnerRB.AddRelativeTorque(Vector3.forward * _currentTorque);
        }
        else if(!_generatorConsole.GetGeneratorState()){
            _currentTorque = Mathf.Lerp(_currentTorque, 0.0f,Time.deltaTime);
        }
    }
    void UpdateGeneratorState(){
        if(_generatorConsole.GetGeneratorState()){
                _generatorInnerLight.GetComponent<LightController>().SetHexColor("#00FFFA");
            _generatorInnerLight.GetComponent<Electricity>().SetPowerState(true);
            if(_generatorConsole.GetIsCritical()){
                _generatorInnerLight.GetComponent<LightController>().SetHexColor("#FF0000");
            }
            else if(!_generatorConsole.GetIsCritical()){
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
    void FixedUpdate(){

        UpdateGeneratorActiveAnim();
    }
}

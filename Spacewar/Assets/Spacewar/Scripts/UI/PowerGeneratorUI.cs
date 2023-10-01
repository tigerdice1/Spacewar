using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PowerGeneratorUI : MonoBehaviour
{
    [SerializeField]
    [Tooltip("대상 발전기")]
    private PowerGenerator _powerGenerator;

    [SerializeField]
    [Tooltip("전원 버튼")]
    private Toggle _powerGeneratorBtn;
    public void ToggleOnclick(bool isOn){
        _powerGenerator.SetGeneratorState(isOn);
    }

    public PowerGenerator GetPowerGenerator(){
        return _powerGenerator;
    }

	// 발전기가 꺼져있는지 확인하고 꺼져있다면 전원 버튼도 off 상태로 변환
    void CheckIsGeneratorPowerd(){
        if(!_powerGenerator.GetGeneratorState()){
            _powerGeneratorBtn.isOn = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckIsGeneratorPowerd();
        if(_powerGenerator.GetGeneratorState()){
        }
    }
}

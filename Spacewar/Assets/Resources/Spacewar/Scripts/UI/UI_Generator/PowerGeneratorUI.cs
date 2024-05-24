using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PowerGeneratorUI : MonoBehaviour
{
    [SerializeField]
    [Tooltip("대상 발전기")]
    private Console_PowerGenerator _powerGenerator;
    private Toggle _powerGeneratorBtn;
    private ProgressBar_Fuel_UI _fuel_UI;
    private RadialGauge_Load_UI _Load_UI;
    private SpriteChanger_UI _diode;

    private void Initalize(){
        _powerGeneratorBtn = transform.Find("Generator_UI_Button").GetComponent<Toggle>();
        _diode = transform.Find("Generator_UI_Diode").GetComponent<SpriteChanger_UI>();
    }
    public void ToggleOnclick(bool isOn){
        _powerGenerator.SetGeneratorState(isOn);
        if(_powerGeneratorBtn){
            if (isOn){
                RectTransform rectTransform = _powerGeneratorBtn.GetComponent<Image>().rectTransform;
                Vector3 currentScale = rectTransform.localScale;
                rectTransform.localScale = new Vector3(currentScale.x, -currentScale.y, currentScale.z);
            }
            else{
                RectTransform rectTransform = _powerGeneratorBtn.GetComponent<Image>().rectTransform;
                Vector3 currentScale = rectTransform.localScale;
                rectTransform.localScale = new Vector3(currentScale.x, Mathf.Abs(currentScale.y), currentScale.z);
            }
        }
        
        
        if(_diode){
            _diode.ChangeImage();
        }
    }

    public Console_PowerGenerator GetPowerGenerator{
        get => _powerGenerator;
    }

	// 발전기가 꺼져있는지 확인하고 꺼져있다면 전원 버튼도 off 상태로 변환
    void CheckIsGeneratorPowerd(){
        if(!_powerGenerator.GetGeneratorState){
            _powerGeneratorBtn.isOn = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Initalize();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIsGeneratorPowerd();
        if(_powerGenerator.GetGeneratorState){
        }
    }
}

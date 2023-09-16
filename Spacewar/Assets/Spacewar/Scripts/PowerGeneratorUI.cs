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
    private  GameObject _tachometer;

    [SerializeField]
    [Tooltip("전원 버튼")]
    private Toggle _powerGeneratorBtn;

    [SerializeField]
    private bool _isInitalized;

    private Coroutine _initCoroutine;
    private int _initSequence;
    public void ToggleOnclick(bool isOn){
        if(isOn){
            _powerGenerator.SetGeneratorState(isOn);
            _initCoroutine = StartCoroutine(InitalizeGenerator());
        }
        else if(!isOn){
            _powerGenerator.SetGeneratorState(isOn);
            _initCoroutine = StartCoroutine(StopGenerator());
        }
    }

    IEnumerator StopGenerator(){
        _isInitalized = false;
        _initSequence = 0;
        if(_initCoroutine != null){
            StopCoroutine(_initCoroutine);
        }
        while(_tachometer.GetComponent<RectTransform>().localRotation.eulerAngles.z > 0.0f){
            RectTransform tachoTransform = _tachometer.GetComponent<RectTransform>();

            Quaternion currentRotation = tachoTransform.rotation;
            // 목표 오브젝트의 회전 쿼터니언
            Quaternion targetRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
            // 보간된 회전 쿼터니언을 계산
            float angleDiff = Quaternion.Angle(currentRotation, targetRotation);
            if(angleDiff < 1.0f){
                tachoTransform.rotation = targetRotation;
            }
            else{
                Quaternion newRotation = Quaternion.Slerp(currentRotation, targetRotation, 1.0f * Time.deltaTime);
                Vector3 eulerAngles = newRotation.eulerAngles;
                eulerAngles.z = Mathf.Clamp(eulerAngles.z, 0.0f, 180.0f);
                Quaternion clamppedRotation = Quaternion.Euler(eulerAngles);
                // 보간된 회전 쿼터니언을 현재 오브젝트에 적용
                tachoTransform.rotation = clamppedRotation;
            }
            yield return null;
        }
        _initCoroutine = null;
    }
    IEnumerator InitalizeGenerator(){
        if(!_isInitalized){
            if(_initCoroutine != null){
            StopCoroutine(_initCoroutine);
            }
            if(_initSequence == 0){
                
                while(_tachometer.GetComponent<RectTransform>().localRotation.eulerAngles.z < 180.0f){
                    RectTransform tachoTransform = _tachometer.GetComponent<RectTransform>();

                    float newRotation = tachoTransform.localRotation.eulerAngles.z + 160.0f * Time.deltaTime;
                    newRotation = Mathf.Clamp(newRotation, 0.0f, 180.0f);
                    tachoTransform.localRotation = Quaternion.Euler(0, 180, newRotation);
                    yield return null;
                }
                _initSequence++;
            }
            if(_initSequence == 1){
                while(_tachometer.GetComponent<RectTransform>().localRotation.eulerAngles.z > 0.0f){
                    RectTransform tachoTransform = _tachometer.GetComponent<RectTransform>();

                    float newRotation = tachoTransform.localRotation.eulerAngles.z + -160.0f * Time.deltaTime;
                    newRotation = Mathf.Clamp(newRotation, 0.0f, 180.0f);
                    tachoTransform.localRotation = Quaternion.Euler(0, 180, newRotation);
                    yield return null;
                }
                _initSequence++;
            }
            if(_initSequence == 2){
                while(_tachometer.GetComponent<RectTransform>().localRotation.eulerAngles.z < _powerGenerator.Load / 100.0f * 180.0f){
                    RectTransform tachoTransform = _tachometer.GetComponent<RectTransform>();

                    Quaternion currentRotation = tachoTransform.rotation;
                    // 목표 오브젝트의 회전 쿼터니언
                    Quaternion targetRotation = Quaternion.Euler(0.0f, 180.0f, _powerGenerator.Load / 100.0f * 180.0f);
                    // 보간된 회전 쿼터니언을 계산
                    float angleDiff = Quaternion.Angle(currentRotation, targetRotation);
                    if(angleDiff < 1.0f){
                        tachoTransform.rotation = targetRotation;
                    }
                    else{
                        Quaternion newRotation = Quaternion.Slerp(currentRotation, targetRotation, 2.0f * Time.deltaTime);
                        Vector3 eulerAngles = newRotation.eulerAngles;
                        eulerAngles.z = Mathf.Clamp(eulerAngles.z, 0.0f, 180.0f);
                        Quaternion clamppedRotation = Quaternion.Euler(eulerAngles);
                        // 보간된 회전 쿼터니언을 현재 오브젝트에 적용
                        tachoTransform.rotation = clamppedRotation;
                    }
                    yield return null;
                }
            }
        }
        _isInitalized = true;
        _initCoroutine = null;
    }

    void CalcLoadTachometer(){
        RectTransform tachoTransform = _tachometer.GetComponent<RectTransform>();

        Quaternion currentRotation = tachoTransform.rotation;
        // 목표 오브젝트의 회전 쿼터니언
        Quaternion targetRotation = Quaternion.Euler(0.0f, 180.0f, _powerGenerator.Load / 100.0f * 180.0f);
        // 보간된 회전 쿼터니언을 계산
        float angleDiff = Quaternion.Angle(currentRotation, targetRotation);
        if(angleDiff < 1.0f){
            tachoTransform.rotation = targetRotation;
        }
        else{
            Quaternion newRotation = Quaternion.Slerp(currentRotation, targetRotation, 2.0f * Time.deltaTime);
            // 보간된 회전 쿼터니언을 현재 오브젝트에 적용
            tachoTransform.rotation = newRotation;
        }
    }

    void CheckIsGeneratorPowerd(){
        if(!_powerGenerator.GetGeneratorState()){
            _initCoroutine = StartCoroutine(StopGenerator());
            _powerGeneratorBtn.isOn = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _initSequence = 0;
        _isInitalized = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIsGeneratorPowerd();
        if(_powerGenerator.GetGeneratorState() && _initCoroutine == null){
            CalcLoadTachometer();
        }
    }
}

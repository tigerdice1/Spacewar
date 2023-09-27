using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialGauge_UI : MonoBehaviour
{

    [SerializeField]
    protected GameObject _parentUI;

    [SerializeField]
    protected float _minimumRotation;

    [SerializeField]
    protected float _maximumRotation;

    [SerializeField]
    protected float _rotationAngle;

    protected Coroutine _playingCoroutine;

    IEnumerator SlerpCoroutine(float targetRotationZ, float rotationSpeed){
        Quaternion currentRotation = gameObject.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0.0f, 180.0f, targetRotationZ);
        
        // 현재 오브젝트의 회전값과 목표 회전값을 비교 (근사값 비교)
        while(!Mathf.Approximately(currentRotation.z,targetRotationZ)){
            currentRotation = gameObject.transform.rotation;
            // 현재 오브젝트의 회전값과 목표 회전값을 비교
            float angleDiff = Quaternion.Angle(currentRotation, targetRotation);
            if(angleDiff < 0.1f){
                gameObject.transform.rotation = targetRotation;
                _playingCoroutine = null;
            }
            else{
                Quaternion newRotation = Quaternion.Slerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
                Vector3 eulerAngles = newRotation.eulerAngles;
                eulerAngles.z = Mathf.Clamp(eulerAngles.z, _minimumRotation, _maximumRotation);
                Quaternion clamppedRotation = Quaternion.Euler(eulerAngles); 
                gameObject.transform.rotation = clamppedRotation;
            }
            yield return null;
        }
    }
    IEnumerator LerpCoroutine(float targetRotationZ, float rotationSpeed){
        Quaternion currentRotation = gameObject.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0.0f, 180.0f, targetRotationZ);
        
        // 현재 오브젝트의 회전값과 목표 회전값을 비교 (근사값 비교)
        while(!Mathf.Approximately(currentRotation.z,targetRotationZ)){
            currentRotation = gameObject.transform.rotation;
            // 현재 오브젝트의 회전값과 목표 회전값을 비교
            float angleDiff = Quaternion.Angle(currentRotation, targetRotation);
            if(angleDiff < 0.1f){
                gameObject.transform.rotation = targetRotation;
                _playingCoroutine = null;
            }
            else{
                Quaternion newRotation = Quaternion.Lerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
                Vector3 eulerAngles = newRotation.eulerAngles;
                eulerAngles.z = Mathf.Clamp(eulerAngles.z, _minimumRotation, _maximumRotation);
                Quaternion clamppedRotation = Quaternion.Euler(eulerAngles); 
                gameObject.transform.rotation = clamppedRotation;
            }
            yield return null;
        }
    }
    public void StaticRotationBySlerp(float targetRotation, float rotationSpeed){
        _playingCoroutine = StartCoroutine(SlerpCoroutine(targetRotation, rotationSpeed));
    }

    public void StaticRotationByLerp(float targetRotation, float rotationSpeed){
        _playingCoroutine = StartCoroutine(LerpCoroutine(targetRotation, rotationSpeed));
    }

    public void DynamicRotationBySlerp(float targetRotationZ, float rotationSpeed){
        if(_playingCoroutine != null){
            _playingCoroutine = null;
        }
        // 지금 오브젝트의 회전값 가져오기
        Quaternion currentRotation = gameObject.transform.rotation;
        // 오브젝트의 목표 회전값 지정
        Quaternion targetRotation = Quaternion.Euler(0.0f, 180.0f, targetRotationZ);
        Quaternion newRotation = Quaternion.Slerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
        Vector3 eulerAngles = newRotation.eulerAngles;
        eulerAngles.z = Mathf.Clamp(eulerAngles.z, _minimumRotation, _maximumRotation);
        Quaternion clamppedRotation = Quaternion.Euler(eulerAngles); 
        gameObject.transform.rotation = clamppedRotation;
    }

    public void DynamicRotationByLerp(float targetRotationZ, float rotationSpeed){
        if(_playingCoroutine != null){
            _playingCoroutine = null;
        }
        // 지금 오브젝트의 회전값 가져오기
        Quaternion currentRotation = gameObject.transform.rotation;
        // 오브젝트의 목표 회전값 지정
        Quaternion targetRotation = Quaternion.Euler(0.0f, 180.0f, targetRotationZ);
        Quaternion newRotation = Quaternion.Lerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
        Vector3 eulerAngles = newRotation.eulerAngles;
        eulerAngles.z = Mathf.Clamp(eulerAngles.z, _minimumRotation, _maximumRotation);
        Quaternion clamppedRotation = Quaternion.Euler(eulerAngles); 
        gameObject.transform.rotation = clamppedRotation;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}

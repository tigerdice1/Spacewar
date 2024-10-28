using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_RadialBase : MonoBehaviour{

    [SerializeField]
    protected GameObject _arrowUI;

    [SerializeField]
    [Range(0, 180)]
    protected float _minimumRotation = 0f;

    [SerializeField]
    [Range(0, -230)]
    protected float _maximumRotation = -230f;

    [SerializeField]
    protected float _rotationAngle;
    protected Coroutine _playingCoroutine;


    // PowerGenerator에서 받을 값이 0~1 (백분율)일 때의 허용 오차
    [SerializeField]
    [Range(0f, 1f)]
    protected float _percentageTolerance = 0.01f;

    protected static bool IsApproximatelyEqual(float a, float b, float tolerance){
        float difference = Mathf.Abs(a - b);
        return difference <= tolerance;
    }

    protected static bool AreQuaternionsApproximatelyEqual(Quaternion quat1, Quaternion quat2, float maxAngleDifference){
        float angleDifference = Quaternion.Angle(quat1, quat2);
        return angleDifference <= maxAngleDifference;
    }

    // Slerp을 이용한 회전 함수
    IEnumerator SlerpCoroutine(Quaternion targetRotation, float updateSpeed){
        Quaternion currentRotation = _arrowUI.gameObject.transform.rotation;
        while (!AreQuaternionsApproximatelyEqual(currentRotation, targetRotation, 0.1f)){
            Quaternion newRotation = Quaternion.Slerp(currentRotation, targetRotation, updateSpeed * Time.deltaTime);
            _arrowUI.gameObject.transform.rotation = newRotation;
            yield return null;
        }
        _arrowUI.gameObject.transform.rotation = targetRotation;
    }

    public void RotationBySlerp(Quaternion targetRotation, float updateSpeed){
        if (_playingCoroutine != null)
        {
            StopCoroutine(_playingCoroutine);
        }
        _playingCoroutine = StartCoroutine(SlerpCoroutine(targetRotation, updateSpeed));
    }

    // 백분율에 맞춰서 스프라이트 회전값을 설정하는 함수
    public void UpdateRotation(float percentage, float minValue, float maxValue, bool halfDivided){
        if(halfDivided){
            percentage = Mathf.Clamp(percentage, minValue, maxValue);

            float targetAngle = 0f;

            float normalizedPercentage = percentage / 100f; // 0 ~ 1로 정규화
            targetAngle = Mathf.Lerp(0f, -230f, normalizedPercentage);


            // 현재 회전을 Quaternion으로 변환하여 타겟 회전값을 생성
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);

            // Slerp을 이용하여 부드럽게 회전
            RotationBySlerp(targetRotation, 1f);
        }
        else{
            // 백분율 값이 minValue와 maxValue 사이로 들어온다고 가정
            percentage = Mathf.Clamp(percentage, minValue, maxValue);

            float targetAngle = 0f;

            if (percentage <= 100f){
                // 0% ~ 100% 구간: 0도에서 -180도까지 선형적으로 보간
                float normalizedPercentage = percentage / 100f; // 0 ~ 1로 정규화
                targetAngle = Mathf.Lerp(0f, -190f, normalizedPercentage);
            }
            else{
                // 100% ~ 120% 구간: -181도에서 -230도까지 선형적으로 보간
                float normalizedPercentage = (percentage - 100f) / 20f; // 100 ~ 120% 사이를 0 ~ 1로 정규화
                targetAngle = Mathf.Lerp(-191f, -230f, normalizedPercentage);
            }

            // 현재 회전을 Quaternion으로 변환하여 타겟 회전값을 생성
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);

            // Slerp을 이용하여 부드럽게 회전
            RotationBySlerp(targetRotation, 1f);
        }
    }

    protected virtual void Update(){
    }
}

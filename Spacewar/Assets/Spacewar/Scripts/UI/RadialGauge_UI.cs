using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialGauge_UI : MonoBehaviour
{
    [SerializeField]
    protected GameObject _parentUI;

    [SerializeField]
    protected GameObject _arrowUI;

    [SerializeField]
    [Range(0, 180)]
    protected float _minimumRotation;

    [SerializeField]
    [Range(0, 180)]
    protected float _maximumRotation;

    [SerializeField]
    protected float _rotationAngle;

    protected bool _isHalf;
    protected Coroutine _playingCoroutine;

    protected static bool IsApproximatelyEqual(float a, float b, float tolerance){
    // 두 숫자 사이의 차이를 계산하고 절대값을 취합니다.
    float difference = Mathf.Abs(a - b);
    
    // 허용 오차(tolerance) 내에 있는지 확인합니다.
    return difference <= tolerance;
}
    protected static bool AreQuaternionsApproximatelyEqual(Quaternion quat1, Quaternion quat2, float maxAngleDifference){
        // Quaternion.Angle 함수를 사용하여 두 Quaternion 간의 각도를 계산
        float angleDifference = Quaternion.Angle(quat1, quat2);

        // 각도 차이가 허용 오차 범위 내에 있는지 확인
        return angleDifference <= maxAngleDifference;
    }
    IEnumerator SlerpCoroutine(Quaternion targetRotation, float updateSpeed){
        // 현재 회전값을 저장하는 Local Variable 와 목표 회전값을 저장하는 Local Variable
        Quaternion currentRotation = _arrowUI.gameObject.transform.rotation;
        // 현재 회전값과 목표 회전값을 근사비교하여 False 일 경우 Loop
        while(!AreQuaternionsApproximatelyEqual(currentRotation, targetRotation, 0.1f)){
            // 현재 회전값 Update
            Quaternion newRotation = Quaternion.Slerp(currentRotation, Quaternion.Inverse(targetRotation), updateSpeed * Time.deltaTime);
            _arrowUI.gameObject.transform.rotation = newRotation;
            yield return null;
        }
        _arrowUI.gameObject.transform.rotation = targetRotation;
    }

    public void RotationBySlerp(Quaternion targetRotation, float updateSpeed){
       _playingCoroutine = StartCoroutine(SlerpCoroutine(targetRotation, updateSpeed));
    }

    public void SyncRotationBySlerp(Quaternion targetRotation, float updateSpeed){
        if(_playingCoroutine != null){
            _playingCoroutine = null;
        }
        Quaternion currentRotation = _arrowUI.gameObject.transform.rotation;
        Quaternion newRotation;
        newRotation = Quaternion.Slerp(currentRotation, Quaternion.Inverse(targetRotation), updateSpeed * Time.deltaTime);
        _arrowUI.gameObject.transform.rotation = newRotation;
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

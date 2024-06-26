using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar_UI : MonoBehaviour
{
    [SerializeField]
    [Tooltip("해당 UI의 가장 상위 UI 오브젝트 지정")]
    protected GameObject _parentUI;
    [SerializeField]
    [Tooltip("해당 UI의 하위 UI 오브젝트 지정(움직이는부분)")]
    private Transform _progressBarInnerUI;

    protected Coroutine _playingCoroutine;

    public void SyncProgressBarBySlerp(float targetPercent, float updateSpeed){
        Vector3 currentScale = _progressBarInnerUI.localScale;
        Vector3 targetScale = new Vector3(currentScale.x, targetPercent, currentScale.z);
        if(!Mathf.Approximately(targetScale.x, currentScale.x)){
            _progressBarInnerUI.localScale = Vector3.Slerp(currentScale, targetScale, Time.deltaTime);
        }
        else{
            _progressBarInnerUI.localScale = targetScale;
        }
    }

    public void SyncProgressBarByLerp(float targetPercent, float updateSpeed){
        Vector3 currentScale = _progressBarInnerUI.localScale;
        Vector3 targetScale = new Vector3(currentScale.x, targetPercent, currentScale.z);
        if(!Mathf.Approximately(targetScale.x, currentScale.x)){
            _progressBarInnerUI.localScale = Vector3.Lerp(currentScale, targetScale, Time.deltaTime);
        }
        else{
            _progressBarInnerUI.localScale = targetScale;
        }
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

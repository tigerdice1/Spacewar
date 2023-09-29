using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar_UI : MonoBehaviour
{
    [SerializeField]
    [Tooltip("해당 UI의 가장 상위 UI 오브젝트 지정")]
    protected GameObject _parentUI;
    [SerializeField]
    private Transform _progressBarInnerUI;

    protected Coroutine _playingCoroutine;

    public void SyncProgressBarBySlerp(float targetPercent, float updateSpeed){
        Vector3 targetScale = new Vector3(targetPercent, 1.0f, 1.0f);
        Vector3 currentScale = _progressBarInnerUI.localScale;
        if(!Mathf.Approximately(targetScale.x, currentScale.x)){
            _progressBarInnerUI.localScale = Vector3.Slerp(currentScale, targetScale, Time.deltaTime);
        }
        else{
            _progressBarInnerUI.localScale = targetScale;
        }
    }

    public void SyncProgressBarByLerp(float targetPercent, float updateSpeed){
        Vector3 targetScale = new Vector3(targetPercent, 1.0f, 1.0f);
        Vector3 currentScale = _progressBarInnerUI.localScale;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar_UI : MonoBehaviour
{
    [SerializeField]
    [Tooltip("해당 UI의 가장 상위 UI 오브젝트 지정")]
    protected Transform _parentUI;
    [SerializeField]
    private Transform _progressBarBase;
    [SerializeField]
    [Tooltip("해당 UI의 하위 UI 오브젝트 지정(움직이는부분)")]
    private Transform _progressBarInnerUI;

    protected Coroutine _playingCoroutine;

    private void Initalize(){
        _parentUI = transform.parent;
        _progressBarBase = transform.GetChild(0).transform;
        _progressBarInnerUI = _progressBarBase.GetChild(0).transform;
    }
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
    void Start(){
        Initalize();
    }

    // Update is called once per frame
     protected virtual void Update()
    {
        
    }
}

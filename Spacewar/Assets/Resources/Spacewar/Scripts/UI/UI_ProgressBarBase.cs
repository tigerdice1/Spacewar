using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ProgressBarBase : MonoBehaviour
{

        // 진행률 (0.0f ~ 1.0f 사이의 값)
    [Range(0f, 1f)]
    public float progress = 0f;

    [SerializeField]
    protected Transform _progressBar;

    protected Coroutine _playingCoroutine;

    public void SetProgress(float value){
        // 입력값을 0과 1 사이로 제한
        progress = Mathf.Clamp01(value);
    }

    private void Initialize(){
    }
    public void SyncProgressBar(float targetPercent, float updateSpeed, bool useSlerp){
        Vector3 currentScale = _progressBar.localScale;
        Vector3 targetScale = new Vector3(currentScale.x, targetPercent, currentScale.z);
        if(!Mathf.Approximately(targetScale.x, currentScale.x)){
            if(useSlerp){
                _progressBar.localScale = Vector3.Slerp(currentScale, targetScale, Time.deltaTime * updateSpeed);
            }
            else{
                _progressBar.localScale = Vector3.Lerp(currentScale, targetScale, Time.deltaTime * updateSpeed);
            }
        }
        else{
            _progressBar.localScale = targetScale;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
     protected virtual void Update(){
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FuelBar : UI_ProgressBarBase
{
    [SerializeField]
    protected PowerGenerator _powerGenerator;

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
        SyncProgressBar(_powerGenerator.CurrentFuel / _powerGenerator.MaxFuel, 5f, false);
    }
}

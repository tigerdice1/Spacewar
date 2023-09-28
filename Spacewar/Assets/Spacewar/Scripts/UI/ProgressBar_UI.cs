using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar_UI : MonoBehaviour
{
    [SerializeField]
    private GameObject _parentUI;
    [SerializeField]
    private Transform _progressBarInnerUI;

    protected Coroutine _playingCoroutine;

    public void SyncProgressBarBySlerp(float targetPercent, float updateSpeed){
        Vector3 targetScale = new Vector3(targetPercent, 1.0f, 1.0f);
        Vector3 currentScale = _progressBarInnerUI.localScale;
        _progressBarInnerUI.localScale = Vector3.Slerp(currentScale, targetScale, Time.deltaTime);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
     protected virtual void Update()
    {
        SyncProgressBarBySlerp(_parentUI.GetComponent<PowerGeneratorUI>().GetPowerGenerator().Fuel / _parentUI.GetComponent<PowerGeneratorUI>().GetPowerGenerator().MaxFuel, 1.0f);
    }
}

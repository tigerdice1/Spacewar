using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity : MonoBehaviour
{
    [SerializeField]
    private float _powerUsage;
    [SerializeField]
    private float _powerIdle;
    [SerializeField]
    private float _powerActive;
    private float _powerUpdateMultiplier;
    [SerializeField]
    private bool _isPowered;
    private Coroutine _powerUsageCoroutine;

    /* Properties */
    public bool IsPowered{
        get { return _isPowered; }
        set { _isPowered = value; }
    }
    IEnumerator PowerOnCoroutine(){
        if(_powerUsageCoroutine != null){
            StopCoroutine(_powerUsageCoroutine);
        }
        while(_powerUsage <= _powerActive){
            float newPowerUsage = Mathf.Lerp(_powerUsage, _powerActive, Time.deltaTime);
            _powerUsage = newPowerUsage;

            yield return null;
        }
    }
    IEnumerator PowerOffCoroutine(){
        if(_powerUsageCoroutine != null){
            StopCoroutine(_powerUsageCoroutine);
        }
        while(_powerUsage <= 0){
            float newPowerUsage = Mathf.Lerp(_powerUsage, 0, Time.deltaTime);
            _powerUsage = newPowerUsage;

            yield return null;
        }
    }
    public void SetPowerState(bool isOn){
        _isPowered = isOn;
        if(isOn){
            _powerUsage = _powerIdle;
        }
        else if(!isOn){
            
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

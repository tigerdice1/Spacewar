using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity : MonoBehaviour
{
    private float _powerUsage;
    private float _powerIdle;
    private float _powerActive;
    private float _powerUpdateMultiplier;
    private bool _isPowered;
    private Coroutine _powerUsageCoroutine;

    IEnumerator PowerOn(){
        while(_powerUsage >= _powerActive){
            _powerUsage = Mathf.Lerp(_powerUsage, _powerActive, Time.deltaTime);

            yield return null;
        }
    }
    public void SetPowerState(bool isOn){
        _isPowered = isOn;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_isPowered){

        }
    }
}

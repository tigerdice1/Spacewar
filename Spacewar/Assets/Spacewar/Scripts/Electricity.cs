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
        while(_powerUsage >= _powerActive){
            float newPowerUsage = Mathf.Lerp(_powerUsage, _powerActive, Time.deltaTime);
            _powerUsage = newPowerUsage;

            yield return null;
        }
    }
    public void SetPowerState(bool isOn){
        _isPowered = isOn;
        _powerUsage = _powerIdle;
        _powerUsageCoroutine = StartCoroutine(PowerOnCoroutine());
    }
    // Start is called before the first frame update
    void Start()
    {
        SetPowerState(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

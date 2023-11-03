using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity : MonoBehaviour
{
    /* Power consumption refers to the total power consumption of an object using electricity */
    [SerializeField]
    private float _powerConsumption;
    [SerializeField]
    private float _powerIdle;
    [SerializeField]
    private float _powerActive;
    [SerializeField]
    private bool _isPowered;
    private bool _isInitalized;

    private Coroutine _powerConsumptionCoroutine;


    /* Properties */
    public bool IsPowered{
        get { return _isPowered; }
        set { _isPowered = value; }
    }
    public float PowerConsumption{
        get { return _powerConsumption; }
        set { _powerConsumption = value; }
    }
    public float PowerIdle{
        get { return _powerIdle; }
        set { _powerIdle = value; }
    }
    public float PowerActive{
        get { return _powerActive; }
        set { _powerActive = value; }
    }
    IEnumerator PowerOnCoroutine(){
        if(_powerConsumptionCoroutine != null){
            StopCoroutine(_powerConsumptionCoroutine);
        }
        while(_powerConsumption <= _powerActive){
            float newpowerConsumption = Mathf.Lerp(_powerConsumption, _powerActive, Time.deltaTime);
            _powerConsumption = newpowerConsumption;

            yield return null;
        }
    }
    IEnumerator PowerOffCoroutine(){
        if(_powerConsumptionCoroutine != null){
            StopCoroutine(_powerConsumptionCoroutine);
        }
        while(_powerConsumption <= 0){
            float newpowerConsumption = Mathf.Lerp(_powerConsumption, 0, Time.deltaTime);
            _powerConsumption = newpowerConsumption;

            yield return null;
        }
    }
    public void SetPowerState(bool isOn){
        _isPowered = isOn; 
        _powerConsumption = _isPowered ? _powerIdle : 0.0f;
    }

    private void CheckPowerState(){
        if(_isInitalized){
            _powerConsumption = _isPowered ? _powerIdle : 0.0f;
            _isInitalized = true;
        }
        if(!_isPowered){
            _isInitalized = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate(){
        CheckPowerState();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

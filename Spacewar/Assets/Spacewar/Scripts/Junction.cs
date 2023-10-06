using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Junction : MonoBehaviour
{
    [SerializeField]
    private PowerGenerator _generator;
    [SerializeField]
    private Electricity[] _connectedObjectsList;
    private float _totalPower;
    // Start is called before the first frame update
    void UpdatePowerUsage(){
        _totalPower = 0.0f;
        foreach(Electricity connectedObject in _connectedObjectsList){
            _totalPower += connectedObject.PowerUsage; 
        }
    }

    void SyncPowerUsage(){
        if(_generator.Power -_totalPower < 0.0f){

        }
    }
    void Start()
    {
        
    }

    private void FixedUpdate() {
        UpdatePowerUsage();    
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(_totalPower);
    }
}

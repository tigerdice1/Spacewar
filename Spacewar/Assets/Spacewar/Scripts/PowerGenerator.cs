using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    발전기는 말그대로 전기 생산만을 전담한다. 
    발전기에서 전기를 얼마나 생산할지 직접 조절할 수 있고
    보통은 분배기에서 요구하는 전력량에 맞추어 자동적으로 조절된다.
    로드율을 올리면 전력 생산량이 늘어나는 식
    100% 초과시 과열
    임계온도 초과시 화재
    화재 후 일정시간 경과시 폭발
*/
public class PowerGenerator : MonoBehaviour
{
    [SerializeField]
    [Tooltip("해당 발전기의 소유함선을 지정")]
    private GameObject _ownerShip;

    [SerializeField]
    [Tooltip("해당 발전기와 연결된 분배기를 지정")]
    private Junction _connectedJunction;

    [SerializeField]
    [Tooltip("현재 트리거 되어있는 컨트롤러")]
    private PlayerController _triggeredController;

    [SerializeField]
    [Tooltip("조명")]
    private Light _lightComponent;

    [SerializeField]
    [Tooltip("사용할 UI")]
    private GameObject _generatorUI;

    /* 발전기 정보 */
    [SerializeField]
    [Range(0, 100)]
    [Tooltip("발전기의 효율 설정 단위 : 백분율")]
    private float _efficiency;

    [SerializeField]
    [Tooltip("현재 연료량")]
    private float _fuel;
    [SerializeField]
    [Tooltip("최대 연료량")]
    private float _maxFuel;

    [SerializeField]
    [Tooltip("현재 출력중인 전력량")]
    private float _power;
    [SerializeField]
    [Tooltip("최대로 출력가능한 전력량")]
    private float _maxPower;

    [SerializeField]
    [Range(0, 100)]
    [Tooltip("발전기의 현재 부하량")]
    private float _load;

    [SerializeField]
    [Tooltip("발전기의 현재 온도")]
    private float _thermal;
    [SerializeField]
    [Tooltip("발전기의 임계 온도")]
    private float _criticalThermal;
    [SerializeField]
    [Tooltip("발전기의 임계 온도가 유지된 시간")]
    private float _criticalThermalTimer;

    private bool _isCritical;

    [SerializeField]
    [Tooltip("발전기 작동여부")]
    private bool _isPowered;

    [SerializeField]
    [Tooltip("업데이트 주기")]
    private float _updateCycleTime;

    /* 시간계산용 변수 */
    private float _fuelTimer;
    private float _thermalTimer;

    /* Properties */
    public float Load{
        set{_load = value; }
        get{return _load; }
    }
    public float Fuel{
        set{_fuel = value; }
        get{return _fuel; }
    }

    public float MaxFuel{
        set{_maxFuel = value; }
        get{return _maxFuel; }
    }
    public bool GetGeneratorState(){
        return _isPowered;
    }
    public void SetGeneratorState(bool isOn){
        _isPowered = isOn;
    }

    bool CheckFuel(){
        if(_fuel < 0.0f){
             SetGeneratorState(false);
            return false;
        }
        else{
            return true;
        }
    }

    void CheckGeneratorLight(){
        if(_isPowered){
            if(_isCritical){
                _lightComponent.GetComponent<Electricity>().SetPowerState(true);
                _lightComponent.GetComponent<LightController>().SetLightColor(Color.red);
            }
            else{
                _lightComponent.gameObject.GetComponent<Electricity>().SetPowerState(true);
                _lightComponent.GetComponent<LightController>().SetLightColor(Color.green);
            }
        }
        else if(!_isPowered){
            _lightComponent.gameObject.GetComponent<Electricity>().SetPowerState(false);
        }
    }
    void CalcFuelConsume(){
        _fuel -= _load / Mathf.Pow(_efficiency, 2);
        _power = _maxPower / 100.0f * _load;
    }

    // 발전기의 온도를 체크해서 임계온도 도달 시 경고음 / 이상효과 / 시간 측정
    void CheckGeneratorThermal(){
        if(_thermal > _criticalThermal){
            _thermalTimer += Time.deltaTime;
            _isCritical = true;
        }
        else if(_thermal < _criticalThermal){
            _thermalTimer = 0.0f;
            _isCritical = false;
        }
        if(_thermalTimer >= _criticalThermalTimer){
            // 임계온도 도달 후 시간 경과 이후 액션
            //if (_lightController != null){
            //    _lightController.LightStateColor = LightController.LightState.Over;
            //    Debug.Log("Critical");
            //}
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") && _triggeredController == null){
            _triggeredController = other.GetComponent<PlayerHuman>().PlayerController;
            _triggeredController.TriggerObject = gameObject;
        }

    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player") && _triggeredController != null){
            _triggeredController.TriggerObject = null;
            _triggeredController = null;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SetGeneratorState(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(_isPowered){
            CheckFuel();
            CalcFuelConsume();
            CheckGeneratorThermal();
            
        }
        CheckGeneratorLight();
    }
}

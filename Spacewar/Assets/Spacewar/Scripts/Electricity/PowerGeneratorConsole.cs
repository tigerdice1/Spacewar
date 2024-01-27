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
public class PowerGeneratorConsole : MonoBehaviour
{
    /* Essential Variables */
    // Specifies the ship that owns the generator.
    [SerializeField]
    [Tooltip("해당 발전기의 소유함선을 지정")]
    private GameObject _ownerShip;
    private bool _isOwnerShipLoaded;

    // Specifies the Junction connected to the generator. If not, an error will occur.
    [SerializeField]
    [Tooltip("해당 발전기와 연결된 정션을 지정")]
    private Junction _connectedJunction;
    private bool _isJunctionLoaded;

    /*
    The player's controller that can interact with this generator. 
    It is automatically specified when the phone that the player manipulates comes into the trigger.
    */ 
    [SerializeField]
    [Tooltip("현재 트리거 되어있는 컨트롤러")]
    private PlayerController _triggeredController;

    /* Optional Variables */
    // Lighting object to be used in various situations such as generator operation and errors. Not required and will Disabled if not specified.
    [SerializeField]
    [Tooltip("조명")]
    private Light _lightComponent;
    private bool _isLightComponentLoaded;
    
    // UI to be used when interacting with the generator.Disabled if not specified.
    [SerializeField]
    [Tooltip("사용할 UI")]
    private GameObject _generatorUI;
    private bool _isGeneratorUILoaded;

    /* Generator Info */
    // It's the efficiency of the generator. The more efficient, the higher the output under less load. Range 0 ~ 100
    [SerializeField]
    [Range(0, 100)]
    [Tooltip("발전기의 효율 설정 단위 : 백분율")]
    private float _efficiency;

    // Current amount of fuel in the generator. Range 0 ~ _maxFuel
    [SerializeField]
    [Tooltip("현재 연료량")]
    private float _currentFuel;
    // Maximum amount of fuel in the generator. Range 0 ~ 
    [SerializeField]
      [Tooltip("최대 연료량")]
    private float _maxFuel;

    // The amount of power currently being output.
    [SerializeField]
    [Tooltip("현재 출력중인 전력량")]
    private float _powerOutput;
    // Maximum output power.
    [SerializeField]
    [Tooltip("최대로 출력가능한 전력량")]
    private float _maxPower;

    // Current load on the generator.
    [SerializeField]
    [Range(0, 100)]
    [Tooltip("발전기의 현재 부하량")]
    private float _load;
    // Current Thermal on the generator.
    [SerializeField]
    [Tooltip("발전기의 현재 온도")]
    private float _currentThermal;

    // The critical temperature of the generator.
    [SerializeField]
    [Tooltip("발전기의 임계 온도")]
    private float _criticalThermal;
    // The time the critical temperature of the generator was maintained. Usually, the action occurs after this time.
    [SerializeField]
    [Tooltip("발전기의 임계 온도가 유지된 시간")]
    private float _criticalThermalTimer;
    private bool _isCritical;

    // A variable that indicates whether the generator is operating.
    [SerializeField]
    [Tooltip("발전기 작동여부")]
    private bool _isPowered;
    
    // Update cycle of the generator. Use to adjust the update rate for a particular action.
    [SerializeField]
    [Tooltip("업데이트 주기")]
    private float _updateCycleTime;

    /* 시간계산용 변수 */
    private float _fuelTimer;
    private float _thermalTimer;

    private void Initalize(){
        if(!_ownerShip){
            Debug.Log("OwnerShip is not initialized. The associated functions are disabled. Please Set the OwnerShip. Location : " + gameObject);
            _isOwnerShipLoaded = false;
        }
        else{
            _isOwnerShipLoaded = true;
        }
        if(!_connectedJunction){
            Debug.Log("ConnectedJunction is not initialized. The associated functions are disabled. Please Set the ConnectedJunction. Location : " + gameObject);
            _isJunctionLoaded = false;
        }
        else{
            _isJunctionLoaded = true;
        }
        if(!_lightComponent){
            Debug.Log("LightComponent is not initialized. The associated functions are disabled. Location : " + gameObject);
            _isLightComponentLoaded = false;
        }
        else{
            _isLightComponentLoaded = true;
        }
        if(!_generatorUI){
            Debug.Log("GeneratorUI is not initialized. The associated functions are disabled. Location : " + gameObject);
            _isGeneratorUILoaded = false;
        }
        else{
            _isGeneratorUILoaded = true;
        }       
        if(!gameObject.GetComponent<BoxCollider>().isTrigger){
            Debug.Log("BoxCollider's trigger is missing. Please add it in the editor. Location : " + gameObject);
        }
    }

    /* Properties */
    public bool IsPowered{
        set{_isPowered = value; }
        get{return _isPowered; }
    }
    public float PowerOutput{
        set{_powerOutput = value; }
        get{return _powerOutput; }
    }
    public float Load{
        set{_load = value; }
        get{return _load; }
    }
    public float CurrentFuel{
        set{_currentFuel = value; }
        get{return _currentFuel; }
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
    public GameObject GetUI(){
        if(_isGeneratorUILoaded){
            return _generatorUI;
        }
        else return null;
    }
    public bool GetIsCritical(){
        return _isCritical;
    }

    /* Essential Functions */
    public void SyncPower(float powerUsage){
        float targetLoad = (powerUsage / _maxPower) * 100.0f;
        float clamppedLoad = Mathf.Clamp(targetLoad, 0.0f, _maxPower);
        _load = Mathf.Lerp(_load, clamppedLoad, Time.deltaTime * 3.0f);
    }
    private bool CheckFuelAmount(){
        if(_currentFuel < 0.0f){
             SetGeneratorState(false);
            return false;
        }
        else{
            return true;
        }
    }
    private void CalcPowerOutput(){
        _currentFuel -= _load / Mathf.Pow(_efficiency, 2);
        _powerOutput = _maxPower / 100.0f * _load;
    }

    // 발전기의 온도를 체크해서 임계온도 도달 시 경고음 / 이상효과 / 시간 측정
    private void CheckGeneratorThermal(){
        if(_currentThermal > _criticalThermal){
            _thermalTimer += Time.deltaTime;
            _isCritical = true;
        }
        else if(_currentThermal < _criticalThermal){
            _thermalTimer = 0.0f;
            _isCritical = false;
        }
        if(_thermalTimer >= _criticalThermalTimer){
            // 임계온도 도달 후 시간 경과 이후 액션
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

    /* Optional Functions */
    private void UpdateGeneratorLight(){
        if(_isPowered){
            _lightComponent.GetComponent<Electricity>().SetActiveState(Electricity.State.ACTIVE);
            if(_isCritical){
                _lightComponent.GetComponent<LightController>().SetLightColor(Color.red);
            }
            else{
                _lightComponent.GetComponent<LightController>().SetLightColor(Color.green);
            }
        }
        else if(!_isPowered){
            _lightComponent.gameObject.GetComponent<Electricity>().SetActiveState(Electricity.State.OFF);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Initalize();
        SetGeneratorState(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(_isPowered){
            CheckFuelAmount();
            CalcPowerOutput();
            CheckGeneratorThermal();
        }
        if(_isLightComponentLoaded){
            UpdateGeneratorLight();
        }
    }
}

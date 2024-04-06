using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Double : MonoBehaviour{
    
    // 단일개체
    [SerializeField]
    private Transform[] _doors = new Transform[2];
    private bool[] _isDoorLoaded = new bool[2];
    private bool _hasElectricity;
    public float _doorOpenDistance; // 문이 열리는 거리
    public float _doorOpenSpeed; // 문이 열리는 속도
    public float _doorCloseTimer; // 플레이어가 떠난 후 문의 닫히기까지의 시간

    private Vector3[] _doorClosedPosition = new Vector3[2];
    private Vector3[] _doorCurrentPosition = new Vector3[2];
    private Vector3[] _doorOpenPosition = new Vector3[2];

    private bool _isOpen = false; // 문이 열려있는지 여부
    private float _autoCloseTimer; // 문이 자동으로 닫히는 시간을 카운트
    private float _totalDistance; // 문이 열리는 총 거리
    private Coroutine _doorCoroutine; // 문 이동 코루틴

    private void Initalize(){
        _doorOpenDistance = 0.5f;
        _doorOpenSpeed = 2f;
        // 초기 위치 저장
        _doorClosedPosition[0] = _doors[0].localPosition;
        _doorClosedPosition[1] = _doors[1].localPosition;

        // 열린 위치 계산
        _doorOpenPosition[0] = _doorClosedPosition[0] + Vector3.left * _doorOpenDistance;
        _doorOpenPosition[1] = _doorClosedPosition[1] + Vector3.left * -_doorOpenDistance;

        // 문이 열리는 길이를 계산
        _totalDistance = Vector3.Distance(_doorOpenPosition[0], _doorClosedPosition[0]);
        Debug.Log(gameObject.GetComponent<Electricity>());
        if(!gameObject.GetComponent<Electricity>()){
            Debug.Log("Electricity is not Loaded. Please add Electricity Module. Location : " + gameObject);
            _hasElectricity = false;
        }
        else{
            _hasElectricity = true;
        }
    }

    private void Start(){
        Initalize();
    }

    private void OnTriggerEnter(Collider other){
        if(!_hasElectricity){
            Debug.Log("OnTriggerEnter(Collider other) Function is not work Property. Electricity is not Loaded. Please add Electricity Module. Location : " + gameObject);
        }
        else{
            if(gameObject.GetComponent<Electricity>().IsPowered){
                // 지금 문의 위치를 저장
                _doorCurrentPosition[0] = _doors[0].localPosition;
                // 자동문 타이머 0으로 설정
                _autoCloseTimer = 0f;
                if (_doorCoroutine != null){
                    StopCoroutine(_doorCoroutine);
                }
                if (!_isOpen && other.CompareTag("Player")){
                    _doorCoroutine = StartCoroutine(OpenDoors());
                }
            }
        }
    }

    private void OnTriggerStay(Collider other){
        _autoCloseTimer = 0f;
    }

    private IEnumerator OpenDoors(){
        if (gameObject.GetComponent<Electricity>().IsPowered){
            float currentDistance = Vector3.Distance(_doorCurrentPosition[0], _doorClosedPosition[0]);
            float elapsedTime = (currentDistance / _totalDistance) * _doorOpenSpeed;
            while (elapsedTime < _doorOpenSpeed){
                //if (_electricity.PowerUsage <= _electricity.PowerActive)
                //{
                //    _electricity.PowerUsage += Time.deltaTime * _doorOpenSpeed * 10;
                //}
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / _doorOpenSpeed);
                // Lerp 함수를 사용하여 문을 열기
                _doors[0].localPosition = Vector3.Lerp(_doorClosedPosition[0], _doorOpenPosition[0], t);
                _doors[1].localPosition = Vector3.Lerp(_doorClosedPosition[1], _doorOpenPosition[1], t);
                yield return null;
            }
            _isOpen = true;
            //_electricity.IsActive = false;
            //_electricity.PowerUsage = _electricity.PowerIdle;
        }

    }

    private IEnumerator CloseDoors(){
        if (gameObject.GetComponent<Electricity>().IsPowered){
            //_electricity.IsActive = true;
            float elapsedTime = 0f;
            while (elapsedTime < _doorOpenSpeed){
                //if (_electricity.PowerUsage <= _electricity.PowerActive)
                //{
                //    _electricity.PowerUsage += Time.deltaTime * _doorOpenSpeed * 10;
                //}
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / _doorOpenSpeed);
                // Lerp 함수를 사용하여 문을 닫기
                _doors[0].localPosition = Vector3.Lerp(_doorClosedPosition[0], _doorOpenPosition[0], t);
                _doors[1].localPosition = Vector3.Lerp(_doorClosedPosition[1], _doorOpenPosition[1], t);

                yield return null;
            }
            //_electricity.IsActive = false;
            //_electricity.PowerUsage = _electricity.PowerIdle;
        }
    }

    private void UpdateDoor(){
        if (_hasElectricity){
            if(gameObject.GetComponent<Electricity>().IsPowered){
                if (_isOpen){
                    _autoCloseTimer += Time.deltaTime;
                    if (_autoCloseTimer > _doorCloseTimer){
                        _doorCoroutine = StartCoroutine(CloseDoors());
                        _isOpen = false;
                    }
                }
            }  
        }
    }
    private void Update()
    {
        UpdateDoor();
    }
}

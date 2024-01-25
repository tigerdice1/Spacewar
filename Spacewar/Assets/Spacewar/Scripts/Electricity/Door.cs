using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // 단일개체
    [SerializeField]
    [Tooltip("왼쪽 문")]
    private Transform _leftDoor;
    
    [SerializeField]
    [Tooltip("오른쪽 문")]
    private Transform _rightDoor;

    private Transform[] _doors = new Transform[2];
    private bool[] _isDoorLoaded = new bool[2];
    private bool _isElectricityLoaded;
    public PowerGeneratorController _powerGenerator;

    public float _doorOpenDistance; // 문이 열리는 거리
    public float _doorOpenSpeed; // 문이 열리는 속도
    public float _doorCloseTimer; // 플레이어가 떠난 후 문의 닫히기까지의 시간

    private Vector3 _leftDoorClosedPosition; // 왼쪽 문의 닫힌 위치
    private Vector3 _rightDoorClosedPosition; // 오른쪽 문의 닫힌 위치
    private Vector3 _doorCurrentPosition; // 왼쪽 문의 현재 위치
    private Vector3 _leftDoorOpenPosition; // 왼쪽 문의 열린 위치
    private Vector3 _rightDoorOpenPosition; // 오른쪽 문의 열린 위치

    private bool _isOpen = false; // 문이 열려있는지 여부
    private float _autoCloseTimer; // 문이 자동으로 닫히는 시간을 카운트
    private float _totalDistance; // 문이 열리는 총 거리
    private Coroutine _doorCoroutine; // 문 이동 코루틴

    private void Initalize(){

    }

    private void Start()
    {
        
        //_powerGenerator = gameObject.GetComponent<PowerGenerator>();
        _doorOpenDistance = 0.5f;
        _doorOpenSpeed = 2f;
        // 초기 위치 저장
        _leftDoorClosedPosition = _leftDoor.localPosition;
        _rightDoorClosedPosition = _rightDoor.localPosition;

        // 열린 위치 계산
        _leftDoorOpenPosition = _leftDoorClosedPosition + new Vector3(-1.0f, 0f,0f) * _doorOpenDistance;
        _rightDoorOpenPosition = _rightDoorClosedPosition + new Vector3(-1.0f, 0f, 0f) * -_doorOpenDistance;

        // 문이 열리는 길이를 계산
        _totalDistance = Vector3.Distance(_leftDoorOpenPosition, _leftDoorClosedPosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_powerGenerator.GetGeneratorState())
        {
            // 지금 문의 위치를 저장
            _doorCurrentPosition = _leftDoor.localPosition;
            // 자동문 타이머 0으로 설정
            _autoCloseTimer = 0f;
            if (_doorCoroutine != null)
            {
                StopCoroutine(_doorCoroutine);
            }
            if (!_isOpen && other.CompareTag("Player"))
            {
                _doorCoroutine = StartCoroutine(OpenDoors());
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        _autoCloseTimer = 0f;
    }

    private IEnumerator OpenDoors()
    {
        //Debug.Log("Open");
        if (_powerGenerator.GetGeneratorState())
        {
            //_electricity.IsActive = true;
            float currentDistance = Vector3.Distance(_doorCurrentPosition, _leftDoorClosedPosition);
            float elapsedTime = (currentDistance / _totalDistance) * _doorOpenSpeed;
            while (elapsedTime < _doorOpenSpeed)
            {
                //if (_electricity.PowerUsage <= _electricity.PowerActive)
                //{
                //    _electricity.PowerUsage += Time.deltaTime * _doorOpenSpeed * 10;
                //}
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / _doorOpenSpeed);
                // Lerp 함수를 사용하여 문을 열기
                _leftDoor.localPosition = Vector3.Lerp(_leftDoorClosedPosition, _leftDoorOpenPosition, t);
                _rightDoor.localPosition = Vector3.Lerp(_rightDoorClosedPosition, _rightDoorOpenPosition, t);
                yield return null;
            }
            _isOpen = true;
            //_electricity.IsActive = false;
            //_electricity.PowerUsage = _electricity.PowerIdle;
        }

    }

    private IEnumerator CloseDoors()
    {
        if (_powerGenerator.GetGeneratorState())
        {
            //_electricity.IsActive = true;
            float elapsedTime = 0f;
            while (elapsedTime < _doorOpenSpeed)
            {
                //if (_electricity.PowerUsage <= _electricity.PowerActive)
                //{
                //    _electricity.PowerUsage += Time.deltaTime * _doorOpenSpeed * 10;
                //}
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / _doorOpenSpeed);
                // Lerp 함수를 사용하여 문을 닫기
                _leftDoor.localPosition = Vector3.Lerp(_leftDoorOpenPosition, _leftDoorClosedPosition, t);
                _rightDoor.localPosition = Vector3.Lerp(_rightDoorOpenPosition, _rightDoorClosedPosition, t);

                yield return null;
            }
            //_electricity.IsActive = false;
            //_electricity.PowerUsage = _electricity.PowerIdle;
        }
    }

    private void Update()
    {
        if (_powerGenerator.GetGeneratorState())
        {
            if (_isOpen)
            {
                _autoCloseTimer += Time.deltaTime;
                if (_autoCloseTimer > _doorCloseTimer)
                {
                    _doorCoroutine = StartCoroutine(CloseDoors());
                    _isOpen = false;
                }
            }
        }
        else if (!_powerGenerator.GetGeneratorState())
        {
            //_electricity.PowerUsage = 0f;
        }
    }
}

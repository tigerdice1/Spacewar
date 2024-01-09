using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Door_Unique : MonoBehaviour
{
    // 단일개체
    [SerializeField]
    private Transform _door;
    private Transform _triggerButton;
    private bool _isDoorLoaded;
    private bool _hasElectricity;
    public float _doorOpenDistance; // 문이 열리는 거리
    public float _doorOpenSpeed; // 문이 열리는 속도
    public float _doorCloseTimer; // 플레이어가 떠난 후 문의 닫히기까지의 시간

    private Vector3 _doorClosedPosition;
    private Vector3 _doorCurrentPosition;
    private Vector3 _doorOpenPosition;
    
    private Vector3 _triggerButtonDefaltRot;
    private Vector3 _triggerButtonCurrentRot;
    private Vector3 _triggerButtonActiveRot;


    private bool _isOpen = false; // 문이 열려있는지 여부
    private float _autoCloseTimer; // 문이 자동으로 닫히는 시간을 카운트
    private float _totalDistance; // 문이 열리는 총 거리
    private Coroutine _doorCoroutine; // 문 이동 코루틴

    void Initialize()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(_triggerButtonCurrentRot * Time.deltaTime);
    }
}

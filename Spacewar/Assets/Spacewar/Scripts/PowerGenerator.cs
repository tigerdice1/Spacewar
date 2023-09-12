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

    /* 발전기 정보 */
    [SerializeField]
    [Range(0, 100)]
    [Tooltip("발전기의 효율 설정 단위 : 백분율")]
    private float _efficiency;

    [SerializeField]
    [Tooltip("발전기 현재 연료량")]
    private float _fuel;
    [SerializeField]
    [Tooltip("발전기 최대 연료량")]
    private float _maxFuel;

    [SerializeField]
    [Tooltip("발전기가 현재 출력중인 전력량")]
    private float _power;
    [SerializeField]
    [Tooltip("발전기가 최대로 출력가능한 전력량")]
    private float _maxPower;

    [SerializeField]
    [Range(0, 100)]
    [Tooltip("발전기의 현재 부하량")]
    private float _load;
    [SerializeField]
    [Tooltip("발전기의 현재 온도")]
    private float _temperture;

    [SerializeField]
    [Tooltip("발전기의 임계 온도")]
    private float _tempertureLimit;

    [SerializeField]
    [Tooltip("발전기 작동여부")]
    private bool _isPowered;

    [SerializeField]
    [Tooltip("발전기 전력 소비량 업데이트 주기")]
    private float _updateCycleTime;

    /* 시간계산용 변수 */
    private float _timer;

    // 발전기의 온도를 체크해서 임계온도 도달 시 경고음 / 이상효과 / 시간 측정
    void CheckGeneratorTemperture(){

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

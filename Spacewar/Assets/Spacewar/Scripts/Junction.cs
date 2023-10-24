using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Junction : MonoBehaviour
{
	// 연결된 대상 발전기
    [SerializeField]
    private PowerGenerator _generator;
	// 연결된 오브젝트 목록
    [SerializeField]
    private Electricity[] _connectedObjectsList;
 	// 총 전력 소비량   
    [SerializeField]
	private float _totalPower;
    

	// 오브젝트 리스트의 파워 소비량을 불러와서 총 소비량을 계산함
    void UpdatePowerUsage(){
        _totalPower = 0.0f;
        foreach(Electricity connectedObject in _connectedObjectsList){
            _totalPower += connectedObject.PowerUsage;
        }
    }
	// 총 전력 소비량보다 파워 생산량이 적을 경우 파워에 로드율을 올리게끔 요청
    void SyncPowerUsage(){
        _generator.SyncPower(_totalPower);

    }
 	// Start is called before the first frame update   
	void Start()
    {
        
    }

    private void FixedUpdate() {
           
    }
    // Update is called once per frame
    void Update()
    {
        UpdatePowerUsage();
        SyncPowerUsage();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationScript : MonoBehaviour
{   
    [SerializeField]
    private List<Transform> _patrolPoints;
    [SerializeField]  // 순찰 포인트
    private float _chaseRange = 10f;    // 플레이어 추적 범위
    private float _attackRange = 2f; 
    [SerializeField]   // 공격 범위
    private float _patrolSpeed = 3f;    // 순찰 속도
    [SerializeField]
    private Transform _player;
    private NavMeshAgent _agent;
    private int _currentPatrolIndex; 



    [SerializeField]
    private float _speed;

    public Transform Player{
        get => _player;
        set => _player = value;
    }
    public float Speed{
        get => _speed;
        set => _speed = value;
    }
    
    public List<Transform> GetPatrolPoints(){
        return _patrolPoints;        
    }
    public List<Transform> SetPatrolPoints(List<Transform> value){
        return _patrolPoints = value;
    }

    enum State {Idle,Patrol,Chase,Attack}
    State _currentState;
  
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _currentState = State.Patrol;  // 기본 상태는 순찰
        _agent.speed =_speed;
        GoToNextPatrolPoint();
    }

    // Update is called once per frame
    void Update()
    {  
        switch (_currentState)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Chase:
                Chase();
                break;
            case State.Attack:
                Attack();
                break;
        }

        // 플레이어와의 거리 계산
        float _distanceToPlayer = Vector3.Distance(transform.position, _player.position);
        
        if (_distanceToPlayer <= _attackRange)
        {
            _currentState = State.Attack; // 공격 범위에 들어가면 공격
        }
        else if (_distanceToPlayer <= _chaseRange)
        {
            _currentState = State.Chase; // 추적 범위에 있으면 추적
        }
        else if (_currentState != State.Patrol)
        {
            _currentState = State.Patrol; // 범위 밖이면 순찰
            _agent.speed = _speed;
            GoToNextPatrolPoint();
        }
    }
    

    void Idle(){

    }
    void Patrol(){
        if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
        {
            GoToNextPatrolPoint(); // 다음 순찰 지점으로 이동
        }
    }
    void Chase(){

    }
    void Attack(){
        
    }
    void GoToNextPatrolPoint(){
        if (_patrolPoints.Count == 0)
            return;

        _agent.destination = _patrolPoints[_currentPatrolIndex].position; // 다음 순찰 포인트로 이동
        _currentPatrolIndex = (_currentPatrolIndex + 1) % _patrolPoints.Count; // 인덱스 갱신

    }

}

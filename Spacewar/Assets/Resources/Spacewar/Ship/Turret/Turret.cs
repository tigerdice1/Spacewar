using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Turret : MonoBehaviour, IControllable
{
    public float TurretRotationSpeed;
    [SerializeField]
    private ShipBase _ownerShip;
    [SerializeField]
    private float _rpm;
    [SerializeField]
    private string _bulletPrefabPath = "Spacewar/Prefabs/Object/BULLET";
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private List<Transform> _bulletSpawnPoints = new List<Transform>();
    private int _fireIndex = 0;
    private bool _isFiring = false;
    private float _fireTimer = 0f;
    private float _secondsPerShot;
    
    public float RotationSpeed => TurretRotationSpeed;
    
    public List<Transform> BulletSpawnPoints{
        get => _bulletSpawnPoints;
        set => _bulletSpawnPoints = value;
    }

        public bool IsFiring{
        get => _isFiring;
        set => _isFiring = value;
    }

    void Initialize(){
        // RPM을 기반으로 초당 발사 간격 계산
        _secondsPerShot = 60f / _rpm;
    }

        void Awake(){
        Initialize();
    }

    void OnDebugMode(){

    }

    public void Move(PlayerController controller){
        // Turret Wont move.
    }

    public void Look(PlayerController controller, float maxRotationSpeed, bool useSlerp){
        controller.LookAtCursor(maxRotationSpeed, false);
    }

    public void HandleMouseClick(PlayerController controller){
        IsFiring = true;
    }

    public void UpdateAnimation(PlayerController controller){
        // 필요 시 구현
    }

    private void Fire(){
        _fireTimer += Time.deltaTime;
        if (_fireTimer >= _secondsPerShot)
        {
            if (_bulletSpawnPoints.Count == 0)
            {
                Debug.LogWarning("Bullet Spawn Points are not assigned.");
                return;
            }

            // 순환하여 발사 위치 선택
            _fireIndex %= _bulletSpawnPoints.Count;
            Transform spawnPoint = _bulletSpawnPoints[_fireIndex];
            GameObject bulletInstance;
            if(GameManager.Instance().IsDebugMode){
                bulletInstance = Instantiate(
                _bulletPrefab,
                spawnPoint.position,
                spawnPoint.rotation);
            }
            else{
                // 탄환 인스턴스 생성
                bulletInstance = PhotonNetwork.Instantiate(
                _bulletPrefabPath,
                spawnPoint.position,
                spawnPoint.rotation);
            }
            

            // 탄환의 OwnerShip 설정
            Projectile projectile = bulletInstance.GetComponent<Projectile>();
            if (projectile != null){
                projectile.OwnerShip = _ownerShip;
            }

            _fireTimer = 0f;
            _fireIndex++;
        }
    }

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if(_isFiring){
            Fire();
        }
    }
}

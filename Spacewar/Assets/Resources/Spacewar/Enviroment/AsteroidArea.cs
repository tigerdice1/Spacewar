using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidArea : MonoBehaviour
{
    [SerializeField]
    [Tooltip("해당 리스트에 할당된 소행성이 랜덤 생성됩니다.")]
    private List<Asteroid> _asteroids;

    [SerializeField]
    [Tooltip("소행성지대의 범위를 지정합니다.")]
    private Vector3 _areaRadius;

    [Tooltip("소행성을 소행성 지대 범위 안에 랜덤하게 지정하기 위한 Transform 변수입니다.")]
    private Transform _asteroidInstTransform;
    // Start is called before the first frame update
    void Start() {
        /*
        _asteroidInstTransform = this.transform;
        
        this.transform.position = new Vector3(Random.Range(-1f * GameManager.Instance().MapSizeX, GameManager.Instance().MapSizeX),
        0f,
        Random.Range(-1f * GameManager.Instance().MapSizeZ, GameManager.Instance().MapSizeZ));
        
        for(int i = 0; i < Random.Range(1, 50); i++){
            _asteroidInstTransform.position = new Vector3(
                this.transform.localPosition.x + Random.Range(-1f * _areaRadius.x, _areaRadius.x),
                0f,
                this.transform.localPosition.z + Random.Range(-1f * _areaRadius.z, _areaRadius.z));
            Instantiate(_asteroids[Random.Range(0,_asteroids.Count)], _asteroidInstTransform);
        }
        */
    }
    void Awake() {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

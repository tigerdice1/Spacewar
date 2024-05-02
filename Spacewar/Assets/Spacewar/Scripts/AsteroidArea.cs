using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidArea : MonoBehaviour
{
    private SceneManager _sceneManager;
    [SerializeField]
    private List<Asteroid> _asteroids;
    [SerializeField]
    private Vector3 _areaRadius;
    private Transform _fixedTransform;
    // Start is called before the first frame update
    void Start() {
        _fixedTransform = gameObject.transform;
        _sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        gameObject.transform.position = new Vector3(Random.Range(-1f * _sceneManager.MapSizeX, _sceneManager.MapSizeX),
        0f,
        Random.Range(-1f * _sceneManager.MapSizeZ, _sceneManager.MapSizeZ));
        
        for(int i = 0; i < Random.Range(1, 50); i++){
            _fixedTransform.position = new Vector3(
                gameObject.transform.localPosition.x + Random.Range(-1f * _areaRadius.x, _areaRadius.x),
                0f,
                gameObject.transform.localPosition.z + Random.Range(-1f * _areaRadius.z, _areaRadius.z));
            Instantiate(_asteroids[Random.Range(0,_asteroids.Count - 1)], _fixedTransform);
        }
    }
    void Awake() {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

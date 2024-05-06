using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private static SceneManager _instance;
    [SerializeField]
    private bool _isDebugMode;
    [SerializeField]
    private int _mapSize_X;
    [SerializeField]
    private int _mapSize_Z;
    [SerializeField]
    private int _minAsteroidAreas;
    
    [SerializeField]
    private int _maxAsteroidAreas;

    [SerializeField]
    private AsteroidArea _asteroidArea;
    private List<AsteroidArea> _asteroidAreas;


    public int MapSizeX{
        get { return _instance._mapSize_X; }
    }

    public int MapSizeZ{
        get { return _instance._mapSize_Z; }
    }
    
    public static SceneManager Instance(){
        return _instance;
    }

    public bool IsDebugMode(){
        return _instance._isDebugMode;
    }
    void Awake(){
        if(_instance == null){
            _instance = this;
        }
    }
    // Start is called before the first frame update
    void Start(){
        int astCount = UnityEngine.Random.Range(_minAsteroidAreas, _maxAsteroidAreas);
        for(int i = 0; i < astCount; i++){
            _instance._asteroidAreas.Add(Instantiate(_asteroidArea));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

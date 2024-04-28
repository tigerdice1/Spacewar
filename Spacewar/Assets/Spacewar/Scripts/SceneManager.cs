using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField]
    private int _mapSize_X;
    [SerializeField]
    private int _mapSize_Z;
    [SerializeField]
    private int _minAsteroidAreas;
    
    [SerializeField]
    private int _maxAsteroidAreas;
    [SerializeField]
    private List<GameObject> _asteroidAreas;
    [SerializeField]
    private Asteroid _ast;

    public int MapSizeX{
        get { return _mapSize_X; }
    }

    public int MapSizeZ{
        get { return _mapSize_Z; }
    }
    // Start is called before the first frame update
    void Start()
    {
        int astCount = UnityEngine.Random.Range(_minAsteroidAreas, _maxAsteroidAreas);
        for(int i = 0; i < astCount; i++){
            _asteroidAreas.Add(new GameObject("AsteroidArea", typeof(AsteroidArea)));
            _asteroidAreas[i].GetComponent<AsteroidArea>()._maxX = _mapSize_X;
            _asteroidAreas[i].GetComponent<AsteroidArea>()._maxZ = _mapSize_Z;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

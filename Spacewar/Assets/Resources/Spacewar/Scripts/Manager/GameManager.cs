using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
public class GameManager : MonoBehaviourPunCallbacks
{
    private static GameManager _instance;
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
    [SerializeField]
    private List<AsteroidArea> _asteroidAreas;

    private List<Player> _team1Player;
    private List<Player> _team2Player;
    public int MapSizeX{
        get => _instance._mapSize_X;
    }

    public int MapSizeZ{
        get => _instance._mapSize_Z;
    }
    
    public static GameManager Instance(){
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
        Player[] players = PhotonNetwork.PlayerList;
        foreach(Player player in players){
            if((int)player.CustomProperties["Team"] == 0){
                _team1Player.Add(player);
            }
            else if((int)player.CustomProperties["Team"] == 1){
                _team2Player.Add(player);
            }
        }
        int astCount = UnityEngine.Random.Range(_minAsteroidAreas, _maxAsteroidAreas);
        for(int i = 0; i < astCount; i++){
            _asteroidAreas.Add(Instantiate(_asteroidArea));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

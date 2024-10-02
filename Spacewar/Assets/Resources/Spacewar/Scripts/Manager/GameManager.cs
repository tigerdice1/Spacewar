using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using CustomTypes;

public class GameManager : MonoBehaviourPunCallbacks
{
    private static GameManager _instance;
    [SerializeField]
    private bool _isDebugMode;
    [SerializeField]
    private CustomTypes.Coordniate2D _mapSize;
    [SerializeField]
    private int _minAsteroidAreas;
    
    [SerializeField]
    private int _maxAsteroidAreas;

    [SerializeField]
    private AsteroidArea _asteroidArea;
    [SerializeField]
    private List<AsteroidArea> _asteroidAreas;

    private List<Player> _team1Player = new List<Player>();
    private List<Player> _team2Player = new List<Player>();

    public GameObject[] _playerModels;
    public GameObject _playerController;

    public GameObject _playerUI;
    
    public static GameManager Instance(){
        return _instance;
    }

    public bool IsDebugMode{
        get => _isDebugMode;
    }
    void Awake(){
        if(_instance == null){
            _instance = this;
        }
    }
    private void SetPlayerTeam(){
        Player[] players = PhotonNetwork.PlayerList;
        foreach(Player player in players){
            if((int)player.CustomProperties["Team"] == 0){
                _team1Player.Add(player);
            }
            else if((int)player.CustomProperties["Team"] == 1){
                _team2Player.Add(player);
            }
        }
    }

    private void SpawnPlayer(){
        // 플레이어 모델 선택 또는 할당 (예시로 랜덤 선택)
            int modelIndex = Random.Range(0, _playerModels.Length);
            string selectedModelName = _playerModels[modelIndex].name;

            // Photon Custom Properties에 선택된 모델 저장
            ExitGames.Client.Photon.Hashtable playerProps = new ExitGames.Client.Photon.Hashtable();
            playerProps.Add("playerModel", selectedModelName);
            PhotonNetwork.LocalPlayer.SetCustomProperties(playerProps);

            // 모델 인스턴스화
            GameObject playerModel = PhotonNetwork.Instantiate("Spacewar/Scripts/NewPlayer/DefaultPlayerModel", Vector3.zero, Quaternion.identity);
            // 추가로, 캐릭터 컨트롤러 등을 부착하는 코드를 여기에 작성
            playerModel.transform.SetParent(null);
            
            GameObject playerController = PhotonNetwork.Instantiate("Spacewar/Prefabs/PlayerContoller",Vector3.zero, Quaternion.identity);
            playerController.GetComponent<PlayerController>().DefaultControlObject = playerModel;
    }

        private void SpawnDebugPlayer(){
            // 모델 인스턴스화
            GameObject playerModel = Instantiate(_playerModels[0], Vector3.zero, Quaternion.identity);
            // 추가로, 캐릭터 컨트롤러 등을 부착하는 코드를 여기에 작성
            playerModel.transform.SetParent(null);
            
            GameObject playerController = Instantiate(_playerController, Vector3.zero, Quaternion.identity);
            playerController.GetComponent<PlayerController>().DefaultControlObject = playerModel;
            GameObject playerUI = Instantiate(_playerUI, Vector3.zero, Quaternion.identity);
            playerUI.GetComponent<UI_Player>().OwnController = playerController.GetComponent<PlayerController>();
            
    }
    // Start is called before the first frame update
    void Start(){
        if(_instance.IsDebugMode){
            SpawnDebugPlayer();
        }
        else if(PhotonNetwork.IsConnected){
            SetPlayerTeam();
            SpawnPlayer();
        }
            /*
            int astCount = UnityEngine.Random.Range(_minAsteroidAreas, _maxAsteroidAreas);
            for(int i = 0; i < astCount; i++){
            _asteroidAreas.Add(Instantiate(_asteroidArea));
            */
    } 

    // Update is called once per frame
    void Update()
    {
        
    }
}

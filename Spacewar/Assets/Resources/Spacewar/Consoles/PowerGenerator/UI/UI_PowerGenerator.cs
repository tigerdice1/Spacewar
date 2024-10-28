using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class UI_PowerGenerator : UI_Base
{
    public PowerGenerator PowerGenerator;
    private Toggle _powerToggle;
    private UI_Radial_Load _loadRadial;
    private UI_Radial_Temperture _temptureRadial;
    private UI_FuelBar _fuelBar;

    private void OnToggleValueChanged(bool isOn){
        // 로컬 소유자 클라이언트에서만 RPC 호출
        if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient){
            photonView.RPC("SetGeneratorStateRPC", RpcTarget.AllBuffered, isOn);
        }
    }

    [PunRPC]
    public void SetGeneratorStateRPC(bool isOn){
        // 모든 클라이언트에서 토글 버튼 상태 및 관련 동작 변경
        _powerToggle.SetIsOnWithoutNotify(isOn);
        PowerGenerator.SetGeneratorState(isOn);
    }
    protected void Start(){
        _powerToggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    protected void Awake(){
        Transform[] allTransforms = Resources.FindObjectsOfTypeAll<Transform>();
        _powerToggle = allTransforms.FirstOrDefault(t => t.gameObject.name == "Generator_UI_Button").GetComponent<Toggle>();
        _loadRadial = allTransforms.FirstOrDefault(t => t.gameObject.name == "Generator_UI_Load").GetComponent<UI_Radial_Load>();
        _loadRadial.PowerGenerator = PowerGenerator;
        _temptureRadial = allTransforms.FirstOrDefault(t => t.gameObject.name == "Generator_UI_Temperture").GetComponent<UI_Radial_Temperture>();
        _temptureRadial.PowerGenerator = PowerGenerator;
        _fuelBar = allTransforms.FirstOrDefault(t => t.gameObject.name == "Generator_UI_FuelBar").GetComponent<UI_FuelBar>();
        _fuelBar.PowerGenerator = PowerGenerator;
    }
}

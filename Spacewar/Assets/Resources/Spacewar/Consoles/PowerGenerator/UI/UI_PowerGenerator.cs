using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class UI_PowerGenerator : UI_Base
{
    [SerializeField]
    public PowerGenerator PowerGenerator;
    [SerializeField]
    private Toggle _powerToggle;
    [SerializeField]
    private UI_Radial_Load _loadRadial;
    [SerializeField]
    private UI_Radial_Temperture _temptureRadial;
    [SerializeField]
    private UI_FuelBar _fuelBar;

    [PunRPC]
    private void SetGeneratorStateRPC(bool isOn){
        PowerGenerator.SetGeneratorState(isOn);
    }
    private void OnToggleValueChanged(bool isOn){
        photonView.RPC("SetGeneratorStateRPC", RpcTarget.AllBuffered, isOn);
    }

    protected void Start(){
        _powerToggle.onValueChanged.AddListener(OnToggleValueChanged);
        _loadRadial.PowerGenerator = PowerGenerator;
        _temptureRadial.PowerGenerator = PowerGenerator;
    }

    protected void Awake(){
        Transform[] allTransforms = Resources.FindObjectsOfTypeAll<Transform>();
        _powerToggle = allTransforms.FirstOrDefault(t => t.gameObject.name == "Generator_UI_Button").GetComponent<Toggle>();
        _loadRadial = allTransforms.FirstOrDefault(t => t.gameObject.name == "Generator_UI_Load").GetComponent<UI_Radial_Load>();
        
        _temptureRadial = allTransforms.FirstOrDefault(t => t.gameObject.name == "Generator_UI_Temperture").GetComponent<UI_Radial_Temperture>();
        
        _fuelBar = allTransforms.FirstOrDefault(t => t.gameObject.name == "Generator_UI_FuelBar").GetComponent<UI_FuelBar>();
        _fuelBar.PowerGenerator = PowerGenerator;
    }
}

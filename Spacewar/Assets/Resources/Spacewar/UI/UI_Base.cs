using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class UI_Base : MonoBehaviourPunCallbacks
{
    public CanvasGroup UICanvasGroup;
    protected virtual void Start(){
        UICanvasGroup = gameObject.GetComponent<CanvasGroup>();
    }
}

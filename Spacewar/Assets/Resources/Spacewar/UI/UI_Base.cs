using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Base : MonoBehaviour
{
    public CanvasGroup UICanvasGroup;
    protected virtual void Start(){
        UICanvasGroup = gameObject.GetComponent<CanvasGroup>();
    }
}

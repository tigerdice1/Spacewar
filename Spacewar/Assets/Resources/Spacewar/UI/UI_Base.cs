using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Base : MonoBehaviour
{
    public CanvasGroup Canvas;
    protected virtual void Start(){
        Canvas = gameObject.GetComponent<CanvasGroup>();
    }
}

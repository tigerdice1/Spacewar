using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class FixableObjects : MonoBehaviourPunCallbacks
{
    #region Protected Variables
    [SerializeField]
    protected MainShip _ownerShip;
    [SerializeField]
    protected GameObject _objectToControl;
    [SerializeField]
    protected UI_Base _consoleUI;
    protected Coroutine _fixObjectCoroutine;
    protected float _durability = 100f;
    protected bool _isInteractive = true;
    protected bool _soloUseOnly = true;
    #endregion Protected Variables

    public UI_Base ConsoleUI{
        get{return _consoleUI;}
    }
    protected virtual void Initialize(){
    }
    protected IEnumerator FixDurabilityCoroutine(float skill){
        while (!CustomTypes.MathExt.Approximately(_durability, 100f)){
            _durability = Mathf.Lerp(_durability, 100f, Time.deltaTime * skill);
            yield return null;
        }
        _durability = 100f;        
    }
    public void FixObject(float skill){
        _fixObjectCoroutine = StartCoroutine(FixDurabilityCoroutine(skill));
    }
    public void StopFixObject(){
        if(_fixObjectCoroutine!= null){
            StopCoroutine(_fixObjectCoroutine);
        }
    }
    protected virtual void Aging(){
        _durability = Mathf.Lerp(_durability, 0.0f, Time.deltaTime * 0.002f);
        if(_durability <= 0f){
            _durability = 0f;
        }
    }

    protected virtual void Update(){
        Aging();
    }
}

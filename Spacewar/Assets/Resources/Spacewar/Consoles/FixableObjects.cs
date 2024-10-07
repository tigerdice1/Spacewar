using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixableObjects : MonoBehaviour
{
    [SerializeField]
    protected MainShip _ownerShip;
    [SerializeField]
    protected GameObject _objectToControl;
    [SerializeField]
    protected UI_Base _consoleUI;
    protected List<PlayerBase> _handlingPlayers = new List<PlayerBase>();
    protected List<PlayerController> _triggeredControllers = new List<PlayerController>();
    protected BoxCollider _boxCollider;
    protected Coroutine _playingCoroutine;
    protected float _durability = 100f;
    protected bool _isInteractive = true;
    protected bool _soloUseOnly = true;

    public UI_Base ConsoleUI{
        get{return _consoleUI;}
    }
    protected virtual void Initialize(){
        _boxCollider = GetComponent<BoxCollider>();
    }
    protected IEnumerator FixDurabilityCoroutine(float skill){
        while (!CustomTypes.MathExt.Approximately(_durability, 100f)){
            _durability = Mathf.Lerp(_durability, 100f, Time.deltaTime * skill);
            yield return null;
        }
        _durability = 100f;        
    }
    public void FixObject(float skill){
        _playingCoroutine = StartCoroutine(FixDurabilityCoroutine(skill));
    }
    protected virtual void Aging(){
        _durability = Mathf.Lerp(_durability, 0.0f, Time.deltaTime * 0.002f);
        if(_durability <= 0f){
            _durability = 0f;
        }
    }
    protected void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")){
            PlayerController playerController = other.GetComponent<PlayerBase>().PlayerController;
            if (playerController != null){
                playerController.TriggerObject = gameObject;
                _triggeredControllers.Add(playerController);
            }
        }
    }
    protected void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")){
            PlayerController playerController = other.GetComponent<PlayerBase>().PlayerController;
            if (playerController != null){
                playerController.TriggerObject = gameObject;
            }
        }
    }
    protected void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")){
            PlayerController playerController = other.GetComponent<PlayerBase>().PlayerController;
            if (playerController != null){
                _triggeredControllers.Remove(playerController);
                playerController.TriggerObject = null;
                if(_playingCoroutine != null){
                    StopCoroutine(_playingCoroutine);
                }
            }
        }
    }
    protected virtual void Update(){
        Aging();
    }
}

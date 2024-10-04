using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerInven.UI
{
    public class ItemActionPanel : MonoBehaviour
    {
        [SerializeField]
        private GameObject _btnPrefab;

        public void AddBtn(string name,Action onClickAction){
            GameObject btn = Instantiate(_btnPrefab,transform);
            btn.GetComponent<Button>().onClick.AddListener(()=> onClickAction());
            btn.GetComponentInChildren<TMPro.TMP_Text>().text = name;
        }
    

        public void Toggle(bool val){
            if(val){
                RemoveOldBtn();
            }
            gameObject.SetActive(val);
        }

        public void RemoveOldBtn(){
            foreach(Transform transformChildObj in transform){
                Destroy(transformChildObj.gameObject);
            }
        }
    }
    
}
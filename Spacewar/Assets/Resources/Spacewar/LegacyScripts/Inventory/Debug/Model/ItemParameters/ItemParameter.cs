using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PlayerInven.Model{
    [CreateAssetMenu]
    public class ItemParameter : ScriptableObject{
        [SerializeField]
        private string _paramName;

        public string ParamName{
            get => _paramName;
            set => _paramName = value;
        }
    }
}


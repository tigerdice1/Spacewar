using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerInven.Model{
    [CreateAssetMenu]
    public class EquipableItem : Item, IDestroyableItem, IItemAction{
        public string ActionName => "Equip";
        
        private AudioClip _actionSFX;

        public AudioClip ActionSFX{
            get => _actionSFX;
            set => _actionSFX = value;
        }

        public bool PerformAction(GameObject character,List<ItemParam> itemState = null){
            throw new System.NotImplementedException();
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerInven.Model
{
    [CreateAssetMenu]
    public class EdibleItem : Item, IDestroyableItem, IItemAction
    {
        [SerializeField]
        private List<ModifierData> _modifierData = new List<ModifierData>();

        public string ActionName =>"Consume";

        private AudioClip _actionSFX;

        public AudioClip ActionSFX{
            get => _actionSFX;
            set => _actionSFX = value;
        }

        public bool PerformAction(GameObject character,List<ItemParam> itemState = null){
            foreach(ModifierData data in _modifierData){
                data.StatModifier.AffectCharacter(character,data.Value);
            }
            return true;
        }
    }

    public interface IDestroyableItem{

    }

    public interface IItemAction{
        
        public string ActionName{ get; }
        public AudioClip ActionSFX{ get; }
        bool PerformAction(GameObject character,List<ItemParam> itemState);

    }

    [Serializable]
    public class ModifierData{
        [SerializeField]
        private CharacterStatModifier _statModifier;
        [SerializeField]
        private float _value;

        public CharacterStatModifier StatModifier{
            get => _statModifier;
            set => _statModifier = value;
        }
        public float Value{
            get =>_value;
            set =>_value = value;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PlayerInven.Model
{
    public abstract class Item : ScriptableObject
    {
        [SerializeField]
        private bool _isStackable;

        [SerializeField]
        private int _id => GetInstanceID();

        [SerializeField]
        private int _maxStackSize =1;

        [SerializeField]
        private string _name;

        [SerializeField]
        [field: TextArea]
        private string _description;

        [SerializeField]
        private Sprite _itemImage;

        public List<ItemParam> DefaultParameterList;

        public bool GetIsStackable{   
            get => _isStackable; 
        }

        public int GetID{
            get => _id;
        }
        
        public int MaxStackSize{
            get => _maxStackSize;
            set => _maxStackSize = value;
        }

        public string Name{
            get => _name;
            set => _name = value;
        }

        public string Description{
            get => _description;
            set => _description = value;
        }

        public Sprite ItemImage{
            get => _itemImage;
            set => _itemImage = value;
        }
        

    }

    [Serializable]
    public struct ItemParam : IEquatable<ItemParam>
    {
        [SerializeField]
        private ItemParameter _itemParameter;
        [SerializeField]
        private float _value;

        public bool Equals(ItemParam other){
            return other._itemParameter ==_itemParameter;
        }

        public ItemParameter ItemParameter{
            get => _itemParameter;
            set => _itemParameter = value;
        }

        public float Value{
            get => _value;
            set => _value = value;
        }
    }
}
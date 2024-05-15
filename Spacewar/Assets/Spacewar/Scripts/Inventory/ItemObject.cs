using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 추상 클래스 생성
public abstract class ItemObject : ScriptableObject{
    [SerializeField]
    private int _id;
    [SerializeField]
    private Sprite _uiDisplay;
    [SerializeField]
    private EnumTypes _type;
    [SerializeField]
    [TextArea(15,20)]
    private string _description;
    [SerializeField]
    private List<ItemBuffs> _buffs;

    public EnumTypes Type{
        get => _type;
    }
    // public ItemBase CreateItem(){
    //     ItemBase _newItem = new ItemBase(this);
    //     return _newItem;
    // }
}

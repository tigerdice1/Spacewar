using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum ItemType{
    Food,
    Equipment,
    Default
}

public enum Attributes{
    Agility,
    Intellect,
    Stamina,
    Strength
}

public abstract class ItemObject : ScriptableObject{ //항목을 생성하기 위한 기본 추상화 클래스

    public int _id;
    public Sprite _uiDisplay; //인벤토리에 항목을 추가한 후 항목에 대한 디스플레이를 보관할 prefab 변수
    public ItemType type;
    [TextArea(15,20)]
    public string _description; //  항목 설명을 담는 문자열
    public ItemBuff[] _buffs;

    public Item CreateItem()
    {
        Item _newItem = new Item(this);
        return _newItem;
    }
}
[System.Serializable]
public class Item
{
    public string _name;
    public int _id;
    public ItemBuff[] _buffs;
    public Item(ItemObject item)
    {
        _name = item.name;
        _id = item._id;
        _buffs = new ItemBuff[item._buffs.Length];

        for(int i = 0;i < _buffs.Length;i++)
        {
            _buffs[i] = new ItemBuff(item._buffs[i]._min, item._buffs[i]._max)
            {
                _attribute = item._buffs[i]._attribute
            };
        }
        
    }
}
[System.Serializable]
public class ItemBuff
{
    public Attributes _attribute;
    public int _value;
    public int _min;
    public int _max;
    public ItemBuff(int min, int max)
    {
        _min = min;
        _max = max;
        GenerateValue();
    }
    public void GenerateValue() //랜덤값
    {
        _value = UnityEngine.Random.Range(_min, _max);
    }
}
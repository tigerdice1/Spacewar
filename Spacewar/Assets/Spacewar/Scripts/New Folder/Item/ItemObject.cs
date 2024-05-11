using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Item{
    private string _name;
    private int _id;
    private ItemBuff[] _buffs;
    private Item(ItemObject item){
        _name = item.name;
        _id = item._id;
        _buffs = new ItemBuff[item._buffs.Length];

        for(int i = 0;i < _buffs.Length;i++){
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
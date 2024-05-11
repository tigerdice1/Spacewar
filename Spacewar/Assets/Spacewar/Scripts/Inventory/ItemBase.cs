using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour{
    private string _name;
    private int _id;
    private List<ItemBuffs> _buffs;
    private Item(ItemBase item){
        this._name = item.name;
        this._id = item._id;
        this._buffs = new ItemBuff[item._buffs.Length];

        for(int i = 0;i < _buffs.Length;i++){
            _buffs[i] = new ItemBuff(item._buffs[i]._min, item._buffs[i]._max)
            {
                _attribute = item._buffs[i]._attribute
            };
        }
    }
}

using PlayerInven.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    [SerializeField]
    private EquipableItem _weapon;

    [SerializeField]
    private Inventory _inventoryData;

    [SerializeField]
    private List<ItemParam> _paramsToMod, _itemCurrentState;

    public void SetWeapon(EquipableItem weaponItem,List<ItemParam> itemState){
        if(_weapon != null){
            _inventoryData.AddItem(_weapon,1 ,_itemCurrentState);
        }

        this._weapon = weaponItem;
        this._itemCurrentState = new List<ItemParam>(itemState);
        ModifyParam();
    }

    private void ModifyParam(){
        foreach (var param in _paramsToMod){
            if(_itemCurrentState.Contains(param)){
                int index = _itemCurrentState.IndexOf(param);
                float newValue = _itemCurrentState[index].Value + param.Value;
                _itemCurrentState[index] = new ItemParam{
                    ItemParameter = param.ItemParameter,
                    Value = newValue
                };
            }
        }
    }

}

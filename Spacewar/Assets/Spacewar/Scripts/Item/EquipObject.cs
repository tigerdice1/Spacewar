using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]
public class EquipObject : ItemObject {
    //공격 보너스
    public float _atkBonus;
    //방어 보너스
    public float _defenceBonus;
    public void Awake(){
        type = ItemType.Equipment;
    }

}

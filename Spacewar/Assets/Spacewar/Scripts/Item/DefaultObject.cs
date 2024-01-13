using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//기본 오브젝트 에셋메뉴 생성
[CreateAssetMenu(fileName = "New Default Object",menuName = "Inventory System/Items/Default")]
public class DefaultObject: ItemObject
{
    public void Awake(){
        type = ItemType.Default;
    }
}

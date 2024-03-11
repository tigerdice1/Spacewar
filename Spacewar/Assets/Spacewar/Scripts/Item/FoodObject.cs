using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Food 에셋메뉴 생성
[CreateAssetMenu(fileName = "New Food Object", menuName = "Inventory System/Items/Food")]
public class FoodObject : ItemObject
{
    public void Awake(){
        type = ItemType.Food;
    }

}

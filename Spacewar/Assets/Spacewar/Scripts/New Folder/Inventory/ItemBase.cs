using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{

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

    public abstract class ItemBase : ScriptableObject{ //항목을 생성하기 위한 기본 추상화 클래스
        private int _id;
        private Sprite _uiDisplay; //인벤토리에 항목을 추가한 후 항목에 대한 디스플레이를 보관할 prefab 변수
        private ItemType type;

        [TextArea(15,20)]
        private string _description; //  항목 설명을 담는 문자열
        private ItemBuff[] _buffs;

        public Item CreateItem(){
            Item _newItem = new Item(this);
            return _newItem;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType{
    Food,
    Equipment,
    Default
}

public abstract class ItemObject : ScriptableObject{ //항목을 생성하기 위한 기본 추상화 클래스
    public GameObject _prefab; //인벤토리에 항목을 추가한 후 항목에 대한 디스플레이를 보관할 prefab 변수
    public ItemType type;
    [TextArea(15,20)]
    public string _description; //  항목 설명을 담는 문자열
}

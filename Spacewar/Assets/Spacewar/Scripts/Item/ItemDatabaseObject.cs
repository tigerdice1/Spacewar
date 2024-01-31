using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database",menuName = "Inventory System/Items/Database")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    // 게임을 포함하려는 모든 장면에 아이템 데이터베이스를 드래그앤 드롭 할 필요없다.
    public ItemObject[] Items;
    // 아이템 오브젝트와 그에 해당하는 정수 ID를 매핑하는 사전. 각 아이템에 고유한 ID를 할당하는 데 사용
    public Dictionary<ItemObject,int> GetId = new Dictionary<ItemObject,int>();
    public Dictionary<int, ItemObject> GetItem = new Dictionary<int,ItemObject>();
    // 역직렬화 후에 호출되는 메서드. 아이템 오브젝트와 그에 해당하는 ID를 사전에 추가
    public void OnAfterDeserialize()
    {
       GetId = new Dictionary<ItemObject,int>();
        GetItem = new Dictionary<int,ItemObject>();
        for(int i = 0; i < Items.Length; i++)
        {
            GetId.Add(Items[i], i);
            GetItem.Add(i, Items[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        
    }
}

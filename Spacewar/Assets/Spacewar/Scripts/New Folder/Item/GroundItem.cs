using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItem : MonoBehaviour
{
    [SerializeField]
    private ItemObject _item;

    public ItemObject Item{
        set => _item = value;
        get => _item;
    }
}

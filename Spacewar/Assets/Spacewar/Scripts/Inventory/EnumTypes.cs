using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumTypes : MonoBehaviour
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
}

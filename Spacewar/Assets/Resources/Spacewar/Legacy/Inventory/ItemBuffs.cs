using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBuffs : MonoBehaviour{
    //private Attributes _attribute;
    private int _value;
    private int _min;
    private int _max;
    public ItemBuffs(int min, int max){
        _min = min;
        _max = max;
        GenerateValue();
    }
    public void GenerateValue(){  //랜덤값
        _value = UnityEngine.Random.Range(_min, _max);
    }
}

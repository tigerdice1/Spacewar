using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPSystem : MonoBehaviour{

    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private Gradient _gradient;
    [SerializeField]
    private Image _fill;

    public void SetMaxHP(float _health){
        _slider.maxValue = _health;
        _slider.value = _health;
        _fill.color = _gradient.Evaluate(1f);
    }

    public void SetHP(float _health){
        _slider.value = _health; 
        _fill.color = _gradient.Evaluate(_slider.normalizedValue);
    }
}

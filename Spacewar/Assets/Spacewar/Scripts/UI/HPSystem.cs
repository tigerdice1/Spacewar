using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider _slider;
    public Gradient _gradient;
    public Image _fill;

    public void SetMaxHealth(float _health){
        _slider.maxValue = _health;
        _slider.value = _health;
        _fill.color = _gradient.Evaluate(1f);
    }

    public void SetHealth(float _health){
        _slider.value = _health; 

        _fill.color = _gradient.Evaluate(_slider.normalizedValue);
    }


}

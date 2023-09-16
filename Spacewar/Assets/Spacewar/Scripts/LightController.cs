using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField]
    private Light _lightComponent;

    [SerializeField]
    private bool _isLightOn;

    void SetLightState(bool isOn)
    {
        if (_lightComponent != null)
        {
            _lightComponent.enabled = isOn;

            // 상태에 따라 메시지 출력
            if (isOn)
            {
                Debug.Log("조명이 켜졌습니다.");
            }
            else
            {
                Debug.Log("조명이 꺼졌습니다.");
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SetLightState(_isLightOn);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isLightOn = !_isLightOn;
            SetLightState(_isLightOn);
        }
    }
}

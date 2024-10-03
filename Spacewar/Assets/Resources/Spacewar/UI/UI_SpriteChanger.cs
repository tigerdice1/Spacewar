using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_SpriteChanger : MonoBehaviour
{
    public Toggle toggle; // Toggle 컴포넌트
    public Image targetImage; // 스프라이트가 적용될 Image 컴포넌트
    public Sprite onSprite; // Toggle이 켜졌을 때 사용할 스프라이트
    public Sprite offSprite; // Toggle이 꺼졌을 때 사용할 스프라이트

    void Start(){
        toggle = GetComponent<Toggle>();
        targetImage = GetComponent<Image>();
        // Toggle 상태에 따라 스프라이트 변경
        toggle.onValueChanged.AddListener(delegate { ToggleValueChanged(toggle); });

        // 시작할 때도 상태에 맞는 스프라이트 설정
        UpdateSprite(toggle.isOn);
    }

    // Toggle 값이 변경될 때 호출되는 함수
    void ToggleValueChanged(Toggle change){
        UpdateSprite(change.isOn);
    }

    // isOn 값에 따라 스프라이트를 변경하는 함수
    void UpdateSprite(bool isOn){
        if (isOn){
            targetImage.sprite = onSprite;
        }
        else{
            targetImage.sprite = offSprite;
        }
    }
}

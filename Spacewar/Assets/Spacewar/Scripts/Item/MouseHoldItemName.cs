using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MouseHoldItemName : MonoBehaviour
{
    public TMP_Text itemNameText; // UI Text 요소

    private bool bCheck;
    void Update(){
        if (bCheck)
        {
            // 오브젝트의 로컬 좌표계를 기준으로 텍스트 위치를 업데이트
            Vector3 localOffset = new Vector3(0f, 0.5f, 0f); // 오브젝트 아래로 텍스트를 배치하기 위한 오프셋
            itemNameText.transform.position = transform.position + localOffset;
            ShowText();
        }
        
    }

    void OnMouseEnter(){
        // 마우스 커서가 오브젝트 위에 올라갔을 때
        itemNameText.text = gameObject.name; // 오브젝트의 이름을 UI 텍스트에 표시
        bCheck = true;
    }

    void OnMouseExit(){
        // 마우스 커서가 오브젝트를 벗어났을 때
        HideText(); // UI 텍스트 비활성화
        bCheck = false;
    }

    void ShowText(){
        itemNameText.gameObject.SetActive(true); // TextMeshPro를 활성화하여 표시
    }

    void HideText(){
        itemNameText.gameObject.SetActive(false); // TextMeshPro를 비활성화하여 숨김
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseHoldItemName : MonoBehaviour
{
    public Text itemNameText; // UI Text 요소

    // Update is called once per frame
    void Update()
    {
        // 마우스 커서의 위치를 가져옴
        //Vector3 mousePosition = Input.mousePosition;
        // UI 좌표로 변환
        //RectTransformUtility.ScreenPointToWorldPointInRectangle(itemNameText.rectTransform.parent as RectTransform, mousePosition, Camera.main, out Vector3 worldPosition);
        // UI 텍스트 위치 설정
        //itemNameText.transform.position = worldPosition;
    }

    void OnMouseEnter()
    {
        // 마우스 커서가 오브젝트 위에 올라갔을 때
        itemNameText.text = gameObject.name; // 오브젝트의 이름을 UI 텍스트에 표시
        itemNameText.gameObject.SetActive(true); // UI 텍스트 활성화
    }

    void OnMouseExit()
    {
        // 마우스 커서가 오브젝트를 벗어났을 때
        itemNameText.gameObject.SetActive(false); // UI 텍스트 비활성화
    }
}

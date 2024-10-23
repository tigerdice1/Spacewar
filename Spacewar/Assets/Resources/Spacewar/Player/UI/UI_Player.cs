using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class UI_Player : MonoBehaviour
{
    public PlayerController OwnController;
    public Transform InventoryPicker;
    public List<UI_InventorySlot> InventorySlotList = new List<UI_InventorySlot>();
    public Transform PlayerUI;
    public GameObject clickedObject;
    
    public PlayerBase _ownPlayer;
    public Transform _inventory;


    public void GetClickedUIElement(){
        GraphicRaycaster graphicRaycaster = PlayerUI.GetComponent<GraphicRaycaster>();
        EventSystem eventSystem = EventSystem.current;
        // PointerEventData 생성 및 마우스 포인터 위치 설정
        PointerEventData pointerData = new PointerEventData(eventSystem);
        pointerData.position = Input.mousePosition;
        // Raycast 결과를 저장할 리스트 생성
        List<RaycastResult> raycastResults = new List<RaycastResult>();

        // GraphicRaycaster를 통해 Raycast 수행
        graphicRaycaster.Raycast(pointerData, raycastResults);

        // Raycast 결과 처리
        foreach (RaycastResult result in raycastResults){
            clickedObject = result.gameObject;
            Transform slotPosition = result.gameObject.transform;
            InventoryPicker.transform.position = slotPosition.position;
        }
    }

    private void UpdateInventoryList(){
        if(_ownPlayer.Inventory.Count != 0){
            for(int i = 0; i < _ownPlayer.Inventory.Count; i++){
                InventorySlotList[i].ItemData = _ownPlayer.Inventory[i];
            }
        }
    }
    // Start is called before the first frame update
    void Start(){
        _ownPlayer = OwnController.DefaultControlObject.GetComponent<PlayerBase>();
    }

    void Awake(){
        PlayerUI = transform.Find("PlayerUI");
        _inventory = PlayerUI.transform.Find("Inventory");
        InventoryPicker = PlayerUI.transform.Find("InventoryPicker");
        int childCount = _inventory.childCount;

        for (int i = 0; i < childCount; i++){
            Transform child = _inventory.GetChild(i);
            InventorySlotList.Add(child.GetComponent<UI_InventorySlot>());
        }
    }
    // Update is called once per frame
    void Update(){
        UpdateInventoryList();
    }
}

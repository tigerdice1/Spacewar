using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Player : MonoBehaviour
{
    public PlayerController OwnController;
    private PlayerBase OwnPlayer;
    private Transform PlayerUI;
    private Transform Inventory;
    private List<UI_InventorySlot> InventorySlotList = new List<UI_InventorySlot>();

    // Start is called before the first frame update
    void Start()
    {
        OwnPlayer = OwnController.DefaultControlObject.GetComponent<PlayerBase>();
        PlayerUI = transform.Find("PlayerUI");
        Inventory = PlayerUI.transform.Find("Inventory");
        int childCount = Inventory.childCount;

        for (int i = 0; i < childCount; i++){
            Transform child = Inventory.GetChild(i);
            InventorySlotList.Add(child.GetComponent<UI_InventorySlot>());
        }
    }

    // Update is called once per frame
    void Update(){
        if(OwnPlayer.Inventory.Count != 0){
            for(int i = 0; i < OwnPlayer.Inventory.Count; i++){
                InventorySlotList[i].itemData = OwnPlayer.Inventory[i];
            }
        }
    }

}

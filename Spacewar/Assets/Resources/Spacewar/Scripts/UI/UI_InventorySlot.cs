using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InventorySlot : MonoBehaviour
{
    public CustomTypes.ItemData itemData;

    private Image image;


    public void ClearItemData(){
        itemData.ItemName = "";
        itemData.ItemType = 0;
        itemData.ThumbnailSprite = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update(){
        if(image.sprite != itemData.ThumbnailSprite){
            image.sprite = itemData.ThumbnailSprite;
        }
    }
}

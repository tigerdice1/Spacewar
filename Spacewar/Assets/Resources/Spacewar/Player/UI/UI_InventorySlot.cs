using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InventorySlot : MonoBehaviour
{
    public CustomTypes.ItemData ItemData;

    private Image image;

    private void UpdateImageSprite(){
        if(image.sprite != ItemData.ThumbnailSprite){
            image.sprite = ItemData.ThumbnailSprite;
        }
    }
    // Start is called before the first frame update
    void Start(){
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update(){
        UpdateImageSprite();
    }
}

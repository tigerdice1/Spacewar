using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpriteChanger_UI : MonoBehaviour
{
    [SerializeField]
    protected Sprite _oldSprite;
    [SerializeField]
    protected Sprite _newSprite;

    public void ChangeImage(){
        Image img = this.GetComponent<Image>();
        img.sprite = img.sprite == _oldSprite ? _newSprite : img.sprite == _newSprite ? _oldSprite : img.sprite;

        /*
        if(img.sprite == _oldSprite){
            img.sprite = _newSprite;
        }
        else if(img.sprite == _newSprite){
            img.sprite = _oldSprite;
        }
        */
    }
}

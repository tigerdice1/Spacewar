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
        if(gameObject.GetComponent<Image>().sprite == _oldSprite){
            gameObject.GetComponent<Image>().sprite = _newSprite;
        }
        else if(gameObject.GetComponent<Image>().sprite == _newSprite){
            gameObject.GetComponent<Image>().sprite = _oldSprite;
        }
    }
}

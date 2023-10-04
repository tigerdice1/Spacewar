using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpriteChanger_UI : MonoBehaviour
{
    [SerializeField]
    protected Sprite oldSprite;
    [SerializeField]
    protected Sprite newSprite;

    public void ChangeImage(){
        if(gameObject.GetComponent<Image>().sprite == oldSprite){
            gameObject.GetComponent<Image>().sprite = newSprite;
        }
        else if(gameObject.GetComponent<Image>().sprite == newSprite){
            gameObject.GetComponent<Image>().sprite = oldSprite;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

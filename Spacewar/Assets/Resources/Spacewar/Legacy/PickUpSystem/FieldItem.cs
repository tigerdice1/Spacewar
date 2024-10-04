using PlayerInven.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItem : MonoBehaviour
{
    [SerializeField]
    private Item _invenItem;

    public Item InvenItem{
        get => _invenItem;
        set => _invenItem = value;
    }

    [SerializeField]
    private int _quantity;

    public int Quantity{
        get =>_quantity;
        set =>_quantity= value;
    }

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private float _duration = 0.3f;
    
    private void Start(){
        //GetComponent<SpriteRenderer>().sprite = _invenItem.ItemImage;
    }

    public void DestroyItem(){
        GetComponent<Collider>().enabled = false;
        StartCoroutine(AnimateItemPickup());
    }

    private IEnumerator AnimateItemPickup(){
        _audioSource.Play();
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;
        float currentTime =0;
        while(currentTime < _duration){
            currentTime += Time.deltaTime;
            transform.localScale =Vector3.Lerp(startScale,endScale,currentTime /_duration);
            yield return null;
        }
        transform.localScale = endScale;
        Destroy(gameObject);
    }

}

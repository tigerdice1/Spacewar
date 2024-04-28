using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidArea : MonoBehaviour
{
    public int _maxX, _maxZ;
    // Start is called before the first frame update
    void Awake() {
        gameObject.transform.position = new Vector3(UnityEngine.Random.Range(-1f * _maxX, _maxX),
        0f,
        UnityEngine.Random.Range(-1f * _maxZ, _maxZ));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

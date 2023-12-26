using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainShip : MonoBehaviour{

    [SerializeField]
    [Tooltip("")]
    private PlayerController _playerController;

    [SerializeField]
    [Tooltip("")]
    private float _speed;


    public float Speed{
        get{ return _speed;}
        set{ _speed = value;}
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

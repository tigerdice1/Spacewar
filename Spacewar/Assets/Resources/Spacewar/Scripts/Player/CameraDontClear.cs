using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDontClear : MonoBehaviour
{
    [SerializeField]
    private Camera _cam;

    void Awake()
    {
        if (_cam == null){
            _cam = GetComponent<Camera>();
        }
        Initialize();
    }
    public void Initialize()
    {
        _cam.clearFlags = CameraClearFlags.Color;
    }
    void OnPostRender()
    {
        _cam.clearFlags = CameraClearFlags.Nothing;
    }
}

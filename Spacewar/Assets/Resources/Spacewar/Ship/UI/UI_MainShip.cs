using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_MainShip : UI_Base
{
    public MainShip Ship;
    public TextMeshProUGUI ThrottleText;
    public TextMeshProUGUI VelocityText;
    public float Throttle;
    private Rigidbody Rigidbody;

    void UpdateThrottleText(){
        if (ThrottleText != null){
            Throttle = Ship.Throttle;
            
            ThrottleText.text = "Throttle: " + Throttle;
        }
    }
    void UpdateVelocityText(){
        if (VelocityText != null){
            Throttle = Ship.Throttle;
            
            VelocityText.text = "Velocity: " + Rigidbody.velocity.magnitude;
        }
    }
    // Start is called before the first frame update
    protected override void Start(){
        base.Start();
        if(Ship != null){
            Rigidbody = Ship.gameObject.GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update(){
        UpdateThrottleText();
        UpdateVelocityText();
    }
}

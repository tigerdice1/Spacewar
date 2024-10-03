using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MainShip : ShipBase{

    
    
    // Start is called before the first frame update
    void Start(){
        base.Initalize();
    }

    // Update is called once per frame
    void Update(){
        ChcekLoadedMissileRooms();
        
    }
    void FixedUpdate(){
        CalcAngularSpeed();
        //ReverseThruster();
    }
}

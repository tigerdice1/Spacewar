using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrench : PickableItem
{
    public override void UseItem(Transform ownPlayer, Transform targetTransform){
        var powerGenerator = targetTransform.gameObject.GetComponent<PowerGenerator>();
        var player = ownPlayer.gameObject.GetComponent<PlayerBase>();
        if(player != null){
            if(powerGenerator!= null){
                powerGenerator.FixObject(player.FixSkill);
            }
        }
        
    }
    // Start is called before the first frame update
    protected override void Start(){
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

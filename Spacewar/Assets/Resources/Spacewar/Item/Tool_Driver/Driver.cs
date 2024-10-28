using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Driver : PickableItem
{
    public override void UseItem(Transform ownPlayer, Transform targetTransform){
        var controlPanel = targetTransform.gameObject.GetComponent<ControlPanel>();
        var junction = targetTransform.gameObject.GetComponent<Junction>();
        var player = ownPlayer.gameObject.GetComponent<PlayerBase>();
        if(player != null){
            if(controlPanel!= null){
                controlPanel.FixObject(player.FixSkill);
            }
            else if(junction!= null){
                junction.FixObject(player.FixSkill);
            }
        }
    }
}

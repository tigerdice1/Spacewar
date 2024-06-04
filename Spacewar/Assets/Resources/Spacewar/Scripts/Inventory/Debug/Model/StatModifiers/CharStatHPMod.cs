using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharStatHPMod : CharacterStatModifier
{
    public override void AffectCharacter(GameObject character,float val){
        Human health =  character.GetComponent<Human>();
        if(health != null)
        {
            health.AddHealth((int)val);
        }
    }
}

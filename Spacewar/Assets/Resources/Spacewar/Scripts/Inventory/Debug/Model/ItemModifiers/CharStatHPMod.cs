using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharStatHPMod : CharacterStatModifier
{
    public override void AffectCharacter(GameObject character,float val){
        HP health =  character.GetComponent<HP>();
        if(health != null)
        {
            health.AddHealth((int)val);
        }
    }
}

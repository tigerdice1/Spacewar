using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public void Damage(GameObject attacker, GameObject victim){
        if(attacker.GetComponent<Projectile>() && victim.GetComponent<Asteroid>()){
            victim.GetComponent<Asteroid>().AsteroidHP -= attacker.GetComponent<Projectile>().ProjectileDamage;
        }
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

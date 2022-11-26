using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public float hitpoint = 10;
    public float maxHitpoint = 10;

    protected virtual void ReciveDamage(Damage dmg)
    {
            hitpoint -= dmg.damageAmount;          
            if (hitpoint <= 0)
            {
                Death();
            }
    }
    protected virtual void Death()
    {

    }
}
public struct Damage
{
    public float damageAmount;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public int hitpoint = 10;
    public int maxHitpoint = 10;

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
    public int damageAmount;
}

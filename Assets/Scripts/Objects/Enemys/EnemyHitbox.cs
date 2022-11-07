using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable
{
    public int damage = 1;
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            Damage dmg = new Damage
            {
                damageAmount = damage
            };
            coll.SendMessage("ReciveDamage", dmg);
        }
        else
        {
            return;
        }
    }
}

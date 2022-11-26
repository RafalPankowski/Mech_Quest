using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Bullet : Gun
{
    public Bullet bulletPrefarb;
    protected override void Shoot()
    {
        base.Shoot();
        Bullet bullet = Instantiate(this.bulletPrefarb, this.transform.position, this.transform.rotation);
        bullet.damage = (int)damagePoint[weaponLevel];
        bullet.Project(this.transform.up);
    }
}

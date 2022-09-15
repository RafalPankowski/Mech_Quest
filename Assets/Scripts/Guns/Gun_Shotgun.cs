using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Shotgun : Gun
{
    public Ball ballPrefarb;
    public int[] bulletAmount = new int[] { 1, 2, 3 };
    protected override void Shoot()
    {
        base.Shoot();
        for (int i = 0; i < bulletAmount[weaponLevel]; i++)
        {
            Ball ball = Instantiate(this.ballPrefarb, this.transform.position, this.transform.rotation);
            //ball.transform.Rotate(new Vector3(0, 0, 30f * i));
            ball.Project(this.transform.up + new Vector3(0.1f * i , 0.1f * i));
        }
    }
}

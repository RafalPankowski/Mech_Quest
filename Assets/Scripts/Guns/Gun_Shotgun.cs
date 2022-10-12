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
            Ball ball = Instantiate(ballPrefarb, this.transform.position, transform.rotation);
            ball.weaponLevel = weaponLevel;
            ball.transform.Rotate(new Vector3(0, 0,  ((-10f * i)/2) + (10f * i)  ));
            ball.Project(ball.transform.up);
        }
    }
}

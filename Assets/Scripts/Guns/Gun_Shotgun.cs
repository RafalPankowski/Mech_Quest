using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Shotgun : Gun
{
    public Ball ballPrefarb;
    public int[] bulletAmount = new int[] { 1, 2, 3 };
    private float spread = 20f;

    protected override void Shoot()
    {
        var y = bulletAmount[weaponLevel];
        var x = spread/y;
        base.Shoot();
        for (int i = 0; i < bulletAmount[weaponLevel]; i++)
        {
            Ball ball = Instantiate(ballPrefarb, this.transform.position, transform.rotation);
            ball.weaponLevel = weaponLevel;
            ball.transform.Rotate(new Vector3(0, 0, -x*(2+i-i*y)));
            ball.Project(ball.transform.up);
        }
    }
}

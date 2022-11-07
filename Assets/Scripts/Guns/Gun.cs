using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float[] fireRate =new float[] { 3f };
    public float[] Heat =new float[] { 4.0f };
    protected float lastShoot;
    public int weaponLevel = 0;

    protected virtual void Update()
    {
        Aim(Input.mousePosition);

        if (Input.GetMouseButton(0) && RoundManager.instance.alive == true)
        {
            if (RoundManager.instance.player.curHeat > RoundManager.instance.player.maxHeat - Heat[weaponLevel])
            {
                return;
            }
            else
            { 
                if (Time.time - lastShoot > (fireRate[weaponLevel] / RoundManager.instance.player.FireRate[RoundManager.instance.player.FireRateLevel]))
                {
                    lastShoot = Time.time;
                    Shoot();
                }
            }
        }
    }
    protected virtual void Shoot()
    {
        RoundManager.instance.player.HeatUp(Heat[weaponLevel]);
    }

    public void Aim(Vector2 mouse)
    {

        Vector2 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
        float lookAngle = Mathf.Atan2(offset.x, -offset.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, lookAngle);
    }
}

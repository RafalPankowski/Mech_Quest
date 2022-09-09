using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float[] fireRate =new float[] { 0.5f };
    public float[] Heat =new float[] { 4.0f };
    protected float lastShoot;
    public int weaponLevel = 0;

    protected virtual void Update()
    {
        Aim(Input.mousePosition);

        if (Input.GetMouseButton(0) && GameManager.instance.alive == true)
        {
            if (GameManager.instance.player.curHeat > GameManager.instance.player.maxHeat - Heat[weaponLevel])
            {
                return;
            }
            else
            { 
                if (Time.time - lastShoot > fireRate[weaponLevel])
                {
                    lastShoot = Time.time;
                    Shoot();
                }
            }
        }
    }
    protected virtual void Shoot()
    {
        GameManager.instance.player.HeatUp(Heat[weaponLevel]);
    }

    public void Aim(Vector2 mouse)
    {

        Vector2 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
        float lookAngle = Mathf.Atan2(offset.x, -offset.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, lookAngle);
    }
}

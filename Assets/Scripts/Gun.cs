using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float fireRate = 0.5f;
    public float Heat = 4.0f;
    private float lastShoot;

    private void Update()
    {
        Aim(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            if (GameManager.instance.player.curHeat >= 100)
            {
                return;
            }
            else
            { 
                if (Time.time - lastShoot > fireRate)
                {
                    lastShoot = Time.time;
                    Shoot();
                }
            }
        }
    }
    protected virtual void Shoot()
    {
        
        GameManager.instance.player.HeatUp(Heat);
    }

    public void Aim(Vector2 mouse)
    {

        Vector2 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
        float lookAngle = Mathf.Atan2(offset.x, -offset.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, lookAngle);
    }
}

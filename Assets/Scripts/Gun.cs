using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Bullet bulletPrefarb;
    public float fireRate = 0.5f;
    private float lastShoot;

    private void Update()
    {
        Aim(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            if(Time.time - lastShoot > fireRate)
            {
                lastShoot = Time.time;
                Shoot();
            }
        }
    }
    private void Shoot()
    {
        Bullet bullet = Instantiate(this.bulletPrefarb, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }

    public void Aim(Vector2 mouse)
    {

        Vector2 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
        float lookAngle = Mathf.Atan2(offset.x, -offset.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, lookAngle);
    }
}

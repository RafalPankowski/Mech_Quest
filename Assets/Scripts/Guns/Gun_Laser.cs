using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Laser : Gun
{
    public Laser laserPrefarb;

    private List<Laser> laserList;

    private bool weaponIsOn;

    private void Awake()
    {
        
            laserList = new List<Laser>();
            Laser newline = Instantiate(laserPrefarb);
            laserList.Add(newline);
            newline.firePoint = this.transform;
            weaponIsOn = false;

    }

    protected override void Update()
    {
        Aim(Input.mousePosition);
        if(Input.GetMouseButton(0) && GameManager.instance.alive == true && GameManager.instance.player.curHeat + this.Heat[weaponLevel] < GameManager.instance.player.maxHeat)
        {
            weaponIsOn = true;
            if (Time.time - this.lastShoot > this.fireRate[weaponLevel])
            {
                this.lastShoot = Time.time;
                Shoot();
            }
        }
        else
        {
            weaponIsOn = false;
        }
        ToggleWeapon();
    }

    private void ToggleWeapon()
    {
        if (weaponIsOn == false)
        {
            foreach (var line in laserList)
            {
                //line.gameObject.SetActive(false);
                line.lineRenderer.enabled = false;
                line.boxCollider2D.enabled = false;
            }
        }
        else
        {
            foreach (var line in laserList)
            {
                //line.gameObject.SetActive(true);
                line.lineRenderer.enabled = true;
                line.boxCollider2D.enabled = true;
            }
        }
    }
}

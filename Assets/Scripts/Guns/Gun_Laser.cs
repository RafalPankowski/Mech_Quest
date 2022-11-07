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
        newline.weaponLevel = weaponLevel;
        laserList.Add(newline);
        newline.firePoint = this.transform;
        weaponIsOn = false;

    }

    protected override void Update()
    {
        Aim(Input.mousePosition);
        if(Input.GetMouseButton(0) && RoundManager.instance.alive == true && RoundManager.instance.player.curHeat + this.Heat[weaponLevel] < RoundManager.instance.player.maxHeat)
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
        if (weaponIsOn == true)
        {
            foreach (var line in laserList)
            {
                line.lineRenderer.enabled = true;
                line.boxCollider2D.enabled = true;
            }
        }
        else
        {
            foreach (var line in laserList)
            {
                line.lineRenderer.enabled = false;
                line.boxCollider2D.enabled = false;
            }
        }
    }
}

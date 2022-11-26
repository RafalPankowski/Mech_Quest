using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Collidable
{

    public LineRenderer lineRenderer;
    public BoxCollider2D boxCollider2D;

    public Gun_Laser laserGun;

    [SerializeField]
    private Texture[] textures;

    private int animationStep;

    [SerializeField]
    private float fps = 10f;

    private float fpsCounter;

    public Transform firePoint;


    private void Awake()
    {
        this.lineRenderer = GetComponent<LineRenderer>();
        this.boxCollider2D = GetComponent<BoxCollider2D>();
        this.laserGun = GetComponent<Gun_Laser>();

    }

    public void AssignTarget(Transform startPosition)
    {
        this.lineRenderer.positionCount = 2;
        this.lineRenderer.SetPosition(0, startPosition.transform.position);
        var mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

        this.lineRenderer.SetPosition(1, mousePos);
        this.boxCollider2D.offset = mousePos;

        Vector2 direction = mousePos - (Vector2)startPosition.transform.position;
        RaycastHit2D hit = Physics2D.Raycast((Vector2)startPosition.transform.position, direction.normalized, direction.magnitude, LayerMask.GetMask("Enemy"));   
        if (hit)
        {
            this.lineRenderer.SetPosition(1, hit.point);
            this.boxCollider2D.offset = hit.point;
        }



    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        AssignTarget(this.firePoint);
        fpsCounter += Time.deltaTime;
        if (fpsCounter >= 1f / fps)
        {
            animationStep++;
            if (animationStep == textures.Length)
                animationStep = 0;

            this.lineRenderer.material.SetTexture("_MainTex", textures[animationStep]);

            fpsCounter = 0f;
        }
    }
    protected override void OnCollide(Collider2D coll)
    {
        var playerDmgBouns = RoundManager.instance.player;

        if (coll.tag == "Enemy")
        {
            if (coll.name == "Player")
                return;

            Damage dmg = new Damage
            {
                damageAmount = laserGun.damagePoint[laserGun.weaponLevel] + (int)((laserGun.damagePoint[laserGun.weaponLevel] * playerDmgBouns.damageBonus[playerDmgBouns.damageBonusLevel])/100),
            };
            coll.SendMessage("ReciveDamage", dmg);
        }
    }
}

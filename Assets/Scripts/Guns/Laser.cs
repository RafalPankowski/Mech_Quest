using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Collidable
{
    public int[] damagePoint = new int[] { 1, 2, 3, 4, 5, 6, 7 };
    public int weaponLevel = 0;

    public LineRenderer lineRenderer;
    public BoxCollider2D boxCollider2D;

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
    }

    public void AssignTarget(Transform startPosition)
    {
        this.lineRenderer.positionCount = 2;
        this.lineRenderer.SetPosition(0, startPosition.transform.position);
        var mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

        this.lineRenderer.SetPosition(1, mousePos);
        this.boxCollider2D.offset = mousePos;

        Vector2 direction = mousePos - (Vector2)startPosition.transform.position;
        RaycastHit2D hit = Physics2D.Raycast((Vector2)startPosition.transform.position, direction.normalized, direction.magnitude, LayerMask.GetMask("Asteroid"));   
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
        if (coll.tag == "Asteroid")
        {
            if (coll.name == "Player")
                return;

            Damage dmg = new Damage
            {
                damageAmount = damagePoint[weaponLevel],
            };
            coll.SendMessage("ReciveDamage", dmg);
        }
    }
}

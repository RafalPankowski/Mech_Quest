using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Collidable
{
    private Rigidbody2D _rigidbody;
    public float speed = 500.0f;
    public float maxLifetime = 1.0f;

    public int[] damagePoint = new int[] {1,2,3,4,5,6,7};
    public int weaponLevel = 4;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        _rigidbody.AddForce(-direction * this.speed);

        Destroy(this.gameObject, this.maxLifetime);
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
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Collidable
{
    private Rigidbody2D _rigidbody;
    public float speed = 500.0f;
    public float maxLifetime = 1.0f;
    public int damage;

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
        var playerDmgBouns = RoundManager.instance.player;
        if (coll.tag == "Enemy")
        {
            if (coll.name == "Player")
                return;
            Damage dmg = new Damage
            {
                damageAmount = damage + (int)((damage * playerDmgBouns.damageBonus[playerDmgBouns.damageBonusLevel])/100),
            };
            coll.SendMessage("ReciveDamage", dmg);
            Destroy(this.gameObject);
        }
    }
}

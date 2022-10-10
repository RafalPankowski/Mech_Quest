using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insect : Mover
{
    public Sprite[] sprites;
    private SpriteRenderer _spriteRenderer;

    protected override void Awake()
    {
        base.Awake();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();
        _playerTarget = RoundManager.instance.player.transform;
    }

    protected override void Death()
    {
        if ((this.size * 0.6) >= this.minSize)
        {
            CreateSplit();
            CreateSplit();
        }
        RoundManager.instance.explosion.transform.position = transform.position;
        RoundManager.instance.explosion.Play();
        if (size > 1.3f)
        {
            RoundManager.instance.GrantXp(3);
        }
        else if (size < 0.8f)
        {
            RoundManager.instance.GrantXp(1);
        }
        else
        {
            RoundManager.instance.GrantXp(2);
        }
        RoundManager.instance.hub.SetLevel();
        Destroy(this.gameObject);
    }

    private void CreateSplit()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.8f;

        Mover half = Instantiate(this, position, this.transform.rotation);
        half.size = this.size * 0.7f;
    }
}

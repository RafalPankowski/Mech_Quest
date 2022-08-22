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
        _playerTarget = GameManager.instance.player.transform;
    }

    protected override void Death()
    {
        if ((this.size * 0.6) >= this.minSize)
        {
            CreateSplit();
            CreateSplit();
        }
        GameManager.instance.EnemyDestroyed(this);
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

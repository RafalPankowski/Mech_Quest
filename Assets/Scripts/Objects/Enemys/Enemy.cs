using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private Transform _playerTarget;
    private Vector2 moveDirection;
    private Animator _animator;


    public float size = 1.0f;
    public float minSize = 0.85f;
    public float maxSize = 1.5f;
    public float speed = 1.0f;
    public float maxLifetime = 30.0f;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        _playerTarget = RoundManager.instance.player.transform;

        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        this.transform.localScale = Vector3.one * this.size;
    }

    private void Update()
    {
        if (_playerTarget && RoundManager.instance.alive == true)
        {
            SetTrajectory(_playerTarget);
        }
    }

    private void FixedUpdate()
    {
        if (_playerTarget && RoundManager.instance.alive == true)
        {
            _rigidbody.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
            _animator.SetBool("isRuning", true);
        }
        else
        {
            _animator.SetBool("isRuning", false);
        }
    }

    public void SetTrajectory(Transform target)
    {
        Vector2 direction = (target.position - this.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90.0f;
        _rigidbody.rotation = angle;
        moveDirection = direction;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            if((this.size * 0.6) >= this.minSize)
            {
                CreateSplit();
                CreateSplit();
            }        
             //FindObjectOfType<GameManager>().EnemyDestroyed(this);
             Destroy(this.gameObject);                   
        }
    }

    private void CreateSplit()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Enemy half = Instantiate(this, position, this.transform.rotation);
        half.size = this.size * 0.5f;
    }

}

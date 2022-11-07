using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : Fighter
{
    protected Rigidbody2D _rigidbody;
    protected Transform _playerTarget;
    protected Vector2 moveDirection;
    protected Animator _animator;

    public float speed = 40.0f;
    public float maxLifetime = 30.0f;

    public float size = 1.0f;
    public float minSize = 0.85f;
    public float maxSize = 1.2f;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    protected virtual void Start()
    {
        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        this.transform.localScale = Vector3.one * this.size;
    }

    protected virtual void Update()
    {
        if (_playerTarget && RoundManager.instance.alive == true)
        {
            SetTrajectory(_playerTarget);
        }
    }

    protected virtual void FixedUpdate()
    {
        if (_playerTarget && RoundManager.instance.alive == true)
        {
            _rigidbody.velocity = new Vector2(moveDirection.x, moveDirection.y) * Random.Range(speed/2, speed+speed/3) * Time.deltaTime;
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
}

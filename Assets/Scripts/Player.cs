using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Fighter
{
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    public Bullet bulletPrefarb;
    private bool _thrusting, _pull;
    private float _turnDirection;
    public float thrustSpeed = 1.0f, turnSpeed = 1.0f;

    protected float immuneTime = 0.5f;
    protected float lastImmune;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveController();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void moveController()
    {
        _thrusting = Input.GetKey(KeyCode.W);
        _pull = Input.GetKey(KeyCode.S);

        if (Input.GetKey(KeyCode.A))
        {
            _turnDirection = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _turnDirection = -1.0f;
        }
        else
        {
            _turnDirection = 0.0f;
        }
    }
    private void Movement()
    {
        if (_thrusting)
        {
            _rigidbody.AddForce(-this.transform.up * this.thrustSpeed);
            _animator.SetBool("isRuning", true);
        }
        else if (_pull)
        {
            _rigidbody.AddForce(this.transform.up * this.thrustSpeed);
            _animator.SetBool("isRuning", true);
        }
        else { _animator.SetBool("isRuning", false); }

        if (_turnDirection != 0.0f)
        {
            _rigidbody.AddTorque(_turnDirection * this.turnSpeed);
            _animator.SetBool("isRuning", true);
        }
    }
    protected override void ReciveDamage(Damage dmg)
    {
        if (Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            base.ReciveDamage(dmg);
        }
    }
    protected override void Death()
    {
        base.Death();
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = 0.0f;

        this.gameObject.SetActive(false);

        FindObjectOfType<GameManager>().PlayerDied();

    }

    public void OnLevelUp()
    { 
        this.maxHitpoint++;
        this.hitpoint = this.maxHitpoint;
    }
    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
            OnLevelUp();
    }
}

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
    public float thrustSpeed = 50.0f, turnSpeed = 10.0f;
    public GameObject[] Gun_Slots;

    protected float immuneTime = 0.5f;
    protected float lastImmune;

    public float curHeat = 0;
    public float maxHeat = 100;
    public int coolLevel;
    public float[] coolRate = {3,5,7,10,13,16,20,24};

    public int FireRateLevel;
    public float[] FireRate = { 1,1.15f,1.3f,1.55f,1.7f,1.85f,2,2.3f };

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
        if (RoundManager.instance.alive == true)
        {
            Movement();
            CoolDown(Time.deltaTime);
        }
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
            _rigidbody.AddForce(-this.transform.up * this.thrustSpeed * Time.deltaTime);
            _animator.SetBool("isRuning", true);
        }
        else if (_pull)
        {
            _rigidbody.AddForce(this.transform.up * this.thrustSpeed * Time.deltaTime);
            _animator.SetBool("isRuning", true);
        }
        else { _animator.SetBool("isRuning", false); }

        if (_turnDirection != 0.0f)
        {
            _rigidbody.AddTorque(_turnDirection * this.turnSpeed * Time.deltaTime);
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

        FindObjectOfType<RoundManager>().PlayerDied();

    }

    public void OnLevelUp()
    { 
        this.maxHitpoint = this.maxHitpoint + 2;
        this.hitpoint = this.maxHitpoint;
    }
    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
            OnLevelUp();
    }

    public void HeatUp(float heat)
    {
        this.curHeat += heat;
    }

    private void CoolDown(float time)
    {
        if (this.curHeat > 0)
        {
            this.curHeat -= time * coolRate[coolLevel];
        }
        else
        {
            this.curHeat = 0;
            return;
        }
    }
}

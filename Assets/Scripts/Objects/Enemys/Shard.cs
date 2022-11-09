using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shard : Collidable
{
    public float ySpeed, xSpeed, triggerLenght;
    private Transform playerTransform;
    private Vector3 moveDelta, originalSize, startingPosition;


    protected override void Start()
    {
        base.Start();
        startingPosition = this.transform.position;
        originalSize = transform.localScale;
        this.transform.parent = RoundManager.instance.shardContainer.transform;
    }

    private void Update()
    {
        playerTransform = RoundManager.instance.player.transform;
    }

    private void UpdateMotor(Vector3 input)
    {
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);
        if (moveDelta.x > 0)
            transform.localScale = originalSize;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(originalSize.x * -1, originalSize.y * 1, originalSize.z);

        transform.Translate(moveDelta.x * Time.deltaTime, moveDelta.y * Time.deltaTime,0);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if(Vector3.Distance(this.playerTransform.position,startingPosition) < triggerLenght)
        {
            UpdateMotor((playerTransform.position - this.transform.position).normalized);
        }
        else
        {
            startingPosition = this.transform.position;
        }
    }
    protected override void OnCollide(Collider2D coll)
    {
        {
            if (coll.tag == "Player")
            {
                Destroy(this.gameObject);
                GameManager.instance.mana++;
                RoundManager.instance.levelManager.GrantXp(1);
            }
        }
    }
}

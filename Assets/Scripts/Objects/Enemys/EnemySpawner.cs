using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefarb;

    public float spawnRate = 5.0f;
    public int spawnAmount = 8;
    public int spawnMaxAmount = 20;
    public float spawnDistance = 20.0f;
    public float trajectoryVariance = 15.0f;
    public float spawnIncreaseTime = 180;

    private float timer;
    private float sTimer;
    public int spawnCounter;

    public int enemyBonus = 1;

    private void Start()
    {
         timer = spawnRate;
        sTimer = spawnIncreaseTime;
    }

     void Update()
    {
        timer -= Time.deltaTime;
        sTimer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = spawnRate;
            Spawn(spawnAmount);
            spawnCounter++;
        }

        if (sTimer < 0)
        {
            spawnAmount++;
            sTimer = spawnIncreaseTime;
        }

        if (spawnCounter * spawnRate >= 60)
        {
            enemyBonus += 10;
            spawnMaxAmount += 5;
            spawnCounter = 0;
        }
    }

    private void Spawn(int Amount)
    {
        if (spawnAmount + spawnMaxAmount >= this.gameObject.transform.childCount)
        {
            for (int i = 0; i < Amount; i++)
            {
                Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
                Vector2 spawnPoint = Camera.main.transform.position + spawnDirection;

                float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
                Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

                GameObject enemy = Instantiate(this.enemyPrefarb[Random.Range(0,enemyPrefarb.Length)], spawnPoint, rotation);
                var mob = enemy.GetComponent<Mover>();
                mob.size = Random.Range(mob.minSize, mob.maxSize);
                mob.transform.parent = this.transform;
                mob.maxHitpoint += (int)(mob.maxHitpoint*enemyBonus)/100;
                mob.hitpoint = mob.maxHitpoint;
            }
        }
        else 
        {
            return;
        }
    }
}
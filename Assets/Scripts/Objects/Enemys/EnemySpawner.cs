using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Mover enemyPrefarb;

    public float spawnRate = 5.0f;
    public int spawnAmount = 8;
    public float spawnDistance = 20.0f;
    public float trajectoryVariance = 15.0f;
    public float spawnIncreaseTime = 180;

    private float timer;
    private float sTimer;
    public int spawnCounter;

    public int enemyBonus;

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
            spawnCounter = 0;
        }
    }

    private void Spawn(int Amount)
    {
        if (spawnAmount * RoundManager.instance.levelManager.lvl * 3 >= this.gameObject.transform.childCount)
        {
            for (int i = 0; i < Amount; i++)
            {
                Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
                Vector2 spawnPoint = Camera.main.transform.position + spawnDirection;

                float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
                Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

                Mover enemy = Instantiate(this.enemyPrefarb, spawnPoint, rotation);
                enemy.size = Random.Range(enemy.minSize, enemy.maxSize);
                enemy.transform.parent = this.transform;
                enemy.maxHitpoint += enemyBonus;
                enemy.hitpoint = enemy.maxHitpoint;
            }
        }
        else 
        {
            return;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Mover[] enemyPrefarb;

    public float spawnRate = 5.0f;
    public int spawnAmount = 8;
    public float spawnDistance = 20.0f;
    public float trajectoryVariance = 15.0f;
    private float timer;
    public int loopCounter;

    private void Start()
    {
         timer = spawnRate;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            timer = spawnRate;
            Spawn(spawnAmount);
            loopCounter++;
        }

        if (loopCounter*(int)spawnRate >= 60)
        {
            spawnAmount++;
            loopCounter = 0;
        }
        else if (loopCounter * (int)spawnRate >= 300)
        {
            Spawn(spawnAmount * 2);
        }

    }

    private void Spawn(int Amount)
    {
        for (int i = 0; i < Amount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            Vector2 spawnPoint = Camera.main.transform.position + spawnDirection;

            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            Mover enemy = Instantiate(this.enemyPrefarb[RoundManager.instance.levelManager.lvl], spawnPoint, rotation);
            enemy.size = Random.Range(enemy.minSize, enemy.maxSize);
            enemy.transform.parent = this.transform;
        }
    }
}
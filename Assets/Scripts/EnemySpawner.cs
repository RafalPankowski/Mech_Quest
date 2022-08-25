using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Mover enemyPrefarb;

    public float spawnRate = 7.0f;
    public int spawnAmount = 8;
    public float spawnDistance = 20.0f;
    public float trajectoryVariance = 15.0f;
    private void Start()
    {
        InvokeRepeating(nameof(Spawn), this.spawnRate , this.spawnRate);
    }

    private void Spawn()
    {
        for (int i = 0; i < this.spawnAmount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            Vector2 spawnPoint = Camera.main.transform.position + spawnDirection;

            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            Mover enemy = Instantiate(this.enemyPrefarb, spawnPoint, rotation);
            enemy.size = Random.Range(enemy.minSize, enemy.maxSize);
            enemy.transform.parent = this.transform;
        }
    }
}
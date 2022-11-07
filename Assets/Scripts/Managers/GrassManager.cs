using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassManager : MonoBehaviour
{
    public GameObject grass;

    public float spawnRate = 3.0f;
    public int spawnAmount = 4;
    public float spawnDistance = 15;
    public float trajectoryVariance = 15.0f;
    private void Start()
    {
        StartSpawn();
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    private void Update()
    {
        spawnDistance = Random.Range(11, 14);
    }

    private void Spawn()
    {
        for (int i = 0; i < this.spawnAmount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            Vector2 spawnPoint = Camera.main.transform.position + spawnDirection;

            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            GameObject enemy = Instantiate(this.grass, spawnPoint, rotation);
            enemy.transform.parent = GameObject.Find("GrassManager").transform;
            Destroy(enemy, 30);
        }
    }

    private void StartSpawn()
    {
        for (int i = 0; i < 14; i++)
        {

            Vector3 spawnDirection = Random.insideUnitCircle * Random.Range(5,12);
            Vector2 spawnPoint = Camera.main.transform.position + spawnDirection;

            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

                GameObject enemy = Instantiate(this.grass, spawnPoint, rotation);
            enemy.transform.parent = GameObject.Find("GrassManager").transform;

        }
    }
}

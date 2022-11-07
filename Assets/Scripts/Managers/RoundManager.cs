using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance;

    public Player player;
    public ParticleSystem explosion;
    public HubManager hub;
    public LevelManager levelManager;
    public Timer timer;
    public PlayerSpawner pSpawner;
    public EnemySpawner eSpawner;

    public int lives = 3;
    public float respawnTime = 3.0f;
    public float respawnImmuneTime = 3.0f;
    public bool alive = true;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void PlayerDied()
    {
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();

        this.lives--;
        this.alive = false;

        if(this.lives <= 0)
        {
            GameOver();
        } else
        {
            Invoke(nameof(Respawn), this.respawnTime);
        }
    }

    private void Respawn()
    {
        this.player.hitpoint = player.maxHitpoint;
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
        this.player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollisions), respawnImmuneTime);
        this.alive = true;
    }

    private void TurnOnCollisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver()
    {
        this.lives = 3;
        Invoke(nameof(Respawn), this.respawnTime);
        //hub.SetLevel();
    }
}

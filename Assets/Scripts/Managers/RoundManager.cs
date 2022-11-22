using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance;

    public Player player;
    public ParticleSystem explosion;
    public HudManager hud;
    public LevelManager levelManager;
    public Timer timer;
    public PlayerSpawner pSpawner;
    public EnemySpawner eSpawner;
    public Animator _deathAnimator, _menuAnimator;

    public GameObject shardContainer;

    public int lives = 1;
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
        levelManager.ImplementGun(levelManager.Guns[Random.Range(0, levelManager.Guns.Length)]);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && _menuAnimator.GetBool("Escape_used") == false)
        {
            _menuAnimator.SetBool("Escape_used", true);
            _menuAnimator.SetTrigger("Initiate");
            GameManager.instance.PauseGame();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && _menuAnimator.GetBool("Escape_used") == true)
        {
            _menuAnimator.SetTrigger("Back");
            _menuAnimator.SetBool("Escape_used", false);
            GameManager.instance.ResumeGame();
        }
    }

    public void PlayerDied()
    {
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();

        this.lives--;
        this.alive = false;

        if(this.lives <= 0)
        {
            StartCoroutine(Wait());
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
        GameManager.instance.PauseGame();
        _deathAnimator.SetTrigger("Died");
    }

    public void BackToMenu()
    {
        GameManager.instance.mech = null;
        GameManager.instance.ResumeGame();
        SceneManager.LoadScene("StartMenu");
    }

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(3);
        GameOver();
    }
}

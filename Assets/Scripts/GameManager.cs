using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Player player;
    public ParticleSystem explosion;
    public HubManager hub;
    public LevelupManager levelupManager;

    public int lives = 3;
    public float respawnTime = 3.0f;
    public float respawnImmuneTime = 3.0f;
    public bool alive = true;

    public int exp = 0;
    public int lvl = 1;
    public List<int> xpTable;

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
        hub.SetLevel();
    }

    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;
        while (exp >= add)
        {
            add += xpTable[r];
            r++;
            if (r == xpTable.Count)
                return r;
        }
        return r;
    }
    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;
        while (r < level)
        {
            xp += xpTable[r];
            r++;
        }
        return xp;
    }
    public void GrantXp(int xp)
    {
        int currLevel = GetCurrentLevel();
        exp += xp;
        if (currLevel < GetCurrentLevel())
            OnLevelUp();
    }
    public void OnLevelUp()
    {
        Debug.Log("Level Up!");
        player.OnLevelUp();
        levelupManager.Upgrade();
    }

    public void PauseGame()
    {

    }
}

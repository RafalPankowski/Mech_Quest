using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Menu menu;
    public CursorManager cursor;

    public GameObject mech;

    public int mana;

    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(cursor.gameObject);
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public void PauseGame()
    {
        Time.timeScale = (float)GameState.Paused;

    }

    public void ResumeGame()
    {
        Time.timeScale = (float)GameState.Gameplay;
    }



}

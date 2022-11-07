using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Menu menu;

    public GameObject mech;

    public int mana;

    private void Awake()
    {
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

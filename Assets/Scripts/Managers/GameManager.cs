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

    public GameState state = GameState.Gameplay;

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
        if (GameManager.instance.state == GameState.Gameplay)
        {
            Time.timeScale = (float)GameState.Paused;
            GameManager.instance.state = GameState.Paused;
        }
        else
            return;
    }

    public void ResumeGame()
    {
        if (GameManager.instance.state == GameState.Paused)
        {
            GameManager.instance.state = GameState.Gameplay;
            Time.timeScale = (float)GameState.Gameplay;
        }
        else
            return;
    }

    public List<int> GenerateRandomNumber(List<int> possibleNumbers)
    {
        List<int> chosenNumber = new List<int>();
        int position = Random.Range(0, possibleNumbers.Count);
        chosenNumber.Add(possibleNumbers[position]);
        possibleNumbers.RemoveAt(position);
        return chosenNumber;
    }

}

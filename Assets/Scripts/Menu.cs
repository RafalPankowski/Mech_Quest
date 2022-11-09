using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Animator _animator;

    private void Start()
    {
        if(GameManager.instance.menu == null)
        {
            GameManager.instance.menu = GetComponent<Menu>();
        }
    }
    public void Play()
    {
        SceneManager.LoadScene("Main");
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void ChooseMech()
    {
        _animator.SetTrigger("ChooseMech");
    }

    public void BackToMenu()
    {
        _animator.SetTrigger("Back");
    }
}

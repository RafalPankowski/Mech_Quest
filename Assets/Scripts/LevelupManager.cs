using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class LevelupManager : MonoBehaviour
{
    public Animator _animator;
    public GameObject[] Guns;
    public GameObject optionContainer;
    
    public GameObject temp;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Upgrade()
    {
        _animator.SetTrigger("LevelUP");
        /*
        for (int i = 0; i < 2; i++)
        {
            Instantiate(temp, optionContainer.transform);
            OptionUpgrade temp = 
        }
        */
    }

}

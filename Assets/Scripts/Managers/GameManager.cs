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




}

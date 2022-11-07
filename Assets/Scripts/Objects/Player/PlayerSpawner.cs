using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerMech;
    private void Awake()
    {
        playerMech = GameManager.instance.mech;
    }

    private void Start()
    {
        Instantiate(playerMech);
    }
}

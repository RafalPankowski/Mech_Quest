using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerMech;
    void Start()
    {
        Instantiate(playerMech);
    }
}

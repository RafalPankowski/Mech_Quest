using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public Transform trPlayer;
    public Renderer renderTerrain;

    void Start()
    {
        trPlayer = FindObjectOfType<Player>().transform;
    }

    void Update()
    {
        transform.position = trPlayer.position;
        renderTerrain.material.mainTextureOffset = new Vector2(trPlayer.position.x,trPlayer.position.y) * (RoundManager.instance.player.thrustSpeed/100);
    }
}

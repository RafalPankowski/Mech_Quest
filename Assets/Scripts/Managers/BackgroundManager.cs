using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public Transform trPlayer;
    public Renderer renderTerrain;
    public float speed;
    float tempScroll;


    // Start is called before the first frame update
    void Start()
    {
        trPlayer = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        /* test
        tempScroll += 0.01f;
        renderTerrain.material.mainTextureOffset = new Vector2(tempScroll, tempScroll);
        */

        transform.position = trPlayer.position;
        renderTerrain.material.mainTextureOffset = new Vector2(trPlayer.position.x, trPlayer.position.y) * speed * RoundManager.instance.player.thrustSpeed;
    }
}

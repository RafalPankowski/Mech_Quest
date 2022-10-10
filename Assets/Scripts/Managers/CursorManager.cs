using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CursorManager : MonoBehaviour
{
    public Texture2D defaultTexture, targetedTexture;
    public CursorMode curMode;
    public Vector2 hotSpot;


    private void Start()
    {
        hotSpot = new Vector2 (defaultTexture.width/2, defaultTexture.height/2);
    }
    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Cursor.SetCursor(targetedTexture, hotSpot, curMode);
        }
        else
        {
            Cursor.SetCursor(defaultTexture, hotSpot, curMode);
        }
    }

}
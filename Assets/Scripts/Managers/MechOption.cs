using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MechOption : MonoBehaviour
{
    public GameObject mech;
    public Image image;

    void Start()
    {
        this.image.sprite = this.mech.GetComponent<SpriteRenderer>().sprite;
    }
    public void MechChoice()
    {
        GameManager.instance.mech = this.mech;
        GameManager.instance.menu.Play();
    }
}

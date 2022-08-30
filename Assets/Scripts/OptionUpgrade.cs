using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUpgrade : MonoBehaviour
{
    public GameObject option, gun;
    public Text Name, Heat, FireRate;
    public Image image;
    public RectTransform rt;

    void Start()
    {
        this.image.sprite = this.gun.GetComponent<SpriteRenderer>().sprite;
        this.Name.text = this.gun.name;
        this.Heat.text = " Heat = " + this.gun.GetComponent<Gun>().Heat.ToString();
        this.FireRate.text = " Fire Rate = " + this.gun.GetComponent<Gun>().fireRate.ToString();
    }
    public void UpgradeMech()
    {
       for (int i = 0; i < GameManager.instance.player.Gun_Slots.Length; i++)
       {
         if (GameManager.instance.player.Gun_Slots[i].transform.childCount == 0)
         {
          GameObject gun = Instantiate(this.gun);
          GameObject slot = GameManager.instance.player.Gun_Slots[i];
          gun.transform.parent = slot.transform;
          gun.transform.position = slot.transform.position;
          gun.transform.localScale = slot.transform.localScale;
          break;
         }
       }
        GameManager.instance.alive = true;
        GameManager.instance.levelupManager._animator.SetTrigger("ChosedUpgrade");
    }
}

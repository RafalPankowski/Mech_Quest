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
        GameObject gun = Instantiate(this.gun);
        for(int i = 0; i <= GameManager.instance.player.Gun_Slots.Length; i++)
        {
            if(GameManager.instance.player.Gun_Slots[i].transform.childCount == 0)
            {
                GameObject slot = GameManager.instance.player.Gun_Slots[i];
                gun.transform.parent = slot.transform;
                gun.transform.position = slot.transform.position;
                break;
            }
        }
        GameManager.instance.levelupManager._animator.SetTrigger("ChosedUpgrade");
    }
}

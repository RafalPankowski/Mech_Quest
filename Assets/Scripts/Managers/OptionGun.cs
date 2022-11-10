using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionGun : MonoBehaviour
{
    public bool active;
    public GameObject option, gun;
    public Text Name, Heat, FireRate;
    public Image image;
    public RectTransform rt;
    public Button button;

    void Start()
    {
        this.image.sprite = this.gun.GetComponent<SpriteRenderer>().sprite;
        this.Name.text = this.gun.name;
        this.Heat.text = " Heat = " + this.gun.GetComponent<Gun>().Heat[0].ToString();
        this.FireRate.text = " Fire Rate = " + this.gun.GetComponent<Gun>().fireRate[0].ToString();
        this.button.GetComponent<Button>().onClick.AddListener(delegate { RoundManager.instance.levelManager.ImplementGun(gun); });
        this.button.GetComponent<Button>().onClick.AddListener(delegate { RoundManager.instance.levelManager.UpgradeChoosed(gun); });
    }
}

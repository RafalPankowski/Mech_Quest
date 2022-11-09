using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionStat : MonoBehaviour
{
    public bool active;
    public string stat;
    public GameObject option;
    public Text Name, Skill_level;
    public Sprite[] statSprites;
    public Image image;
    public RectTransform rt;
    public Button button;

    void Start()
    {
        var playerStat = RoundManager.instance.player;
        this.Name.text = this.stat;

        switch(stat)
        {
            case "FireRate":
                this.Skill_level.text = " Increasied skill to : " + playerStat.FireRate[playerStat.FireRateLevel+1];
                this.image.sprite = statSprites[0];
                this.button.GetComponent<Button>().onClick.AddListener(delegate { RoundManager.instance.levelManager.IncreaseStat(stat); });
                break;
            case "CoolRate":
                this.Skill_level.text = " Increasied skill to : " + playerStat.coolRate[playerStat.coolLevel + 1];
                this.image.sprite = statSprites[0];
                this.button.GetComponent<Button>().onClick.AddListener(delegate { RoundManager.instance.levelManager.IncreaseStat(stat); });
                break;
        }
    }
}

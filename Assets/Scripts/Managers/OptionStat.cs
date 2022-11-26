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
                this.Skill_level.text = " Increasied skill: " + playerStat.FireRateBonus[playerStat.FireRateLevel+1] + "%";
                this.image.sprite = statSprites[1];
                this.button.GetComponent<Button>().onClick.AddListener(delegate { RoundManager.instance.levelManager.IncreaseStat(stat); });
                break;
            case "CoolingRate":
                this.Skill_level.text = " Increasied skill: " + playerStat.coolingBonus[playerStat.coolingLevel + 1] + "%";
                this.image.sprite = statSprites[0];
                this.button.GetComponent<Button>().onClick.AddListener(delegate { RoundManager.instance.levelManager.IncreaseStat(stat); });
                break;
            case "MaxHeat":
                this.Skill_level.text = " Increasied skill to: " + (playerStat.maxHeat + ((int)playerStat.maxHeat / 10)) ;
                this.image.sprite = statSprites[0];
                this.button.GetComponent<Button>().onClick.AddListener(delegate { RoundManager.instance.levelManager.IncreaseStat(stat); });
                break;
            case "DamageBonus":
                this.Skill_level.text = " Increasied skill: " + playerStat.damageBonus[playerStat.damageBonusLevel + 1] + "%";
                this.image.sprite = statSprites[2];
                this.button.GetComponent<Button>().onClick.AddListener(delegate { RoundManager.instance.levelManager.IncreaseStat(stat); });
                break;
        }
    }
}

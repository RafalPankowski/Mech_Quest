using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HubManager : MonoBehaviour
{
    public Text levelText, livesText, hitpointText;
    public RectTransform xpBar;
    public GameObject heatBar;
    public RectTransform heatLevel;

    private void FixedUpdate()
    {
        LivesCounter();
        HitpointCounter();
        //SetLevel();
        xpBarUpdate();
        HeatLevel();
    }


    public void xpBarUpdate()
    {
        int currLevel = RoundManager.instance.levelManager.GetCurrentLevel();
        if (currLevel == RoundManager.instance.levelManager.xpTable.Count)
        {
            return;
        }
        else
        {
            int prevLevelXp = RoundManager.instance.levelManager.GetXpToLevel(currLevel - 1);
            int currLevelXp = RoundManager.instance.levelManager.GetXpToLevel(currLevel);

            int diff = currLevelXp - prevLevelXp;
            int currXpIntoLevel = RoundManager.instance.levelManager.exp - prevLevelXp;

            float completionRatio = (float)currXpIntoLevel / (float)diff;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
        }
    }

    public void LivesCounter()
    {
        this.livesText.text = "x" + RoundManager.instance.lives.ToString();
    }

    public void HitpointCounter()
    {
        this.hitpointText.text = RoundManager.instance.player.hitpoint.ToString();
    }
    public void SetLevel()
    {
        levelText.text = RoundManager.instance.levelManager.GetCurrentLevel().ToString();
    }

    public void HeatLevel()
    {
        if (RoundManager.instance.player.curHeat <= 0)
        {
            heatBar.SetActive(false);
        }
        else
        {
            heatBar.SetActive(true);
            float maxLevel = RoundManager.instance.player.maxHeat;
            float currLevel = RoundManager.instance.player.curHeat;

            float completionRatio = (120 * currLevel)/maxLevel;
            heatLevel.localScale = new Vector3(completionRatio, 1, 1);
        }
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HubManager : MonoBehaviour
{
    public Text levelText, livesText, hitpointText;
    public RectTransform xpBar;

    private void FixedUpdate()
    {
        LivesCounter();
        HitpointCounter();
        SetLevel();
        xpBarUpdate();
    }

    public void xpBarUpdate()
    {
        int currLevel = GameManager.instance.GetCurrentLevel();
        if (currLevel == GameManager.instance.xpTable.Count)
        {
            return;
        }
        else
        {
            int prevLevelXp = GameManager.instance.GetXpToLevel(currLevel - 1);
            int currLevelXp = GameManager.instance.GetXpToLevel(currLevel);

            int diff = currLevelXp - prevLevelXp;
            int currXpIntoLevel = GameManager.instance.exp - prevLevelXp;

            float completionRatio = (float)currXpIntoLevel / (float)diff;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
        }
    }

    public void LivesCounter()
    {
        this.livesText.text = "x" + GameManager.instance.lives.ToString();
    }

    public void HitpointCounter()
    {
        this.hitpointText.text = GameManager.instance.player.hitpoint.ToString();
    }
    public void SetLevel()
    {
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();
    }

}


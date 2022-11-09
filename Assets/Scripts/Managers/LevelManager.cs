using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Animator _animator;
    public GameObject[] Guns;
    public GameObject optionPanel;
    public GameObject gunPrefarb, statPrefarb;

    public int exp = 0;
    public int lvl;
    public List<int> xpTable;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;
        while (exp >= add)
        {
            add += xpTable[r];
            r++;
            if (r == xpTable.Count)
                return r;
        }
        return r;
    }
    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;
        while (r < level)
        {
            xp += xpTable[r];
            r++;
        }
        return xp;
    }
    public void GrantXp(int xp)
    {
        int currLevel = GetCurrentLevel();
        exp += xp;
        if (currLevel < GetCurrentLevel())
            OnLevelUp();
    }
    public void OnLevelUp()
    {
        Debug.Log("Level Up!");
        RoundManager.instance.player.OnLevelUp();
        GameManager.instance.PauseGame();
        lvl++;
        Upgrade();
    }
    public void Upgrade()
    {
        string[] upgradeCategory = {"Gun","Stat"};
        _animator.SetTrigger("LevelUP");
        for (int i = 0; i <= Random.Range(2, 3); i++)
        {
            int index = Random.Range(0,upgradeCategory.Length);
            string choosed = upgradeCategory[index];
            switch (choosed)
            {
                case "Gun":
                    bool gunSlotAvailable = true;
                    for (int ii = 0; ii < RoundManager.instance.player.Gun_Slots.Length; ii++)
                    {
                        if (RoundManager.instance.player.Gun_Slots[ii].transform.childCount == 0)
                        {
                            gunSlotAvailable = false;
                            break;
                        }
                    }
                    if (Guns.Length != 0 && gunSlotAvailable == false)
                    {                 
                        int gIndex = Random.Range(0, Guns.Length);
                        OptionGun(Guns[index], new Vector3(0, i * -110.0f));
                    }
                    else
                    {
                        i--;
                    }
                    break;
                case "Stat":
                    string[] statName = {"FireRate","CoolRate" };
                    int sIndex = Random.Range(0, statName.Length);
                    OptionStat(statName[sIndex], new Vector3(0, i * -110.0f));
                    Debug.Log("wybrano statystyke");
                    break;
            }
        }
    }
    public void OptionGun(GameObject Gun, Vector3 position)
    {
        GameObject option = Instantiate(gunPrefarb);
        option.GetComponent<OptionGun>().gun = Gun;
        option.transform.SetParent(optionPanel.transform, transform.parent);
        option.transform.localPosition = position;
    }
    
    public void OptionStat(string statName, Vector3 position)
    {
        GameObject option = Instantiate(statPrefarb);
        option.GetComponent<OptionStat>().stat = statName;
        option.transform.SetParent(optionPanel.transform, transform.parent);
        option.transform.localPosition = position;
    }

    public void IncreaseStat(string statName)
    {
        var playerStat = RoundManager.instance.player;
        switch(statName)
        {
            case "FireRate":
                playerStat.FireRateLevel++;
                break;
            case "CoolRate":
                playerStat.coolLevel++;
                break;
        }
        _animator.SetTrigger("ChosedUpgrade");
        foreach (Transform child in RoundManager.instance.levelManager.optionPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        GameManager.instance.ResumeGame();
    }

    public void ImplementGun(GameObject Gun)
    {
        for (int i = 0; i < RoundManager.instance.player.Gun_Slots.Length; i++)
        {
            if (RoundManager.instance.player.Gun_Slots[i].transform.childCount == 0)
            {
                GameObject gun = Instantiate(Gun);
                GameObject slot = RoundManager.instance.player.Gun_Slots[i];
                gun.transform.parent = slot.transform;
                gun.transform.position = slot.transform.position;
                gun.transform.localScale = slot.transform.localScale;
                break;
            }
        }
        UpgradeChoosed(Gun);
    }
    public void OrganizedGunInArray(GameObject gun)
    {
        GameObject[] temp = new GameObject[Guns.Length - 1];
        int z = 0;
        for (int i = 0; i < Guns.Length; i++)
        {
            if (Guns[i].name != gun.name)
            {
                temp[z] = Guns[i];
            }
            else
            {
                continue;
            }
            z++;
        }
        Guns = temp;
    }
    public void UpgradeChoosed(GameObject Gun)
    {
        _animator.SetTrigger("ChosedUpgrade");
        foreach (Transform child in RoundManager.instance.levelManager.optionPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        GameManager.instance.ResumeGame();
        OrganizedGunInArray(Gun);
    }
}

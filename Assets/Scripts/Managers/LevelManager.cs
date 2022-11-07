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
    public GameObject optionPrefarb;

    public int exp = 0;
    public int lvl = 1;
    public List<int> xpTable;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Upgrade()
    {
        _animator.SetTrigger("LevelUP");
        for (int i = 0; i <= Random.Range(2,3); i++)
        {
            int upgradeChosed = Random.Range(0, Guns.Length);
            OptionUpgrade(Guns[upgradeChosed], new Vector3(0, i * -110.0f));
        }
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
        Upgrade();
        GameManager.instance.PauseGame();
    }

    public void OptionUpgrade(GameObject Gun, Vector3 position)
    {
        GameObject option = Instantiate(optionPrefarb);
        option.GetComponent<OptionUpgrade>().gun = Gun;
        option.transform.SetParent(optionPanel.transform, transform.parent);
        option.transform.localPosition = position;
    }
}

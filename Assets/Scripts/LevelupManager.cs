using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class LevelupManager : MonoBehaviour
{
    public Animator _animator;
    public GameObject[] Guns;
    public GameObject optionContainer;
    public GameObject optionPrefarb;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Upgrade()
    {
        _animator.SetTrigger("LevelUP");
        for (int i = 0; i <= 2; i++)
        {
            int upgradeChosed = Random.Range(0, Guns.Length);
            OptionUpgrade(Guns[upgradeChosed], new Vector3(0, i * -110.0f));
        }
    }

    public void OptionUpgrade(GameObject Gun, Vector3 position)
    {
        GameObject option = Instantiate(optionPrefarb);
        option.GetComponent<OptionUpgrade>().gun = Gun;
        option.transform.SetParent(optionContainer.transform, transform.parent);
        option.transform.localPosition = position;
    }
}

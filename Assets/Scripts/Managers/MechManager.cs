using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechManager : MonoBehaviour
{

    public GameObject[] mech;
    public GameObject optionContainer;
    public GameObject optionMech;

    public void Start()
    {
        for(int i = 0; i < mech.Length; i++)
        {
            MechOption(mech[i], new Vector3(0,-20.0f + ( i * -120.0f)));
        }
    }

    public void MechOption(GameObject mech, Vector3 position)
    {
        GameObject option = Instantiate(optionMech);
        option.GetComponent<MechOption>().mech = mech;
        option.transform.SetParent(optionContainer.transform, transform.parent);
        option.transform.localPosition = position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraHP : MonoBehaviour
{
    public Image hp;
    private int lastHP;

    public string who;

    private void Start()
    {
        HPUpdate();
    }

    private void Update()
    {
        if ( (who == "player" && lastHP != GameManager.instance.PHp) ||
             (who == "enemy" && lastHP != GameManager.instance.EHp) )
            HPUpdate();
    }

    private void HPUpdate()
    {
        int myhp = 0;
        if (who == "player")
            myhp = GameManager.instance.PHp;
        else if (who == "enemy")
            myhp = GameManager.instance.EHp;

        foreach (Transform child in transform.GetComponentInChildren<Transform>())
        Destroy(child.gameObject);
        if (myhp > 0)
        {
            for (int i = 0; i < myhp; i++)
            {
                Image health = Instantiate(hp, gameObject.transform);
                health.transform.position += new Vector3(55 * i, 0, 0);
            }
            lastHP = myhp;
        }
    }
}

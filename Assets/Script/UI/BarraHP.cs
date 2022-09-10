using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraHP : MonoBehaviour
{
    public Image hp;
    private int lastHP;

    private void Start()
    {
        HPUpdate();
    }

    private void Update()
    {
        if (lastHP != GameManager.instance.PHp)
            HPUpdate();
    }

    private void HPUpdate()
    {
        foreach (Transform child in transform.GetComponentInChildren<Transform>())
            Destroy(child.gameObject);
        if (GameManager.instance.PHp > 0)
        {
            for (int i = 0; i < GameManager.instance.PHp; i++)
            {
                Image health = Instantiate(hp, gameObject.transform);
                health.transform.position += new Vector3(55 * i, 0, 0);
            }
            lastHP = GameManager.instance.PHp;
        }
    }
}

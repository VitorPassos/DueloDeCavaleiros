using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnAttackTypes attack;
    public EnBlockTypes block;
    public bool shield = false;

    Vector3 move;
    Vector3 shake;
    float startY;
    float bob = -50;
    [SerializeField] float bobForce = 20;
    [SerializeField] float bobFreq = 3;

    void Start()
    {
        move = transform.position;
        startY = move.y;
    }


    void Update()
    {
        if (GameManager.instance.ECooldown == 0)
        {
            if (!shield)
            {
                move.y = startY + Mathf.Sin(bob * bobFreq) / bobForce;
                transform.position = move;
            }
        }

        //block = new EnMidBlock();
        //block.Block(this);
    }

    private void FixedUpdate()
    {
        //Valores atualizados por frame
        if (GameManager.instance.ECooldown > 0)
            GameManager.instance.ECooldown -= 1;

        if (GameManager.instance.ECooldown == 0 && !shield)
            bob += 0.022f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnHighBlock : EnBlockTypes
{
    public void Block(Enemy e)
    {
        e.shield = true;
        GameManager.instance.EBlock = 2;
    }
}

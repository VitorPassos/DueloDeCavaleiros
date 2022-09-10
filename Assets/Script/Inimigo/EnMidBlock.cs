using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnMidBlock : EnBlockTypes
{
    public void Block(Enemy e)
    {
        e.shield = true;
        GameManager.instance.EBlock = 1;
    }
}

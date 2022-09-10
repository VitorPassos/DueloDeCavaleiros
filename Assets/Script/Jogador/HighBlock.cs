using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighBlock : BlockTypes
{
    public void Block(Player p)
    {
        p.shield = true;
        GameManager.instance.PBlock = 2;

        //Debug - desenha escudo
        Vector3 l = p.transform.position;
        Vector3 r = p.transform.position;
        Vector3 t = p.transform.position;
        Vector3 b = p.transform.position;

        t.z += 1;
        t.y += 0.75f;

        b.z += 1;
        b.y -= 0.25f;

        l.z += 1;
        l.x += 0.5f;
        l.y += 0.25f;

        r.z += 1;
        r.x -= 0.5f;
        r.y += 0.25f;

        Debug.DrawLine(l, r, Color.red);
        Debug.DrawLine(t, b, Color.red);

        Debug.DrawLine(t, l, Color.red);
        Debug.DrawLine(t, r, Color.red);

        Debug.DrawLine(b, l, Color.red);
        Debug.DrawLine(b, r, Color.red);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnMidBlock : EnBlockTypes
{
    private IEnumerator coroutine;
    public void Block(Enemy e)
    {
        coroutine = Shielding(e);
        e.StartCoroutine(coroutine);
    }

    private IEnumerator Shielding(Enemy e)
    {
        while (e.decisionTimer > 0 && !GameManager.instance.EHit)
        {
            e.EAnim.Play("EAnim.MidShield");
            if (GameManager.instance.PHit)
            {
                e.decisionTimer = e.decisionSpeed + Random.Range(-20, 21);
                e.attack = new EnHighSlash();
                e.attack.Attack(e);
                e.RemShield();
                yield break;
            }

            e.shield = true;
            GameManager.instance.EBlock = 1;
            yield return new WaitForFixedUpdate();

            //Debug - desenha escudo
            Vector3 l = e.transform.position;
            Vector3 r = e.transform.position;
            Vector3 t = e.transform.position;
            Vector3 b = e.transform.position;

            t.z += 1;
            t.y += 0.25f;

            b.z += 1;
            b.y -= 0.75f;

            l.z += 1;
            l.x += 0.5f;
            l.y -= 0.25f;

            r.z += 1;
            r.x -= 0.5f;
            r.y -= 0.25f;

            Debug.DrawLine(l, r, Color.blue);
            Debug.DrawLine(t, b, Color.blue);

            Debug.DrawLine(t, l, Color.blue);
            Debug.DrawLine(t, r, Color.blue);

            Debug.DrawLine(b, l, Color.blue);
            Debug.DrawLine(b, r, Color.blue);

        }
        e.RemShield();
    }
}

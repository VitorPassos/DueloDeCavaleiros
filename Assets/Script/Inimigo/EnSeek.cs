using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnSeek : EnMoveType
{
    private IEnumerator coroutine;
    public void RandomMove(Enemy e)
    {
        coroutine = Move(e);
        e.StartCoroutine(coroutine);
    }

    private IEnumerator Move(Enemy e)
    {
        while (e.decisionTimer > 0)
        {
            e.move.x = Mathf.Clamp(e.move.x, GameManager.instance.Px - e.maxReach, GameManager.instance.Px + e.maxReach);
            yield return new WaitForFixedUpdate();
        }
    }
}

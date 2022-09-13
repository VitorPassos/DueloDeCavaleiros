using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnRandomMove : EnMoveType
{
    private IEnumerator coroutine;
    public void RandomMove(Enemy e)
    {
        coroutine = Move(e);
        e.StartCoroutine(coroutine);
    }

    private IEnumerator Move(Enemy e)
    {
        float randSpeed = Random.Range(0, e.topSpeed);
        int randDir = Random.Range(-1, 2);
        while (randDir == 0) { randDir = Random.Range(-1, 2); }

        while (e.decisionTimer > 0)
        {
            e.move.x += randDir * Time.deltaTime * randSpeed;
            e.move.x = Mathf.Clamp(e.move.x, -e.maxReach, e.maxReach);
            yield return new WaitForFixedUpdate();
        }

    }
}

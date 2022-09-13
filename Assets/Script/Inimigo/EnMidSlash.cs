using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnMidSlash : EnAttackTypes
{
    private IEnumerator coroutine;

    private static int ID = 1;                          //Tipo de ataque que será comparado no GM
    private static int ADELAY = 45;                     //Atraso do ataque em frames entre o botão ser apertado e a espada usada
    private static int COOLD = 55;                      //Por quantos frames o jogador é travado ao atacar
    private static float RANGE = 0.525f;                //Tamanho max do ataque (esq/dir)


    public void Attack(Enemy e)
    {
        coroutine = Slash(e);
        e.StartCoroutine(coroutine);
    }

    private IEnumerator Slash(Enemy e)
    {
        //Debug - ver rays
        Vector3 pos = e.transform.position;
        Debug.DrawRay(pos + Vector3.left * RANGE, Vector3.forward, Color.blue, (float)ADELAY / 50);
        Debug.DrawRay(pos + Vector3.left * (RANGE / 4), Vector3.forward, Color.blue, (float)ADELAY / 50);
        Debug.DrawRay(pos + Vector3.right * RANGE, Vector3.forward, Color.blue, (float)ADELAY / 50);
        Debug.DrawRay(pos + Vector3.right * (RANGE / 4), Vector3.forward, Color.blue, (float)ADELAY / 50);

        GameManager.instance.ECooldown = COOLD;

        e.EAnim.Play("EAnim.MidSlash");
        GameManager.instance.EAttack = ID;
        float epos = e.transform.position.x;

        for (int i = 0; i < ADELAY; i++)
        {
            if (GameManager.instance.EHit) { yield break; }
            yield return new WaitForFixedUpdate();
        }

        if (epos < GameManager.instance.Px + RANGE*2 && epos > GameManager.instance.Px - RANGE*2)
        {
            if (GameManager.instance.PBlock == GameManager.instance.EAttack)
            {
                SoundManager.instance.PlaySound("ShieldHit");
                e.shaken = true;
                GameManager.instance.ECooldown += COOLD + COOLD / 3;
            }
            else
            {
                SoundManager.instance.PlaySound("SlashM");
                GameManager.instance.PHp -= 1;
                GameManager.instance.PHit = true;
            }
        }
        else
        {
            GameManager.instance.ECooldown += 5;
            SoundManager.instance.PlaySound("MissM");
        }
    }
}

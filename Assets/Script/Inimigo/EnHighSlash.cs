using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnHighSlash : EnAttackTypes
{
    private IEnumerator coroutine;

    private static int ID = 2;                          //Tipo de ataque que será comparado no GM
    private static int ADELAY = 45;                     //Atraso do ataque em frames entre o botão ser apertado e a espada usada
    private static int COOLD = 55;                      //Por quantos frames o jogador é travado ao atacar
    private static float RANGE = 0.35f;                //Tamanho max do ataque (esq/dir)

    public void Attack(Enemy e)
    {
        coroutine = Slash(e);
        e.StartCoroutine(coroutine);
    }

    private IEnumerator Slash(Enemy e)
    {
        //Debug - ver rays
        Vector3 pos = e.transform.position;
        Debug.DrawRay(pos + (Vector3.up * 0.7f) + (Vector3.right * RANGE/2), Vector3.forward, Color.blue, (float)ADELAY / 50);
        Debug.DrawRay(pos + (Vector3.up * 0.35f) + (Vector3.left * RANGE/2), Vector3.forward, Color.blue, (float)ADELAY / 50);
        Debug.DrawRay(pos - (Vector3.up * 0.35f) + (Vector3.right * RANGE/2), Vector3.forward, Color.blue, (float)ADELAY / 50);
        Debug.DrawRay(pos - (Vector3.up * 0.7f) + (Vector3.left * RANGE/2), Vector3.forward, Color.blue, (float)ADELAY / 50);

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

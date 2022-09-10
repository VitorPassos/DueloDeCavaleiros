using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidSlash : AttackTypes
{
    private static int ID = 1;                          //Tipo de ataque que será comparado no GM
    private static int ADELAY = 8;                      //Atraso do ataque em frames entre o botão ser apertado e a espada usada
    private static int COOLD = 20;                      //Por quantos frames o jogador é travado ao atacar
    private static float RANGE = 0.525f;                //Tamanho max do ataque (esq/dir)

    public void Attack(Player p)
    {
        GameManager.instance.PCooldown = COOLD;
        IEnumerator coroutine = Slash(p);               //Iniciando ataque através de uma coroutine
        p.StartCoroutine(coroutine);
    }

    private IEnumerator Slash(Player p)
    {
        GameManager.instance.PAttack = ID;

        for (int i = 0; i < ADELAY; i++)
            yield return new WaitForFixedUpdate();

        Vector3 pos = p.transform.position + Vector3.forward * 0.51f;
        bool hit1 = Physics.Raycast(pos + Vector3.left * RANGE, Vector3.forward, 2);
        bool hit2 = Physics.Raycast(pos + Vector3.left * (RANGE/4), Vector3.forward, 2);
        bool hit3 = Physics.Raycast(pos + Vector3.right * RANGE, Vector3.forward, 2);
        bool hit4 = Physics.Raycast(pos + Vector3.right * (RANGE/4), Vector3.forward, 2);

        if (hit1 || hit2 || hit3 || hit4)                                    //Acertamos alguma coisa?
        {
            if (GameManager.instance.EBlock == GameManager.instance.PAttack) //Acertamos o escudo...aumente nosso cooldown para 1.3x do nosso ataque e aplique shaken
            {
                p.shaken = true;
                GameManager.instance.PCooldown += COOLD + COOLD / 3;
            }
            else                                                             //Ataque direto! Diminua o HP do inimigo
            {
                SoundManager.instance.PlaySound("SlashM");
                GameManager.instance.EHp -= 1;
                GameManager.instance.EHit = true;
            }
        }
        else                                                                 //Erramos. Aumente nosso cooldown para criar um tempo de vulnerabilidade extra
        {
            GameManager.instance.PCooldown += 5;
            SoundManager.instance.PlaySound("MissM");
        }

        //Debug - ver rays
        Debug.DrawRay(pos + Vector3.left * RANGE, Vector3.forward, Color.red, 0.2f);
        Debug.DrawRay(pos + Vector3.left * (RANGE / 4), Vector3.forward, Color.red, 0.2f);
        Debug.DrawRay(pos + Vector3.right * RANGE, Vector3.forward, Color.red, 0.2f);
        Debug.DrawRay(pos + Vector3.right * (RANGE / 4), Vector3.forward, Color.red, 0.2f);

    }
}
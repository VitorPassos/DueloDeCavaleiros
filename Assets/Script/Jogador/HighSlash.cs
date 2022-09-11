using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighSlash : AttackTypes
{
    private static int ID = 2;                          //Tipo de ataque que será comparado no GM
    private static int ADELAY = 10;                     //Atraso do ataque em frames entre o botão ser apertado e a espada usada
    private static int COOLD = 28;                      //Por quantos frames o jogador é travado ao atacar
    private static float RANGE = 0.35f;                  //Tamanho max do ataque (esq/dir)

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
        {
            if (GameManager.instance.PHit) { yield break; }
            yield return new WaitForFixedUpdate();
        }

        float ppos = p.transform.position.x;

        if (ppos < GameManager.instance.Ex + RANGE*2 && ppos > GameManager.instance.Ex - RANGE*2)
        {
            if (GameManager.instance.EBlock == GameManager.instance.PAttack) //Acertamos o escudo...aumente nosso cooldown para 46 frames e aplique shaken
            {
                SoundManager.instance.PlaySound("ShieldHit");
                p.shaken = true;
                GameManager.instance.PHit = true;
                GameManager.instance.PCooldown += 46;    
            }
            else                                                             //Ataque direto! Diminua o HP do inimigo
            {
                SoundManager.instance.PlaySound("SlashH");
                GameManager.instance.EHp -= 1;
                GameManager.instance.EHit = true;
            }
        }
        else                                                                 //Erramos. Aumente nosso cooldown para criar um tempo de vulnerabilidade extra
        {
            GameManager.instance.PCooldown += 5;                        
            SoundManager.instance.PlaySound("MissH");
        }

        //Debug - ver rays
        Vector3 pos = p.transform.position + Vector3.forward * 0.51f;
        Debug.DrawRay(pos + (Vector3.up * 0.7f) + (Vector3.right * RANGE/2), Vector3.forward, Color.red, 0.2f);
        Debug.DrawRay(pos + (Vector3.up * 0.35f) + (Vector3.left * RANGE/2), Vector3.forward, Color.red, 0.2f);
        Debug.DrawRay(pos - (Vector3.up * 0.35f) + (Vector3.right * RANGE/2), Vector3.forward, Color.red, 0.2f);
        Debug.DrawRay(pos - (Vector3.up * 0.7f) + (Vector3.left * RANGE/2), Vector3.forward, Color.red, 0.2f);
    }
}

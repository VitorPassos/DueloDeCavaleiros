using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public AttackTypes attack;                  //Strategy - ataques
    public BlockTypes block;                    //Strategy - escudo
    public bool shield = false;                 //Jogador está defendendo?
    public bool shaken = false;                 //Jogador está chacoalhando?

    Vector3 move;                               //Movimento geral
    Vector3 shake;                              //Jogador bateu em um escudo
    float alternator = 1;                       //Var multi-uso que alterna entra 1 e -1
    float bob = 0;                              //Contador que agirá como seno
    float startY;                               //Referência de onde o jogador começou a chacoalhar
    [SerializeField] float bobForce = 20;       //Força do movimento cima/baixo
    [SerializeField] float bobFreq = 3;         //Frequência do movimento cima/baixo
    [SerializeField] float speed = 3;           //Velocidade do movimento cima/baixo
    [SerializeField] float maxReach = 2;        //Distância máxima que o jogador pode ir na esquerda/direita

    private void Start()
    {
        move = transform.position;  //Pareando Vector3 com transform do player
        startY = move.y;
    }

    void Update()
    {
        if (GameManager.instance.PCooldown == 0)                                    //Tudo aqui só é possível se o jogador não está em cooldown/hit stun
        {
            if (!shield)
            {
                //Movimento
                move.x += Input.GetAxis("Horizontal") * Time.deltaTime * speed;     //Deslocamento
                move.x = Mathf.Clamp(move.x, -maxReach, maxReach);                  //Distância máxima de deslocamento em X
                move.y = startY + Mathf.Sin(bob * bobFreq) / bobForce;              //Movimento cima/baixo constante
                transform.position = move;


                //Combate
                if (Input.GetButtonDown("A"))                                       //Ataques
                {
                    if (Mathf.Round(Input.GetAxis("Vertical")) > 0)                 
                        attack = new HighSlash();                                   //Ataque alto
                    else                                                            
                        attack = new MidSlash();                                    //Ataque médio
                    attack.Attack(this);
                }
            }

            if (Input.GetButton("B"))                                               //Defesa
            {
                if (Mathf.Round(Input.GetAxis("Vertical")) > 0)
                    block = new HighBlock();                                        //Defesa alta
                else
                    block = new MidBlock();                                         //Defesa média
                block.Block(this);
            }
            else
                RemShield();
        } else
            RemShield();

        if (GameManager.instance.PHit) { StartCoroutine("WaitRemPHit"); }           //Fomos acertados. Inicie a corotina que remove o status de hit.

        GameManager.instance.Px = transform.position.x;                             //Informando nossa posição X ao GM (usado pelo inimigo)
    }

    private void FixedUpdate()
    {
        //Valores atualizados por frame
        if (GameManager.instance.PCooldown > 0)                             //Cooldown
            GameManager.instance.PCooldown -= 1;

        if (GameManager.instance.PCooldown == 0 && !shield)
            bob += 0.022f;                                                  //Movimento cima/baixo

        alternator *= -1;                                                   //Alternando alternador

        //Player acertou o escudo do inimigo
        if (!shaken)
            shake = transform.position;                                     //Mantendo Vector3 shake atualizado para quando levarmos stun
        if (shaken && GameManager.instance.PCooldown > 0)
        {
            shake.x += alternator / 20;                                     //Shake shake
            transform.position = shake;
        } else if (shaken && GameManager.instance.PCooldown == 0)
        {
            transform.position = move;                                      //Voltando ao normal
            shaken = false;
        }

    }

    private void RemShield() { shield = false; GameManager.instance.PBlock = -1; }  //Remover escudo e atualizar GM

    private IEnumerator WaitRemPHit()
    { 
        for (int i = 0; i < 3; i++) { yield return new WaitForFixedUpdate(); }      //Esperar 3 frames e mover status PHit do GM
        GameManager.instance.PHit = false;
    }
}

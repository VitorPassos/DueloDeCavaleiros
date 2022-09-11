using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnAttackTypes attack;
    public EnBlockTypes block;
    public EnMoveType movement;
    public bool shield = false;
    public bool shaken = false;
    public int decisionSpeed = 60;
    public int decisionTimer;
    public float maxReach = 2;
    public float topSpeed = 2.3f;
    public Vector3 move;

    Vector3 shake;
    float startY;
    float bob = -50;
    float alternator = 1;
    [SerializeField] float bobForce = 20;
    [SerializeField] float bobFreq = 3;

    private void Awake()
    {
        decisionTimer = decisionSpeed;
    }

    void Start()
    {
        move = transform.position;
        startY = move.y;
        StartCoroutine("AI");
    }


    void Update()
    {
        if (GameManager.instance.ECooldown == 0)
        {
            if (!shield)
            {
                move.y = startY + Mathf.Sin(bob * bobFreq) / bobForce;
                transform.position = move;
            }
        }

        if (GameManager.instance.EHit) { RemShield(); StartCoroutine("WaitRemEHit"); }

        GameManager.instance.Ex = transform.position.x;
    }

    private void FixedUpdate()
    {
        //Valores atualizados por frame
        if (GameManager.instance.ECooldown > 0)
            GameManager.instance.ECooldown -= 1;

        if (GameManager.instance.ECooldown == 0 && !shield)
            bob += 0.022f;

        alternator *= -1;

        if (decisionTimer > 0)
            decisionTimer -= 1;                                 //O que define quando nossa IA decide algo

        //Inimigo acertou o escudo do jogador
        if (!shaken)
            shake = transform.position;                         //Mantendo Vector3 shake atualizado para quando levarmos stun
        if (shaken && GameManager.instance.ECooldown > 0)
        {
            shake.x += alternator / 20;                         //Shake shake
            transform.position = shake;
        }
        else if (shaken && GameManager.instance.ECooldown == 0)
        {
            transform.position = move;                          //Voltando ao normal
            shaken = false;
        }
    }

    public void RemShield() { shield = false; GameManager.instance.EBlock = -1; }  //Remover escudo e atualizar GM

    private IEnumerator WaitRemEHit()
    {
        GameManager.instance.ECooldown = 5;
        decisionTimer = 5;
        for (int i = 0; i < 3; i++) { yield return new WaitForFixedUpdate(); }      //Esperar 3 frames e mover status PHit do GM
        GameManager.instance.EHit = false;
    }

    private IEnumerator AI()
    {
        while(GameManager.instance.EHp > 0)
        {
            if (GameManager.instance.ECooldown == 0 && decisionTimer <= 0)
            {
                int choice = Random.Range(0, 101);
                decisionTimer = decisionSpeed + Random.Range(-20, 21);

                //TO DO - colocar um IF aqui para checar a posição do inimigo antes de realizar uma ação para impedir "snap" de posições

                if (choice < 15)                                            //Mexa (0-15 C)
                {
                    movement = new EnSeek();
                    movement.RandomMove(this);
                }
                else if (choice < 15 + 15)                                  //Escudo acima (15-30 C)                   
                {
                    block = new EnHighBlock();
                    block.Block(this);
                }
                else if (choice < 15 + 30)                                  //Escudo meio (30-45 C)
                {
                    block = new EnMidBlock();
                    block.Block(this);
                }
                else if (choice < 15 + 45)                                  //Ataque acima (45-60 C)
                {
                    attack = new EnHighSlash();
                    attack.Attack(this);
                }
                else if (choice < 15 + 60)                                  //Ataque meio (60-75 C)
                {
                    attack = new EnMidSlash();
                    attack.Attack(this);
                }
                else                                                        //Pare de agir e siga o limite de movimento (75-100 C)
                {
                    movement = new EnRandomMove();
                    movement.RandomMove(this);
                }

            }
            yield return new WaitForEndOfFrame();
        }
    }
}

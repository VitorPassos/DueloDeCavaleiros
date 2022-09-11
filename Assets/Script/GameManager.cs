using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //Vars de jogador
    public float Px = 0;
    public int PCooldown = 0;
    public int PHp = 10;
    public int PAttack = -1;
    public int PBlock = -1;
    public bool PHit = false;

    //Vars de inimigo
    public float Ex = 0;
    public int ECooldown = 0;
    public int EHp = 4;
    public int EAttack = -1;
    public int EBlock = -1;
    public bool EHit = false;

    //Inicializando o GM
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public GameManager getInstance()
    {
        return instance;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource SlashM;
    public AudioSource SlashH;

    public AudioSource Miss1;
    public AudioSource Miss2;
    public AudioSource MissHigh1;
    public AudioSource MissHigh2;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public SoundManager getInstance()
    {
        return instance;
    }

    public void PlaySound(string aud)
    {
        int rand = 0;
        switch (aud)
        {
            case "SlashM":
                SlashM.Play();
                break;

            case "SlashH":
                SlashH.Play();
                break;

            case "MissM":
                rand = Random.Range(0, 2);
                if (rand == 0)
                    Miss1.Play();
                else
                    Miss2.Play();
                break;

            case "MissH":
                rand = Random.Range(0, 2);
                if (rand == 0)
                    MissHigh1.Play();
                else
                    MissHigh2.Play();
                break;

            default:
                Debug.LogError("Audio escpecificado como string n�o existe");
                break;
        }
        
    }
}

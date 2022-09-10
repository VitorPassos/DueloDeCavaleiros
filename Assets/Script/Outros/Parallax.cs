using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    Vector2 movement;

    public Material front;
    public Material mid;
    public Material back;

    public float frontSpeed = 0;
    public float midSpeed = 0;
    public float backSpeed = 0;

    private void Awake()
    {
        front.SetTextureOffset("_MainTex", Vector2.zero);
        mid.SetTextureOffset("_MainTex", Vector2.zero);
        back.SetTextureOffset("_MainTex", Vector2.zero);
    }

    private void FixedUpdate()
    {
        movement = front.GetTextureOffset("_MainTex");
        movement.x += frontSpeed / 1000;
        front.SetTextureOffset("_MainTex", movement);

        movement = mid.GetTextureOffset("_MainTex");
        movement.x += midSpeed / 1000;
        mid.SetTextureOffset("_MainTex", movement);

        movement = back.GetTextureOffset("_MainTex");
        movement.x += backSpeed / 1000;
        back.SetTextureOffset("_MainTex", movement);
    }

    //Eu até que podia melhorar isso com um loop super besta, mas estou com preguiça.
}

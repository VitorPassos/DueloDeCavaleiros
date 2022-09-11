using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    Vector2 movement;
    public Material[] layer = new Material[4];
    public float[] speed = new float[4];

    private void Awake()
    {
        foreach (Material mat in layer)
            mat.SetTextureOffset("_MainTex", Vector2.zero);
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < layer.Length; i++)
        {
            movement = layer[i].GetTextureOffset("_MainTex");
            movement.x += speed[i] / 1000;
            layer[i].SetTextureOffset("_MainTex", movement);
        }
    }
}

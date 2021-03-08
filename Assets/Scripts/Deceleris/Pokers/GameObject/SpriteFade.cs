using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFade : MonoBehaviour
{
    public float time;
    public SpriteRenderer spriteRenderer;

    float start;

    public void Start()
    {
        start = Time.time;
    }

    public void Update()
    {
        float t = Time.time - start;
        if (t < time) {
            float a = 1 - t / time;
            spriteRenderer.color = spriteRenderer.color.WithA(a);
        }
    }
}
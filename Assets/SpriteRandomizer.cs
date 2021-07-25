using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpriteRandomizer : MonoBehaviour
{
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }
}
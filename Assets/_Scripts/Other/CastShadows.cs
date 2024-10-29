using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastShadows : MonoBehaviour
{
    [SerializeField] private SpriteRenderer parentObject;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = new Color(0f, 0f, 0f, 0.5f);
        transform.localScale *= 1.1f;
        transform.localPosition = new Vector3(-0.5f, 0.3f, 0f);
    }

    private void FixedUpdate()
    {
        _spriteRenderer.sprite = parentObject.sprite;
    }
}

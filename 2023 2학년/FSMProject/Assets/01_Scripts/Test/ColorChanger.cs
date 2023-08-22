using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    public SpriteRenderer SpriteRenderer
    {
        get
        {
            if (_spriteRenderer == null)
            {
                _spriteRenderer = GetComponent<SpriteRenderer>();
            }
            return _spriteRenderer;
        }
    }

    private void Start()
    {
        GenerateColor();
    }

    public void GenerateColor()
    {
        SpriteRenderer.color = Random.ColorHSV();
    }

    public void ResetColor()
    {
        _spriteRenderer.color = Color.white;
    }
}

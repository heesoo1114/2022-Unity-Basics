using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRenderer : MonoBehaviour
{
    [SerializeField] protected int _playerSortinOrder = 0;
    protected SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void FlipSprite(bool value)
    {
        Vector3 localScale = Vector3.one;
        if (value == true)
            localScale.y = -1;
        transform.localScale = localScale;
    }

    public void RenderBehindHead(bool value)
    {
        if(value == true)
        {
            _spriteRenderer.sortingOrder = _playerSortinOrder - 1;
        }
        else
        {
            _spriteRenderer.sortingOrder = _playerSortinOrder + 1;
        }
    }
}

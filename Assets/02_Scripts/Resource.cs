using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : PoolAbleMono
{
    [SerializeField]
    public ResourceDataSO ResourceData;
    private AudioSource _audioSource;
    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = ResourceData.useSound;
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void PickUpResource() //????? ?????? ?? ?????? ?????
    {
        StartCoroutine(DestroyCoroutine());
    }
    IEnumerator DestroyCoroutine()
    {
        _collider.enabled = false;
        _spriteRenderer.enabled = false;
        _audioSource.Play();
        yield return new WaitForSeconds(_audioSource.clip.length + 0.3f);

        PoolManager.Instance.Push(this);
    }
    private void OnDisable()
    {
        StopAllCoroutines();
        transform.DOKill();
    }
    public override void Init()
    {
        _spriteRenderer.enabled = true;
        _collider.enabled = true;
    }
}

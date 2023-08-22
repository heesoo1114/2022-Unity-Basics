using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Resource : PoolAbleMono
{
    public ResourceDataSO ResourceData;

    private AudioSource _audioSource;
    private Collider _collider;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = ResourceData.UseSound;
        _collider = GetComponent<Collider>();
    }

    public void PickUpResource()
    {
        StartCoroutine(DestroyCoroutine());
    }

    private IEnumerator DestroyCoroutine()
    {
        if (_audioSource.clip != null)
        {
            _audioSource.Play();
            yield return new WaitForSeconds(_audioSource.clip.length + 0.3f);
        }
        else
        {
            yield return null;
        }

        PoolManager.Instance.Push(this);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public override void Init()
    {
        _collider.enabled = true;
        gameObject.layer = LayerMask.NameToLayer("Resource");
    }
}

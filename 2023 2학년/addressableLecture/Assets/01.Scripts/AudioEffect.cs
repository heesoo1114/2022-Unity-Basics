using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class AudioEffect : PoolableMono
{
    private AudioSource _audioSource;
    private float _basePitch;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _basePitch = _audioSource.pitch;
    }

    public void PlayClip(AudioClip clip, float pitchRandom = 0)
    {
        if (pitchRandom != 0)
        {
            _audioSource.pitch = _basePitch + Random.Range(-pitchRandom, pitchRandom);
        }

        _audioSource.clip = clip;
        _audioSource.Play();
        StartCoroutine(StopCoroutine(clip.length + 0.1f));
    }

    IEnumerator StopCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        _audioSource.Stop();
        PoolManager.Instance.Push(this);
    }

    public override void Init()
    {

    }
}
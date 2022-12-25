using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    protected AudioSource _audioSource;

    [SerializeField]
    protected float _pitchRandomness = 0.2f;

    protected float _basePitch;

    protected virtual void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    protected virtual void Start()
    {
        _basePitch = _audioSource.pitch;
    }

    protected void PlayClipwithVariablePitch(AudioClip clip)
    {
        float randomPitcch = Random.Range(-_pitchRandomness, +_pitchRandomness);
        _audioSource.pitch = _basePitch + randomPitcch;
        PlayClip(clip);
    }

    protected void PlayClip(AudioClip clip)
    {
        _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}

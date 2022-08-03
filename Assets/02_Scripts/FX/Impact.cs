using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Impact : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void DestroyAfterAnimation()
    {
        Destroy(gameObject); // 나중에 풀매니징으로 변경 예정
    }

    public void SetPositionAndRotation(Vector3 pos, Quaternion rot)
    {
        // transform.position = pos;
        // transform.rotation = rot;   =>  transform.SetPositionAndRotation(pos, rot);

        transform.SetPositionAndRotation(pos, rot);
        _audioSource.Play();
    }

    public void SetScaleAndTime(Vector3 scale, float time)
    {
        transform.localScale = scale;
        Invoke("DestroyAfterAnimation", time);
    }
}

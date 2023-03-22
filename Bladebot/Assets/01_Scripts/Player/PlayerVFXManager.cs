using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFXManager : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem[] _blades;

    private AttackState _atkState;

    private void Awake()
    {
        _atkState = transform.Find("States").GetComponent<AttackState>();
        _atkState.OnAttackStart += PlayBlade;
        _atkState.OnAttackstateEnd += StopBlade;
    }

    private void PlayBlade(int combo)
    {
        _blades[combo - 1].Play();
    }

    private void StopBlade()
    {
        foreach (ParticleSystem p in _blades)
        {
            p.Simulate(0);
            p.Stop();
        }
    }
}

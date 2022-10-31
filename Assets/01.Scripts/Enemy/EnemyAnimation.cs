using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator _animator;
    private Enemy _enemy;
    private EnemyHealth _enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<Enemy>();
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    private void PlayHurtAnimation()
    {
        _animator.SetTrigger("Hurt");
    }

    private void PlayDieAnimation()
    {
        _animator.SetTrigger("Die");
    }

    private float GetCurrentAnimationLength()
    {
        float animationLength = _animator.GetCurrentAnimatorStateInfo(0).length;
        return animationLength;
    }

    private IEnumerator PlayHurt()
    {
        _enemy.StopMovement();
        PlayHurtAnimation();
        yield return new WaitForSeconds(GetCurrentAnimationLength() + 0.6f);
        _enemy.ResumeMovement();
    }

    private IEnumerator PlayDead()
    {
        _enemy.StopMovement();
        PlayDieAnimation();
        yield return new WaitForSeconds(GetCurrentAnimationLength() + 0.6f);
        _enemy.ResumeMovement();

        _enemyHealth.ResetHealth();
        objectPooler.ReturnToPool(_enemy.gameObject);
    }

    private void EnemyHit(Enemy enemy)
    {
        if(_enemy == enemy)
        {
            StartCoroutine(PlayHurt());
        }
    }

    private void EnemyDead(Enemy enemy)
    {
        if(_enemy == enemy)
        {
            StartCoroutine(PlayDead());
        }
    }

    private void OnEnable()
    {
        EnemyHealth.OnEnemyHit += EnemyHit;
        EnemyHealth.OnEnemyKilled += EnemyDead;
    }
    private void OnDisable()
    {
        EnemyHealth.OnEnemyHit -= EnemyHit;
        EnemyHealth.OnEnemyKilled -= EnemyDead;
    }
}

using DG.Tweening;
using System;
using UnityEngine;

public class CrystalController : MonoBehaviour
{
    private CrystalSkill _skill;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private readonly int _hashExplodeTrigger = Animator.StringToHash("explode");

    private float _lifeTimer;
    private bool _isDestroyed = false;

    private Transform _targetTrm = null;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetUpCrystal(CrystalSkill skill, float timer)
    {
        _skill = skill;
        _lifeTimer = timer;
        _isDestroyed = false;
    }

    private void Update()
    {
        // ���⼭ targetTrm�� null�̸� 
        //_skill.FindClosestEnemy �� �̿��ؼ� ���� �ٰŸ� ���� ã�� 
        // ���� ���� �ƴϸ� �����ӵ��� ���� ���� ���ٰ�
        // ���� �Ÿ��� �������Ϸ� �Ǹ� , EndOfCrystal �� �����ϸ� �ȴ�. return�ϴ°� ���� ����

        if(_skill.chaseToEnemy)
        {
            if(_targetTrm == null)
            {
                _targetTrm = _skill.FindClosestEnemy(transform, _skill.searchingRadius);
            }
            else
            {
                ChaseToTarget();//Ÿ��ã������ ���� ���� ����.
            }
        }

        _lifeTimer -= Time.deltaTime;
        if(_lifeTimer <= 0 && !_isDestroyed)
        {
            EndOfCrystal();
        }
    }

    private void ChaseToTarget(float speedMultiplier = 1f)
    {
        transform.position = Vector2.MoveTowards(
                                transform.position, _targetTrm.position, 
                                _skill.moveSpeed * speedMultiplier * Time.deltaTime);

        if (_isDestroyed) return;

        //�Ÿ��� ����������� ����
        if(Vector2.Distance(transform.position, _targetTrm.position) < 1f)
        {
            EndOfCrystal();
        }
    }

    public void EndOfCrystal()
    {
        _isDestroyed = true;

        if(_skill.canExplode)
        {
            transform.DOScale(Vector3.one * 2.5f, 0.05f);
            _animator.SetTrigger(_hashExplodeTrigger);
        }
        else
        {
            DestroySelf();
        }
    }

    private void EndOfExplosionAnimation()
    {
        transform.DOKill();
        DestroySelf(0.1f);
    }

    private void DestroySelf(float tweenTime = 0.4f)
    {
        _skill.UnlinkCrystal();
        _spriteRenderer.DOFade(0f, tweenTime).OnComplete(() => Destroy(gameObject));
    }
}

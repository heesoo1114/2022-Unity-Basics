using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CloneSkillController : MonoBehaviour
{
    [SerializeField] private int _attackCategoryCount = 3; //1,2,3Ÿ�߿� � �� ������
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private readonly int _attackNumberHash = Animator.StringToHash("AttackNumber");
    private int _facingDirection = 1;

    private CloneSkill _cloneSkill;


    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    public void SetUpClone(CloneSkill cloneSkill, Transform originTrm, Vector3 offset, float cloneDuration, Player player)
    {
        //�ִϸ����Ϳ� 1,2,3Ÿ�� ������ �ִϸ��̼� �����ϰ�
        _animator.SetInteger(_attackNumberHash, Random.Range(0, _attackCategoryCount));
        _cloneSkill = cloneSkill;
        transform.position = originTrm.position + offset;

        FacingClosestTarget();
        FadeAfterDelay(cloneDuration);
    }

    private void FacingClosestTarget()
    {
        Transform closestEnemy = _cloneSkill.FindClosestEnemy(transform, _cloneSkill.findEnemyRadius);
        if(closestEnemy != null)
        {
            if(transform.position.x > closestEnemy.position.x)
            {
                _facingDirection = -1;//�̰� �����൵ ��
                transform.Rotate(0, 180f, 0);
            }
        }
        
    }

    //�����ð��Ŀ� ��������� �����
    private void FadeAfterDelay(float cloneDuration)
    {
        //Ŭ�� �෹�̼��� ������ 0.4�ʵ��� ���̵� �ƿ��Ǹ鼭 �н��� ������� �ϰ�
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(cloneDuration);
        seq.Append(_spriteRenderer.DOFade(0, 0.4f));
        seq.AppendCallback(() => Destroy(gameObject));
    }


    private void AnimationEndTrigger()
    {
        //���⼭ ���� �ؾ߰���?
    }
}

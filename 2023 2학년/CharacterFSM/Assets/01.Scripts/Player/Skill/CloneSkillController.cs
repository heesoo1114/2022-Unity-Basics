using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class CloneSkillController : MonoBehaviour
{
    [SerializeField] private int _attackCategoryCount = 3; // 1, 2, 3Ÿ �߿� � �� �� ������
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

    public void SetUpClone(CloneSkill cloneSkilll, Transform originTrm, Vector3 offset, float cloneDuration, Player player)
    {
        _animator.SetInteger(_attackNumberHash, Random.Range(0, _attackCategoryCount));
        _cloneSkill = cloneSkilll;
        transform.position = originTrm.position + offset;

        FacingClosestTarget();
        FadeAfterDelay(cloneDuration);
    }

    private void FacingClosestTarget()
    {
        
    }
    
    // ���� �ð� �Ŀ� ��������� �����
    private void FadeAfterDelay(float cloneDuration)
    {

    }

    private void AnimationEndTrigger()
    {

    }
}

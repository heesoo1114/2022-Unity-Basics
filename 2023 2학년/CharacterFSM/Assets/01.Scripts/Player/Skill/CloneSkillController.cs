using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CloneSkillController : MonoBehaviour
{
    [SerializeField] private int _attackCategoryCount = 3; //1,2,3타중에 어떤 거 쓸건지
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
        //애니메이터에 1,2,3타중 랜덤한 애니메이션 셋팅하고
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
                _facingDirection = -1;//이건 안해줘도 돼
                transform.Rotate(0, 180f, 0);
            }
        }
        
    }

    //일정시간후에 사라지도록 만들기
    private void FadeAfterDelay(float cloneDuration)
    {
        //클론 듀레이션이 끝나면 0.4초동안 페이드 아웃되면서 분신이 사라지게 하고
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(cloneDuration);
        seq.Append(_spriteRenderer.DOFade(0, 0.4f));
        seq.AppendCallback(() => Destroy(gameObject));
    }


    private void AnimationEndTrigger()
    {
        //여기서 뭔가 해야겠지?
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AttackAIState : CommonAIState
{
    [SerializeField]
    protected float _rotateSpeed = 360f;
    protected Vector3 _targetVector;

    protected bool _isActive = false;

    public override void OnEnterState()
    {
        _enemyController.NavMovement.StopImmediately();
        _enemyController.AgentAnimator.OnAnimationEndTrigger += AttackAnimationEndHandle;
        _enemyController.AgentAnimator.OnAnimationEventTrigger += AttackCollisionHandle;
        _isActive = true;
    }


    public override void OnExitState()
    {
        _enemyController.AgentAnimator.OnAnimationEndTrigger -= AttackAnimationEndHandle;
        _enemyController.AgentAnimator.OnAnimationEventTrigger -= AttackCollisionHandle;

        _enemyController.AgentAnimator.SetAttackState(false);
        _enemyController.AgentAnimator.SetAttackTrigger(false);
        _isActive = false;
    }

    private void AttackAnimationEndHandle()
    {
        //애니메이션이 끝났을 때를 위한 식
        _enemyController.AgentAnimator.SetAttackState(false);
        _aiActionData.IsAttacking = false;
    }

    private void AttackCollisionHandle()
    {
        //적이 공격했을 때 주변을 캐스팅해서 데미지를 주는 식을 넣어야 한다.
        //나중에 컴포넌트화 예정이고
    }

    private void SetTarget()
    {
        _targetVector = _enemyController.TargetTrm.position - transform.position;
        _targetVector.y = 0; //타겟을 바라보는 방향을 구해주는 매서드
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_aiActionData.IsAttacking == false && _isActive)  //액티브
        {
            SetTarget(); //적의 위치를 설정해서 targetVector를 만들어주고 

            // _enemyController.transform.rotation = Quaternion.LookRotation(_targetVector);
            Vector3 currentFrontVector = transform.forward;
            float angle = Vector3.Angle(currentFrontVector, _targetVector);

            if (angle >= 10f)
            {
                // 돌려야 함
                Vector3 result = Vector3.Cross(currentFrontVector, _targetVector);

                float sign = result.y > 0 ? 1 : -1;
                _enemyController.transform.rotation = Quaternion.Euler(0, sign * _rotateSpeed * Time.deltaTime, 0) * _enemyController.transform.rotation;
            }
            else
            {
                _aiActionData.IsAttacking = true;
                _enemyController.AgentAnimator.SetAttackState(true);
                _enemyController.AgentAnimator.SetAttackTrigger(true);
            }
        }
    }

}
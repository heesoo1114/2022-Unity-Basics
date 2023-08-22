using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations;

public class AttackAIState : CommonAIState
{
    protected Vector3 _targetVector;
    protected bool _isActive = false;

    private float _lastAtkTime;

    private EnemyDataSO _dataSO;

    public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);
        _dataSO = _enemyController.EnemyData;
    }

    public override void OnEnterState()
    {
        _enemyController.NavMovement.StopImmediately();
        // _enemyController.AgentAnimator.OnAnimationEndTrigger += AttackAnimationEndHandle;
        // _enemyController.AgentAnimator.OnAnimationEventTrigger += AttackCollisionHandle;
        // _enemyController.AgentAnimator.OnPreAnimationEventTrigger += PreAttackHandle;
        _isActive = true;
    }

    public override void OnExitState()
    {
        // _enemyController.AgentAnimator.OnAnimationEndTrigger -= AttackAnimationEndHandle;
        // _enemyController.AgentAnimator.OnAnimationEventTrigger -= AttackCollisionHandle;
        // _enemyController.AgentAnimator.OnPreAnimationEventTrigger -= PreAttackHandle;
        // 
        // _enemyController.AgentAnimator.SetAttackState(false);
        // _enemyController.AgentAnimator.SetAttackTrigger(false);
        _enemyController.EnemyAttackCompo.CancelAttack(); // 나갈 때 공격하던 거 취소 시키고

        _aiActionData.IsAttacking = false;
        _isActive = false;
    }

    private void PreAttackHandle()
    {
        _enemyController.EnemyAttackCompo.PreAttack();
    }

    private void AttackAnimationEndHandle()
    {
        //애니메이션이 끝났을 때를 위한 식
        _enemyController.AgentAnimator.SetAttackState(false);
        _lastAtkTime = Time.time; // 1초 기다렸다가 다시 레이저 발사
        StartCoroutine(DelayCoroutine(() => _aiActionData.IsAttacking = false, _dataSO.MotionDelay));
    }

    private IEnumerator DelayCoroutine(Action Callback, float time)
    {
        yield return new WaitForSeconds(time);
        Callback?.Invoke();
    }

    private void AttackCollisionHandle()
    {
        //적이 공격했을 때 주변을 캐스팅해서 데미지를 주는 식을 넣어야 한다.
        _enemyController.EnemyAttackCompo.Attack(_dataSO.AtkDamage, _targetVector);
    }

    private void SetTarget()
    {
        _targetVector = _enemyController.TargetTrm.position - transform.position;
        _targetVector.y = 0; //타겟을 바라보는 방향을 구해주는 매서드
    }

    public override bool UpdateState()
    {
        if (base.UpdateState())
        {
            return true;
        }

        if (_aiActionData.IsAttacking == false && _isActive)  //액티브
        {
            SetTarget(); // 적의 위치를 설정해서 targetVector를 만들어주고 

            // _enemyController.transform.rotation = Quaternion.LookRotation(_targetVector);
            Vector3 currentFrontVector = transform.forward;
            float angle = Vector3.Angle(currentFrontVector, _targetVector);

            if (angle >= 10f)
            {
                // 돌려야 함
                Vector3 result = Vector3.Cross(currentFrontVector, _targetVector);

                float sign = result.y > 0 ? 1 : -1;
                _enemyController.transform.rotation = Quaternion.Euler(0, sign * _dataSO.RotateSpeed * Time.deltaTime, 0) * _enemyController.transform.rotation;
            }
            else if (_lastAtkTime + _dataSO.AtkCooltime < Time.time) // 쿨타임도 찼고 각도도 10도로 들어왔다면
            {
                _aiActionData.IsAttacking = true;
                _enemyController.AgentAnimator.SetAttackState(true);
                _enemyController.AgentAnimator.SetAttackTrigger(true);
            }
        }

        return false;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private CommonAIState _currentState;
    public CommonAIState CurrentState => _currentState;

    [SerializeField]
    protected EnemyDataSO _enemyData;
    public EnemyDataSO EnemyData => _enemyData;

    protected EnemyHealth _enemyHealth;
    public EnemyHealth EnemyHealth => _enemyHealth;

    public Transform TargetTrm;

    private NavmeshAgentMovement _navMovement;
    public NavmeshAgentMovement NavMovement => _navMovement;

    private AgentAnimator _agentAnimator;
    public AgentAnimator AgentAnimator => _agentAnimator;

    private CommonAIState _initState;
    private AIActionData _actionData;

    private EnemyAttack _enemyAttack;
    public EnemyAttack EnemyAttackCompo => _enemyAttack;

    private List<AITransition> _anyTransitions = new List<AITransition>();
    public List<AITransition> AnyTransitions => _anyTransitions;

    protected virtual void Awake()
    {
        List<CommonAIState> states = new List<CommonAIState>();
        transform.Find("AI").GetComponentsInChildren<CommonAIState>(states);

        // 각 스테이트에 대한 셋업이 여기서 들어감
        states.ForEach(s => s.SetUp(transform));

        _navMovement = GetComponent<NavmeshAgentMovement>();
        _agentAnimator = transform.Find("Visual").GetComponent<AgentAnimator>();
        _enemyHealth = GetComponent<EnemyHealth>();

        _actionData = transform.Find("AI").GetComponent<AIActionData>();
        _enemyAttack = GetComponent<EnemyAttack>();

        Transform anyTranTrm = transform.Find("AI/AnyTransitions");
        if (anyTranTrm != null)
        {
            anyTranTrm.GetComponentsInChildren<AITransition>(_anyTransitions);
            _anyTransitions.ForEach(t => t.SetUp(transform));
        }

        _initState = _currentState;
    }

    protected virtual void Start()
    {
        _navMovement.SetInitData(_enemyData.MoveSpeed);
        _enemyHealth.SetMaxHP(_enemyData.MaxHP);
    }

    public void ChangeState(CommonAIState nextState)
    {
        // 스테이트 변경
        _currentState?.OnExitState();
        _currentState = nextState;
        _currentState?.OnEnterState();
    }

    private void Update()
    {
        if (_enemyHealth.IsDead) return;
        _currentState?.UpdateState();
    }

    public UnityEvent OnAfterDead = null;

    public void SetDead()
    {
        _navMovement.StopNavigation();
        _agentAnimator.StopAnimator(true);
    }
}
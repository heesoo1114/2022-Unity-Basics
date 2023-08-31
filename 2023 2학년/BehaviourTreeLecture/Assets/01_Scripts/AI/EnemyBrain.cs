using BehaviourTree;
using System.Collections;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBrain : MonoBehaviour
{
    [SerializeField] protected Transform _targetTrm; // 플레이어 타겟

    protected NavMeshAgent _navAgent;
    public NavMeshAgent NavAgent => _navAgent;

    private UIBar _uiBar;
    private Coroutine _coroutine;

    public NodeActionCode currentCode; // 현재 상태를 저장

    protected virtual void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        _uiBar = transform.Find("Status").GetComponent<UIBar>();
    }

    protected virtual void LateUpdate()
    {
        _uiBar.LookToCamera();
    }

    public void TryToTalk(string text, float timer = 1f)
    {
        _uiBar.SetText(text);

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _coroutine = StartCoroutine(PanelFade(timer));
    }

    private IEnumerator PanelFade(float timer)
    {
        _uiBar.IsOn = true;
        yield return new WaitForSeconds(timer);
        _uiBar.IsOn = false;
    }

    public abstract void Attack();
}

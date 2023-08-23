using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBrain : MonoBehaviour
{
    [SerializeField] protected Transform _targetTrm; // 플레이어 타게ㅅ

    protected NavMeshAgent _navAgent;
    public NavMeshAgent NavAgent => _navAgent;

    private UIBar _uiBar;
    private Camera _camera;

    private Coroutine _coroutine;

    protected virtual void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        _uiBar = transform.Find("Status").GetComponent<UIBar>();
        _camera = Camera.main;
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
}

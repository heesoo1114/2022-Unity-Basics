using UnityEngine;

public abstract class AIDecsion : MonoBehaviour
{
    public bool IsReverse = false;

    protected AIActionData _actionData;

    protected virtual void Awake()
    {
        _actionData = GetComponentInParent<AIActionData>();
    }

    public abstract bool MakeDecision();
}

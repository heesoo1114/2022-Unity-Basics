using UnityEngine;

public abstract class AIDecsion : MonoBehaviour
{
    public bool IsReverse = false;

    public abstract bool MakeDecision();
}

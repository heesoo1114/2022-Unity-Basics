using UnityEngine;

[CreateAssetMenu(menuName = "SO/Agent/Move")]
public class AgentMoveSO : ScriptableObject
{
    public float accel = 50f;
    public float deAccel = 50f;
    [Range(0.1f, 10f)]
    public float maxSpeed = 10f;
}

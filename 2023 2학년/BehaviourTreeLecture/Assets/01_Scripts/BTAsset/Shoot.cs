using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;

[Action("MyAction/Shoot")]
[Help("슈팅을 1회 하는 노드 (쿨타임 존재)")]
public class Shoot : ShootOnce
{
    
    [InParam("coolTime", DefaultValue = 1)] public float cooltime;
    public float _lastFiretime;

    public override TaskStatus OnUpdate()
    {
        

        if (Time.time < cooltime + _lastFiretime)
        {
            return TaskStatus.RUNNING;
        }

        base.OnUpdate(); // 실제 사격 수행
        _lastFiretime = Time.time;
        return TaskStatus.RUNNING;
    }
}

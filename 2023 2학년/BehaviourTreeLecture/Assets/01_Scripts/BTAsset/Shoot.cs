using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;

[Action("MyAction/Shoot")]
[Help("������ 1ȸ �ϴ� ��� (��Ÿ�� ����)")]
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

        base.OnUpdate(); // ���� ��� ����
        _lastFiretime = Time.time;
        return TaskStatus.RUNNING;
    }
}

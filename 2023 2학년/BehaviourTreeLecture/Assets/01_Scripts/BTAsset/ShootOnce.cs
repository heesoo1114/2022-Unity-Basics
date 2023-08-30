using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using UnityEditor;
using UnityEngine;

[Action("MyAction/ShootOnce")]
[Help("������ 1ȸ �ϴ� ���")]
public class ShootOnce : BasePrimitiveAction
{
    [InParam("ShootPoint")] public Transform shootPoint;
    [InParam("Prefab")] public Bullet bullet;
    [InParam("Clip")] public Clip clip;

    [InParam("Velocity", DefaultValue = 30f)] public float velocity;
    [InParam("Body")] public Transform body;
    // [InParam("Target")] public GameObject target;

    public override TaskStatus OnUpdate()
    {
        // Vector3 dir = target.transform.position - body.position;
        // dir.y = 0;
        // body.rotation = Quaternion.LookRotation(dir.normalized);

        if (clip.IsEmpty)
        {
            return TaskStatus.FAILED;
        }

        var newBullet = GameObject.Instantiate(bullet, shootPoint.position, Quaternion.identity);
        newBullet.Fire(shootPoint.forward * velocity);
        clip.Ammo--;

        return TaskStatus.COMPLETED;
    }
}

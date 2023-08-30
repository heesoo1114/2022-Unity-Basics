using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using UnityEngine;

[Action("MyAction/Reload")]
public class ReloadClip : BasePrimitiveAction
{
    [InParam("Clip")] public Clip clip;
    [InParam("ReloadTime")] public float reloadTime = 1f;

    private float _currentTIme;

    public override void OnStart()
    {
        _currentTIme = 0;
    }

    public override TaskStatus OnUpdate()
    {
        _currentTIme += Time.deltaTime;
        if (_currentTIme >= reloadTime)
        {
            clip.Reload();
            return TaskStatus.COMPLETED;
        }

        return TaskStatus.RUNNING;
    }

}

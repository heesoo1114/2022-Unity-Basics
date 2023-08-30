using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyCondition/IsEmptyClip")]
[Help("플레이어 탄창을 체크해서 비어있으면 true, 그렇지 않으면 false")]
public class IsEmptyBullet : ConditionBase
{
    [InParam("Clip")] public Clip clip;

    public override bool Check()
    {
        return clip.IsEmpty;
    }
}

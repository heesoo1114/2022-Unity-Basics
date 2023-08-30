using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyCondition/IsEmptyClip")]
[Help("�÷��̾� źâ�� üũ�ؼ� ��������� true, �׷��� ������ false")]
public class IsEmptyBullet : ConditionBase
{
    [InParam("Clip")] public Clip clip;

    public override bool Check()
    {
        return clip.IsEmpty;
    }
}

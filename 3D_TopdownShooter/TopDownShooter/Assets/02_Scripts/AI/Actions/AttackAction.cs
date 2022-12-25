using UnityEngine;

public class AttackAction : AIAction
{
    public override void TakeAction()
    {
        _aiMovementData.direction = Vector2.zero;
        if(_aiActionData.isAttack == false)
        {
            _brain.Attack(); // ���� ����
            _aiMovementData.pointOfInterest = _brain.Target.position;
        }
        _brain.Move(_aiMovementData.direction, _aiMovementData.pointOfInterest);
    }
}

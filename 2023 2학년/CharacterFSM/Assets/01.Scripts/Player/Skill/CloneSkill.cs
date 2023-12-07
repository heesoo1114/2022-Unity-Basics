using UnityEngine;

public class CloneSkill : Skill
{
    [Header("Clone_Info")]
    [SerializeField] private CloneSkillController _clonePrefab;
    [SerializeField] private float _cloneDuration;

    [SerializeField] private bool _createCloneOnDashStart;
    [SerializeField] private bool _createCloneOnDashOver;
    // [SerializeField] private bool _createCloneOnCounterAttack;

    [Header("Duplicate clone")]
    public bool canDuplicate; // 복제가능여부
    public float duplicatePercent; // 복제 확률

    [Header("Crystal instead of clone")]
    [SerializeField] private bool _crystalInsteadOfClone;

    public float findEnemyRadius = 5f;

    public void CreateClone(Transform originTrm, Vector3 offset = new Vector3())
    {
        if (_crystalInsteadOfClone)
        {
            return;
        }

        CloneSkillController newClone = Instantiate(_clonePrefab);
        newClone.SetUpClone(this, originTrm, offset, _cloneDuration, _player);
    }

}

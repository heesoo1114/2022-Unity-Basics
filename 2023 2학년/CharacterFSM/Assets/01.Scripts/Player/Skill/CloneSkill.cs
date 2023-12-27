using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkill : Skill, ISaveManager
{
    [Header("Clone_info")]
    [SerializeField] private CloneSkillController _clonePrefab;
    [SerializeField] private float _cloneDuration;

    [SerializeField] private bool _createCloneOnDashStart;
    [SerializeField] private bool _createCloneOnDashOver;
    //[SerializeField] private bool _createCloneOnCounterAttack;

    [Header("Duplicate clone")]
    public bool canDuplicate; //복제가능여부
    public float duplicatePercent; //복제 확률

    [Header("Crystal instead of clone")]
    [SerializeField] private bool _crystalInsteadOfClone;

    public float findEnemyRadius = 5f;

    
    public void CreateClone(Transform originTrm, Vector3 offset)
    {
        if(_crystalInsteadOfClone)
        {
            //여기선 나중에 처리
            return;
        }

        CloneSkillController newClone = Instantiate(_clonePrefab);
        newClone.SetUpClone(this, originTrm, offset, _cloneDuration, _player);
    }

    public void CreateCloneOnDashStart(Transform origin, Vector3 offset)
    {
        if (_createCloneOnDashStart)
        {
            CreateClone(origin, offset);
        }
    }

    public void CreateCloneOnDashOver(Transform origin, Vector3 offset)
    {
        if (_createCloneOnDashOver)
        {
            CreateClone(origin, offset);
        }
    }

    // clone_enable, clone_start, clone_end
    public void LoadData(GameData data)
    {
        if(data.skillTree.TryGetValue("clone_enable", out int value))
        {
            skillEnabled = value == 1;
        }

        if (data.skillTree.TryGetValue("clone_start", out int startValue))
        {
            _createCloneOnDashStart = startValue == 1;
        }

        if (data.skillTree.TryGetValue("clone_end", out int endValue))
        {
            _createCloneOnDashOver = endValue == 1;
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.skillTree.TryGetValue("clone_enable", out int value))
        {
            data.skillTree["clone_enable"] = skillEnabled ? 1 : 0;
        }
        else
        {
            data.skillTree.Add("clone_enable", skillEnabled ? 1 : 0);
        }


        if (data.skillTree.TryGetValue("clone_start", out int startValue))
        {
            data.skillTree["clone_start"] = _createCloneOnDashStart ? 1 : 0;
        }else
        {
            data.skillTree.Add("clone_start", _createCloneOnDashStart ? 1 : 0);
        }


        if (data.skillTree.TryGetValue("clone_end", out int endValue))
        {
            data.skillTree["clone_end"] = _createCloneOnDashOver ? 1 : 0;
        }
        else
        {
            data.skillTree.Add("clone_end", _createCloneOnDashOver ? 1 : 0);
        }
    }
}

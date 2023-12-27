using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSkill : Skill
{
    [SerializeField] private CrystalController _crystalPrefab;
    [SerializeField] private float _timeOut = 4f;


    private CrystalController _currentCrystal;


    public bool canExplode;
    public bool chaseToEnemy; //이게 참이면 가장 가까운 적을 향해서 계속해서 이동한다.

    [Header("skill settings")]
    public float searchingRadius = 7f;
    public float moveSpeed = 4f;

    public void UnlinkCrystal()
    {
        _cooldownTimer = _cooldown; //언링크 하면 넣어준다.
        _currentCrystal = null;
    }

    public override bool AttemptUseSkill()
    {
        if (_cooldownTimer <= 0 && skillEnabled && _currentCrystal == null)
        {
            UseSkill();
            return true;
        }

        if(_currentCrystal != null)
        {
            WarpToCrystalPosition();
            //여기선 나중에 크리스탈로 점프하던가 다른 뭔가 기능을 줘야 한다.
            //플레이와 크리스탈의 위치를 교체한다.
            //삭제하고 쿨타임이 돌기 시작하면 된다.
        }

        Debug.Log("Skill Cooldown or locked");
        return false;
    }

    private void WarpToCrystalPosition()
    {
        Vector2 playerPos = _player.transform.position;
        _player.transform.position = _currentCrystal.transform.position;
        _currentCrystal.transform.position = playerPos;

        _currentCrystal.EndOfCrystal();
        //(_player.transform.position, _currentCrystal.transform.position) 
        //    = (_currentCrystal.transform.position, _player.transform.position);
    }

    public override void UseSkill()
    {
        base.UseSkill();

        if(_currentCrystal == null)
        {
            CreateCrystal(_player.transform.position);
        }
    }

    private void CreateCrystal(Vector3 position)
    {
        _currentCrystal = Instantiate(_crystalPrefab, position, Quaternion.identity);
        _currentCrystal.SetUpCrystal(this, _timeOut);
    }
}

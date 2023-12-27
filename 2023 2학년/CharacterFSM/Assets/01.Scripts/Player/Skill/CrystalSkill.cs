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
    public bool chaseToEnemy; //�̰� ���̸� ���� ����� ���� ���ؼ� ����ؼ� �̵��Ѵ�.

    [Header("skill settings")]
    public float searchingRadius = 7f;
    public float moveSpeed = 4f;

    public void UnlinkCrystal()
    {
        _cooldownTimer = _cooldown; //��ũ �ϸ� �־��ش�.
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
            //���⼱ ���߿� ũ����Ż�� �����ϴ��� �ٸ� ���� ����� ��� �Ѵ�.
            //�÷��̿� ũ����Ż�� ��ġ�� ��ü�Ѵ�.
            //�����ϰ� ��Ÿ���� ���� �����ϸ� �ȴ�.
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

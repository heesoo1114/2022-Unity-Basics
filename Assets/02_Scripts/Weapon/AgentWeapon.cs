using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class AgentWeapon : MonoBehaviour
{
    protected Weapon _weapon; // �ڱⰡ ��� �ִ� weapon
    protected WeaponRenderer _weaponRenderer;
    protected float _deireAngle; // ���Ⱑ �ٶ󺸰��� �ϴ� ����

    [SerializeField]
    private int _maxTotalAmmo = 2000, _totalAmmo = 200; // �ִ� 200 / 2000�� ������ �ְ�
    protected bool _isReloading = false;
    public bool IsReloading { get => _isReloading; }
    private ReloadGaugeUI _reloadUI;

    protected virtual void Awake()
    {
        _reloadUI = transform.parent.Find("ReloadBar").GetComponent<ReloadGaugeUI>();
        AssignWeapon();
    }

    public virtual void AssignWeapon()
    {
        _weaponRenderer = GetComponentInChildren<WeaponRenderer>();
        _weapon = GetComponentInChildren<Weapon>();
    }

    public virtual void AimWeapon(Vector2 pointerPos)
    {
        Vector3 aimDirection = (Vector3)pointerPos - transform.position;
        _deireAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        AdjustWeaponRendering(); // ���⸦ ������

        transform.rotation = Quaternion.AngleAxis(_deireAngle, Vector3.forward);
    }

    private void AdjustWeaponRendering()
    {
        _weaponRenderer.FlipSprite(_deireAngle > 90f || _deireAngle < -90f);
        _weaponRenderer.RenderBehindHead(_deireAngle > 0 && _deireAngle < 180f);
    }

    public virtual void Shoot()
    {
        if(_isReloading == true)
        {
            _weapon.PlayCannotSound(); // źȯ���� ���� ���
            return;
        }
        _weapon.TryShooting();
    }

    public virtual void StopShooting()
    {
        _weapon.StopShooting();
    }

    public void ReloadGun()
    {
        if (_isReloading == false && _totalAmmo > 0 && _weapon.AmmoFull == false)
        {
            _isReloading = true;
            _weapon.StopShooting(); // �������ÿ� �ѽ�°� �����ְ�
            StartCoroutine(ReloadCoroutin());
        }
        else
        {
            _weapon.PlayCannotSound(); // źȯ���� ���� ���
        }
    }

    IEnumerator ReloadCoroutin()
    {
        _reloadUI.gameObject.SetActive(true);
        // yield return new WaitForSeconds(_weapon.WeaponData.reloadTime);
        float time = 0;
        while(time <= _weapon.WeaponData.reloadTime)
        {
            _reloadUI.ReloadGaugeNormal(time / _weapon.WeaponData.reloadTime);
            time += Time.deltaTime;
            yield return null;
        }


        _reloadUI.gameObject.SetActive(false);
        _weapon.PlayReloadSound();
        // ���� ���� źȯ�� 20�߸� �Ѿ˿� 30��
        int reloadAmmo = Mathf.Min(_totalAmmo, _weapon.EmptyBulletCnt);
        _totalAmmo -= reloadAmmo;
        _weapon.Ammo += reloadAmmo;

        _isReloading = false;
    }
    
}

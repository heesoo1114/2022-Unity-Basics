using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    // �߻� ���� ����
    public UnityEvent OnShoot;
    public UnityEvent OnShootNoAmmo;
    public UnityEvent OnStopShooting;
    protected bool _isShooting = false;
    protected bool _delayCorutine = false;


    [SerializeField] protected WeaponDataSO _weaponData;
    [SerializeField] protected GameObject _muzzle; // �ѱ� ��ġ
    [SerializeField] protected TrackedReference _shellEjectPos; // ź�� ��������

    public WeaponDataSO WeaponData { get => _weaponData;  }

    // AMMO ���� �ڵ�
    public UnityEvent<int> OnAmmoChange; // �Ѿ� ����� �߻��� �̺�Ʈ
    [SerializeField] protected int _ammo; // ���� �Ѿ� ��

    public int Ammo
    {
        get => _ammo;
        set
        {
            _ammo = Mathf.Clamp(value, 0, _weaponData.ammoCapacity);
            OnAmmoChange?.Invoke(_ammo);
        }
    }

    public bool AmmoFull { get => Ammo == _weaponData.ammoCapacity; }
    public int  EmptyBulletCnt { get => _weaponData.ammoCapacity - _ammo; }

    private void Start()
    {
        // ���߿� ���� ����
        Ammo = _weaponData.ammoCapacity;
        WeaponAudio wa = transform.Find("WeaponAudio").GetComponent<WeaponAudio>();
        wa.SetAudioClip(_weaponData.shootClip,
                        _weaponData.outOfAmmoClip,
                        _weaponData.reloadClip);
    }

    void Update()
    {
        UseWeapon();
    }

    private void UseWeapon()
    {
        // ���콺 Ŭ�����̰�, ���� �����̰� false�̸� �߻�
        if(_isShooting && _delayCorutine == false)
        {
            if(Ammo > 0)
            {
                Ammo -= _weaponData.GetBulletCountToSpawn();

                OnShoot?.Invoke();
                for(int i = 0; i < _weaponData.GetBulletCountToSpawn(); i++)
                {
                    ShootBullet();
                }
            }
            else
            {
                _isShooting = false;
                OnShootNoAmmo?.Invoke();
                return;
            }
        }

    }

    protected void FinishShooting()
    {
        StartCoroutine(DelayNextShootCoroutine());
        if(_weaponData.automaticFire == false)
        {
            _isShooting = false;
        }
    }

    protected IEnumerator DelayNextShootCoroutine()
    {
        _delayCorutine = true;
        yield return new WaitForSeconds(_weaponData.weaponDelay);
        _delayCorutine = false;
    }

    private void ShootBullet()
    {
        Debug.Log("���߻���");
    }

    public void TryShooting()
    {
        _isShooting = true;
    }
    public void StopShooting()
    {
        _isShooting = false;
        OnStopShooting?.Invoke();
    }
}

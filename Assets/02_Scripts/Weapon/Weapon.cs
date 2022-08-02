using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    // 발사 관련 로직
    public UnityEvent OnShoot;
    public UnityEvent OnShootNoAmmo;
    public UnityEvent OnStopShooting;
    protected bool _isShooting = false;
    protected bool _delayCorutine = false;


    [SerializeField] protected WeaponDataSO _weaponData;
    [SerializeField] protected GameObject _muzzle; // 총구 위치
    [SerializeField] protected TrackedReference _shellEjectPos; // 탄피 생성지점

    public WeaponDataSO WeaponData { get => _weaponData;  }

    // AMMO 관련 코드
    public UnityEvent<int> OnAmmoChange; // 총알 변경시 발생할 이벤트
    [SerializeField] protected int _ammo; // 현재 총알 수

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
        // 나중에 변경 예정
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
        // 마우스 클릭중이고, 총의 딜레이가 false이면 발사
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
        Debug.Log("빵야빵야");
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/WEAPON/WeaponData")]
public class WeaponDataSO : ScriptableObject
{
    public BulletDataSO bulletData;

    [Range(0, 999)] public int ammoCapacity = 100; // 탄창 크기
    public bool automaticFire; // 연사 모드 가능
    [Range(0.1f, 2f)] public float weaponDelay = 0.1f; // 사격지연 시간
    [Range(0, 10f)] public float spreadAngle = 5f; // 탄이 퍼지는 정도

    [SerializeField] private bool _multiBulletShot = false; // 한번클릭에 여러발 발사
    [SerializeField] private int _bulleetCount = 1;

    [Range(0.1f, 2f)] public float reloadTime = 1f; // 재장전 시간

    public AudioClip shootClip;
    public AudioClip outOfAmmoClip;
    public AudioClip reloadClip;

    public int GetBulletCountToSpawn()
    {
        return _multiBulletShot ? _bulleetCount : 1;
    }
}

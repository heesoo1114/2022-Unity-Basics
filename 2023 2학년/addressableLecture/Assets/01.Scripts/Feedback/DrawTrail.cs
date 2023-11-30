using UnityEngine;
using UnityEngine.AddressableAssets;

public class DrawTrail : Feedback
{
    [SerializeField] private Transform _firePos;
    [SerializeField] private AssetReference _prefab;
    [SerializeField] private UziGun _playerGun;
    [SerializeField] private float _trailTime = 0.01f;
    public override void CreateFeedback()
    {
        var trail = PoolManager.Instance.Pop(_prefab) as BulletTrail;
        trail.DrawTrail(_firePos.position, _playerGun.HitPoint, _trailTime);
    }

    public override void CompletePrevFeedback()
    {

    }
}
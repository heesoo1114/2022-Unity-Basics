using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    protected float _timeToLive;

    protected int _enemyLayer;
    protected int _obstacleLayer;

    protected bool _isDead = false;

    [SerializeField]
    protected BulletDataSO _bulletData;
    public BulletDataSO BulletData
    {
        get => _bulletData;
        set => _bulletData = value;
    }

    protected bool _isEnemy;
    public bool IsEnemy
    {
        get => _isEnemy;
        set => _isEnemy = value;
    }

    private void Awake()
    {
        _obstacleLayer = LayerMask.NameToLayer("Obstacle"); // 장애물 레이어의 번호를 알아오고
        _enemyLayer = LayerMask.NameToLayer("Enemy");
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetPositionAndRotation(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);
    }

    private void FixedUpdate()
    {
        _timeToLive += Time.fixedDeltaTime;
        _rigidbody.MovePosition(transform.position + _bulletData.bulletSpeed * transform.right * Time.fixedDeltaTime);

        if(_timeToLive >= _bulletData.lifeTime)
        {
            _isDead = true;
            Destroy(gameObject); // 나중엔 풀매니징으로 변경 예정
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isDead == true) return;

        IHitable hitable = collision.GetComponent<IHitable>();

        if(hitable != null && hitable.IsEnemy == IsEnemy)
        {
            return;
        }

        hitable?.GetHit(_bulletData.damage, gameObject);
        
        if (collision.gameObject.layer == _obstacleLayer)
            HitObstacle(collision);
        if(collision.gameObject.layer == _enemyLayer)
            HitEnemy(collision);

        _isDead = true;
        Destroy(gameObject);
    }

    private void HitEnemy(Collider2D col)
    {
        Vector2 randomOffset = Random.insideUnitCircle * 0.5f;
        Impact impact = Instantiate(_bulletData.impactEnemyPrefab).GetComponent<Impact>();
        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360f)));
        impact.SetPositionAndRotation(col.transform.position + (Vector3)randomOffset, rot);
        impact.SetScaleAndTime(Vector3.one * 0.7f, 0.2f);
    }

    private void HitObstacle(Collider2D col)
    {
        // 실질적인 데미치 처리는 여기서 이루어짐 (아직 안 함)
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 10f);

        if (hit.collider != null)
        {
            Impact impact = Instantiate(_bulletData.impactObstaclePrefab).GetComponent<Impact>();
            Quaternion rot = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360f)));
            impact.SetPositionAndRotation(hit.point + (Vector2)transform.right * 0.5f , rot);
            impact.SetScaleAndTime(Vector3.one, 0.2f);
        }

        
    }
}

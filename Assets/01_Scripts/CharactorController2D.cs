using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalType;

public class CharactorController2D : MonoBehaviour
{
    public float raycastDistance = 0.2f;
    public LayerMask layerMask;
    public float slopAngleLimit = 45f;

    // flags
    public bool below;
    public GroundType groundType;

    // 나중에 private으로 변경
    public Vector2 _slopNormal;
    public float _slopAngle;

    private Vector2 _moveAmount;
    private Vector2 _currentPosition;
    private Vector2 _lastPosition;

    private Rigidbody2D _rigidbody;
    private CapsuleCollider2D _capsuleCollider;

    private bool _didsbleGroundCheck;

    private Vector2[] _raycastPosition = new Vector2[3];
    private RaycastHit2D[] _raycastHits = new RaycastHit2D[3];

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void FixedUpdate()
    {
        _lastPosition = _rigidbody.position;

        if(_slopAngle != 0 && below)
        {
            if ((_moveAmount.x > 0f && _slopAngle > 0) || (_moveAmount.x < 0f && _slopAngle < 0f))
            {
                    _moveAmount.y = -Mathf.Abs(Mathf.Tan(_slopAngle * Mathf.Deg2Rad) * _moveAmount.x);
            }
        }

        _currentPosition = _lastPosition + _moveAmount;

        _rigidbody.MovePosition(_currentPosition);

        _moveAmount = Vector2.zero;

        if (!_didsbleGroundCheck) CheckGrounded();
    }

    public void Move(Vector2 movement)
    {
        _moveAmount += movement;
    }

    private void CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.CapsuleCast(_capsuleCollider.bounds.center, _capsuleCollider.size, 
            CapsuleDirection2D.Vertical, 0f, Vector2.down, raycastDistance);

        if(hit.collider)
        {
            groundType = DetermineGroundType(hit.collider);
            _slopNormal = hit.normal;
            _slopAngle = Vector2.SignedAngle(_slopNormal, Vector2.up); // 경사 각도

            if (_slopAngle > slopAngleLimit || _slopAngle < -slopAngleLimit)
            {
                below = false;
            }
            else
            {
                below = true;
            }
        }
        else
        {
            below = false;
            groundType = GroundType.none;
        }
    }

    private GroundType DetermineGroundType(Collider2D collider)
    {
        if(collider.GetComponent<GroundEffector>())
        {
            GroundEffector groundEffector = collider.GetComponent<GroundEffector>();
            return groundEffector.groundType;
        }
        else
        {
            return GroundType.Level;
        }
    }

    private void DrawDebugRays(Vector2 direction, Color color)
    {
        for(int  i = 0; i < _raycastPosition.Length; i++)
        {
            Debug.DrawRay(_raycastPosition[i], direction * raycastDistance, color);
        }
    }

    public void DisableGroundCheck(float delayTime)
    {
        below = false;
        _didsbleGroundCheck = true;
        StartCoroutine("EnableGroundCheck", delayTime);
    }

    IEnumerator EnableGroundCheck(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        _didsbleGroundCheck = false;
    }
}

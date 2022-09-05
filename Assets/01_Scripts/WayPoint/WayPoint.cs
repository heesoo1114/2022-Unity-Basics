using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField]
    private Vector3[] points;

    // editor에서 사용하기 위해 public 선언
    public Vector3[] Points => points;

    private Vector3 _currentPosition;
    public Vector3 CurrentPosition => _currentPosition;
    private bool _gameStarted;

    private void Start()
    {
        _gameStarted = true;
        _currentPosition = transform.position;
    }

    private void OnDrawGizmos()
    {
        if(!_gameStarted && transform.hasChanged)
        {
            _currentPosition = transform.position;
        }

        for(int i = 0; i < points.Length; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(points[i] + _currentPosition, radius: 0.5f);

            if(i < points.Length - 1)
            {
                Gizmos.color = Color.gray;
                Gizmos.DrawLine(points[i] + _currentPosition, points[i + 1] + _currentPosition);
            }
        }
    }
}

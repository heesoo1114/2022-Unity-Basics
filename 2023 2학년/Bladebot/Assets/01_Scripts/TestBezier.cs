using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;

public class TestBezier : MonoBehaviour
{
    LineRenderer _lineRenderer;

    [SerializeField]
    private Transform _startTrm, _startCtrlTrm, _endTrm, _endCtrlTrm;

    private Vector3[] _points;

    private void Awake()
    {
        _lineRenderer = transform.Find("Line"). GetComponent<LineRenderer>();
    }

    
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _points = DOCurve.CubicBezier.GetSegmentPointCloud(_startTrm.position, _startCtrlTrm.position, _endTrm.position, _endCtrlTrm.position, 30);

            _lineRenderer.positionCount = _points.Length;
            _lineRenderer.SetPositions(_points);

            StartCoroutine(MoveCube());
        }
    }

    private IEnumerator MoveCube()
    {
        float delayTime = 2.0f / _points.Length;

        for (int i = 0; i < _points.Length; i++)
        {
            yield return new WaitForSeconds(delayTime);
            transform.position = _points[i];
        }
    }
}

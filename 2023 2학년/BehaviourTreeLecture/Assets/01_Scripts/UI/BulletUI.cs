using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletUI : MonoBehaviour
{
    private int _currentCnt;
    private int _maxCount;
    private List<VisualElement> _bulletList;

    public int BulletCount
    {
        get => _currentCnt;
        set
        {
            _currentCnt = Mathf.Clamp(value, 0, _maxCount);
            DrawBullet();
        }
    }

    public BulletUI(VisualElement root, int maxCount)
    {
        // 모든 불렛 클래스를 가져와서 리스트로 만들어줌
        _bulletList = root.Query(className: "bullet").ToList();
    }

    private void DrawBullet()
    {
        for (int i = 0; i < _bulletList.Count; i++)
        {
            if (i < _currentCnt - 1)
            {
                _bulletList[i].visible = true;
            }
            else
            {
                _bulletList[i].visible = false;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBlock : MonoBehaviour
{
    [SerializeField]
    private RoadBlock _roadPrefab;

    private RoadBlock _roadBlock = null;

    public void ClickBaseBlock()
    {
        if (_roadBlock == null)
            _roadBlock = Instantiate(_roadPrefab, transform);
        else
            Destroy(_roadBlock.gameObject);
    }

}

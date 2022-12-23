using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    [SerializeField]
    private List<ResourceDataSO> _dropTable;
    private float[] _itemWeights; //?????? ??????

    [SerializeField]
    private float _dropPower = 2f;
    [SerializeField]
    [Range(0, 1)]
    private float _dropChance; //??????


    void Start()
    {
        _itemWeights = _dropTable.Select(item => item.rate).ToArray();
    }

    public void Drop()
    {
        float dropVariable = Random.value; //0~1
        if (dropVariable < _dropChance) //????????? ???
        {
            int index = GetRandomItemIndex();
            Resource r = PoolManager.Instance.Pop(_dropTable[index].itemPrefab.name) as Resource ;
            r.transform.position = transform.position;

            Vector3 offset = Random.insideUnitCircle;

            r.transform.DOJump(transform.position + offset, _dropPower, 1, 0.4f);
        }
    }

    private int GetRandomItemIndex()
    {
        float sum = 0f;
        for (int i = 0; i < _itemWeights.Length; i++)
        {
            sum += _itemWeights[i];
        }
        // 0.5, 0.2  => 0.7      0.6
        float randomValue = Random.Range(0, sum);
        float tempSum = 0f;

        for (int i = 0; i < _itemWeights.Length; i++)
        {
            if (randomValue >= tempSum && randomValue < tempSum + _itemWeights[i])
            {
                return i;
            }
            else
            {
                tempSum += _itemWeights[i];
            }
        }
        return 0;
    }
}

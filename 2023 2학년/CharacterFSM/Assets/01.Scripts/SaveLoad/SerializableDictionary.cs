using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<TKey> keys = new List<TKey>();
    [SerializeField] private List<TValue> values = new List<TValue>();


    /// <summary>
    /// �����ϱ��� ���� ����� �ϴ� ��
    /// </summary>
    public void OnBeforeSerialize()
    {
        keys.Clear();    
        values.Clear();

        foreach(var pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    /// <summary>
    /// �ҷ��µڿ� ���� ��
    /// </summary>
    public void OnAfterDeserialize()
    {
        this.Clear();
        if(keys.Count != values.Count)
        {
            Debug.LogError("Key count does not match to value count");
        }
        else
        {
            for(int i = 0; i < keys.Count; ++i)
            {
                this.Add(keys[i], values[i]);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<TKey> keys = new List<TKey>();
    [SerializeField] private List<TValue> values = new List<TValue>();


    /// <summary>
    /// 저장하기전 전에 해줘야 하는 일
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
    /// 불러온뒤에 해줄 일
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

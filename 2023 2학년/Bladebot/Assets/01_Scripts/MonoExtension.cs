using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MonoExtension 
{
    public static CoroutineHandle RunCoroutine(this MonoBehaviour owner, IEnumerator co)
    {
        return new CoroutineHandle(owner, co);
    }
}

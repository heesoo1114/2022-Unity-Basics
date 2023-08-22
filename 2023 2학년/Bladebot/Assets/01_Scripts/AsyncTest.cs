using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

// public class Test : IEnumerable
// {
//     private int[] _arr = { 1, 2, 3, 4, 5 };
//     private int _index;
// 
//     public IEnumerator GetEnumerator()
//     {
//         _index = 0;
//         while (_index < _arr.Length)
//         {
//             yield return _arr[_index];
//             _index++;
//         }
//     }
// }]

public class CoroutineHandle : IEnumerator
{
    public bool IsDone { get; private set; }
    public object Current { get; }

    public bool MoveNext()
    {
        return !IsDone;
    }

    public void Reset()
    {

    }

    public CoroutineHandle(MonoBehaviour owner, IEnumerator coroutine)
    {
        Current = owner.StartCoroutine(Wrap(coroutine));
    }

    private IEnumerator Wrap(IEnumerator coroutine)
    {
        yield return coroutine;
        IsDone = true;
    }
}

public class AsyncTest : MonoBehaviour
{
    private void Start()
    {
        Debug.Log(Thread.CurrentThread.Name);
        if (Thread.CurrentThread.Name == null)
        {
            Thread.CurrentThread.Name = "MainThread";
        }
        Debug.Log(Thread.CurrentThread.Name);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _num = 0;
            Task.Run(() => Inc());
            Task.Run(() => Dec());
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("I'm Alive");
        }
    }

    private int _num = 0;
    private object obj = new object();

    private void Inc()
    {
        for (int i = 0; i < 9999999; i++)
        {
            lock(obj)
            {
                _num++;
            }
        }
    }

    private void Dec()
    {
        for (int i = 0; i < 9999999; i++)
        {
            lock(obj)
            {
                _num--;
            }
        }
    }

    private async void StartJob()
    {
        _num = 0;
        var t1 = Task.Run(() => Inc());
        var t2 = Task.Run(() => Dec());

        await Task.WhenAll(new[] { t1, t2 });
        Debug.Log(_num);
    }

    private void MyJob()
    {
        Thread.Sleep(3000);
        Debug.Log("Job Complete");
        Debug.Log($"{Thread.CurrentThread.Name} : {Thread.CurrentThread.ManagedThreadId}");
    }

    // private IEnumerator Start()
    // {
    //     // Debug.Log(Time.time);
    //     // 
    //     // var task1 = StartCoroutine(CoA());
    //     // var task2 = StartCoroutine(CoB());
    //     // 
    //     // yield return task2; CoB의 딜레이만 받음
    //     // 
    //     // Debug.Log(Time.time);
    //     // Debug.Log("Job Complete");
    // 
    //     var handle1 = this.RunCoroutine(CoA());
    //     var handle2 = this.RunCoroutine(CoB());
    // 
    //     while (!handle1.IsDone && !handle2.IsDone)
    //     {
    //         yield return null;
    //     }
    // 
    //     Debug.Log("Complete");
    // }
    // 
    // private IEnumerator CoA()
    // {
    //     yield return new WaitForSeconds(1f);
    //     Debug.Log("A Complete");
    // }
    // 
    // private IEnumerator CoB()
    // {
    //     yield return new WaitForSeconds(3f);
    //     Debug.Log("B Complete");
    // }
}

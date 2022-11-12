using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue<T> where T : IComparable<T>
{
    public List<T> _heap = new List<T>();

    public int Count => _heap.Count;

    public T Contains(T t)
    {
        int idx = _heap.IndexOf(t);
        if (idx < 0) return default(T);
        return _heap[idx];
    }

    public void Push(T data)
    {
        _heap.Add(data); // ����Ʈ�� �� ���� �ִ´�.
        int now = _heap.Count - 1; // �� ������ ������ �ε����� �˾Ƴ���  �ִ´�.

        while (now > 0)
        {
            int next = (now - 1) / 2; // �θ� ��������
            if (_heap[now].CompareTo(_heap[next]) > 0)
            {
                break;
            }

            // ������� �Դٴ� ���� �ٸ� �ְ� ������ ũ�ٴ� ��
            T temp = _heap[now];
            _heap[now] = _heap[next];
            _heap[next] = temp;

            now = next;
        }
    }

    public T Pop()
    {
        T ret = _heap[0];

        int lastIndex = _heap.Count - 1;
        _heap[0] = _heap[lastIndex];
        _heap.RemoveAt(lastIndex); // ����� �������� ���ִ� �� ����Ʈ ���縦 ����Ų��
        lastIndex--;

        int now = 0;

        while (true)
        {
            int left = 2 * now + 1; // ���� �ڼհ�
            int right = 2 * now + 2; // ������ �ڼ��� �ε����� ���ϰ�

            int next = now;

            // �����̳� �������̳� �˻縦 �ؾ� �ϴµ�
            if (left <= lastIndex && _heap[next].CompareTo(_heap[left]) < 0)
            {
                next = left;
            }
            if (right <= lastIndex && _heap[next].CompareTo(_heap[right]) < 0)
            {
                next = right;
            }

            // �˻縦 ������
            if (next == now)
            {
                break; // ���ڸ��� ã��
            }

            // ������ �Ͼ�ٸ� next�� now �� ��ü
            T temp = _heap[now];
            _heap[now] = _heap[next];
            _heap[next] = temp;

            now = next;
        }

        return default(T);
    }

    public T Peek()
    {
        return _heap.Count == 0 ? default(T) : _heap[0];
    }

    public void Clear()
    {
        _heap.Clear(); // �� �����
    }
}

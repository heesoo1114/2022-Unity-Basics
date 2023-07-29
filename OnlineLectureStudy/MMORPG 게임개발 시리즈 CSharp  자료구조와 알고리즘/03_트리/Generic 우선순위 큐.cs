using System;
using System.Collections.Generic;

namespace MMORPG_게임개발_시리즈_CSharp__자료구조와_알고리즘
{
    class PriorityQueue<T> where T : IComparable<T>
    {
        List<T> _heap = new List<T>();

        // 0(logN)
        public void Push(T data)
        {
            // 힙의 맨 끝에 새로운 데이터 삽입
            _heap.Add(data);

            // 위로 올라가는 과정 (도장깨기)
            int now = _heap.Count - 1;
            while (now > 0)
            {
                // 부모 노드 구하기
                int next = (now - 1) / 2;

                // 부모가 자식보다 크면
                if (_heap[now].CompareTo(_heap[next]) < 0)
                {
                    break;
                }

                // 두 값 교체
                T temp = _heap[now];
                _heap[now] = _heap[next];
                _heap[next] = temp;

                // 검사 위치 이동 (위로 이동했으면 위로 가서 검사하게)
                now = next;
            }
        }

        // 0(logN)
        public T Pop()
        {
            // 반환할 데이터를 따로 저장
            T ret = _heap[0];

            // 마지막 데이터를 루트로 이동
            int lastIndex = _heap.Count - 1;
            _heap[0] = _heap[lastIndex];
            _heap.RemoveAt(lastIndex);
            lastIndex--;

            // 역으로 내려가는 도장깨기 시작
            int now = 0;
            while (true)
            {
                // 공식 사용해서 왼쪽 노드, 오른쪽 노드 구하기
                int left = 2 * now + 1;
                int right = 2 * now + 2;

                int next = now;
                // 왼쪽 노드가 현재 노드보다 크면
                if (left <= lastIndex && _heap[next].CompareTo(_heap[left]) < 0)
                {
                    // 왼쪽으로 이동
                    next = left;
                }
                // 오른쪽 노드가 현재 노드보다 크면
                if (right <= lastIndex && _heap[next].CompareTo(_heap[right]) < 0)
                {
                    // 오른쪽으로 이동
                    next = right;
                }

                // 왼쪼 노드, 오른쪽 노드가 둘 다 현재 노드보다 작다면
                if (next == now)
                {
                    break;
                }

                // 두 값 교체
                T temp = _heap[now];
                _heap[now] = _heap[next];
                _heap[next] = temp;

                // 검사 위치 이동 (아래로 이동했으면 아래로 가서 검사하게)
                now = next;
            }

            return ret;
        }

        public int Count()
        {
            return _heap.Count;
        }
    }

    class knight : IComparable<knight>
    {
        public int Id { get; set; }

        public int CompareTo(knight other)
        {
            if (Id == other.Id)
            {
                return 0;
            }
            return Id > other.Id ? 1 : -1;
        }
    }

    class Sample
    {
        static void Main(string[] args)
        {
            PriorityQueue<knight> q = new PriorityQueue<knight>();
            q.Push(new knight() { Id = 20 });
            q.Push(new knight() { Id = 30 });
            q.Push(new knight() { Id = 40 });
            q.Push(new knight() { Id = 10 });
            q.Push(new knight() { Id = 05 });

            while (q.Count() > 0)
            {
                Console.WriteLine(q.Pop().Id);
            }
        }
    }
}

using System;
using System.Collections.Generic;

namespace MMORPG_게임개발_시리즈_CSharp__자료구조와_알고리즘
{
    class PriorityQueue
    {
        List<int> _heap = new List<int>();
        
        // 0(logN)
        public void Push(int data)
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
                if (_heap[now] < _heap[next])
                {
                    break;
                }

                // 두 값 교체
                int temp = _heap[now];
                _heap[now] = _heap[next];
                _heap[next] = temp;

                // 검사 위치 이동 (위로 이동했으면 위로 가서 검사하게)
                now = next;
            }
        }

        // 0(logN)
        public int Pop()
        {
            // 반환할 데이터를 따로 저장
            int ret = _heap[0];

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
                if (left <= lastIndex && _heap[next] < _heap[left])
                {
                    // 왼쪽으로 이동
                    next = left;
                }
                // 오른쪽 노드가 현재 노드보다 크면
                if (right <= lastIndex && _heap[next] < _heap[right])
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
                int temp = _heap[now];
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

    class Sample
    {
        static void Main(string[] args)
        {
            PriorityQueue q = new PriorityQueue();
            q.Push(3);
            q.Push(2);
            q.Push(4);
            q.Push(1);
            q.Push(5);

            while (q.Count() > 0)
            {
                // 기본적으로 큰 순서대로 정렬해서 나옴
                Console.WriteLine(q.Pop());
            }

            // 가능한 방법
            // 부호를 반대로 해서 작은 순서대로 정렬해서 나오게
            /*PriorityQueue q = new PriorityQueue();
            q.Push(-3);
            q.Push(-2);
            q.Push(-4);
            q.Push(-1);
            q.Push(-5);

            while (q.Count() > 0)
            {
                Console.WriteLine(-q.Pop());
            }*/
        }
    }
}

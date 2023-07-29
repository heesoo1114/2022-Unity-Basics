using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMORPG_게임개발_시리즈_CSharp__자료구조와_알고리즘
{
    class MyList<T>
    {
        const int DEFAULT_SIZE = 1;
        T[] _data2 = new T[DEFAULT_SIZE];

        public int Count = 0; // 실제로 사용중인 데이터 개수
        public int Capacity {  get { return _data2.Length; } } // 예약된 데이터 개수
        
        // O(1) 예외 케이스 : 이사 비용은 무시한다
        public void Add(T item)
        {
            // 1. 공간 크기 체크
            if (Count >= Capacity)
            {
                // 공간을 늘려서 확보
                T[] newArray = new T[Count * 2];
                for (int i = 0; i < Count; i++)
                {
                    newArray[i] = _data2[i];
                }
                _data2 = newArray;
            }

            // 2. 공간에 데이터 삽입
            _data2[Count] = item;
            Count++;
        }

        // 인덱스
        // O(1)
        public T this[int index]
        {
            get { return _data2[index]; }
            set { _data2[index] = value; }
        }

        // O(N)
        public void RemoveAt(int index)
        {
            for (int i = index; i < Count - 1; i++)
            {
                // 뒤에 애들을 한 칸식 앞으로 데려오기
                _data2[i] = _data2[i + 1];
            }
            _data2[Count - 1] = default(T); // 원래의 형식의 null값으로 
            Count--;
        }
    }

    class Board
    {
        public MyList<int> _data = new MyList<int>();

        public void Initialize()
        {
            // 추가
            _data.Add(101);
            _data.Add(102);
            _data.Add(103);
            _data.Add(104);
            _data.Add(105);

            int temp = _data[2];
            
            // 삭제
            _data.RemoveAt(2); // 2번째 인덱스에 있는 data 삭제
        }
    }
}

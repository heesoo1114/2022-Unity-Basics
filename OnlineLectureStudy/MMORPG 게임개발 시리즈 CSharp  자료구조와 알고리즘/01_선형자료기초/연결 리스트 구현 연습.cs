using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMORPG_게임개발_시리즈_CSharp__자료구조와_알고리즘
{
    class MyLinkedListNode<T>
    {
        public T Data;
        public MyLinkedListNode<T> Next;
        public MyLinkedListNode<T> Prev;
    }

    class MyLinkedList<T>
    {
        public MyLinkedListNode<T> Head = null;
        public MyLinkedListNode<T> Tail = null;
        public int Count = 0;

        // O(1)
        public MyLinkedListNode<T> AddLast(T data)
        {
            MyLinkedListNode<T> newRoom = new MyLinkedListNode<T>();
            newRoom.Data = data;

            // 아직 방이 아예 비어있을 때
            if (Head == null)
            {
                // 새로 추가한 방이 Head가 됨
                Head = newRoom;
            }

            if (Tail != null)
            {
                Tail.Next = newRoom;
                newRoom.Prev = Tail;
            }

            Tail = newRoom;
            Count++;
            return newRoom;
        }

        // O(1)
        public void Remove(MyLinkedListNode<T> room)
        {
            // 기존의 첫번째 방의 다음 방을 첫번째 방으로
            if (Head == room)
            {
                Head = Head.Next;
            }

            // 기존의 마지막 방의 이전 방을 마지막 방으로 
            if (Tail == room)
            {
                Tail = Tail.Prev;
            }

            // 바꾸려는 방의 이전 방이 존재한다면
            if (room.Prev != null)
            {
                // 바꾸려는 방의 이전 방의 다음 방을 바꾸려는 방의 다음 방으로 설정
                room.Prev.Next = room.Next;
            }

            // 바꾸려는 방의 다음 방이 존재한다면
            if (room.Next != null)
            {
                // 바꾸려는 방의 다음 방의 이전 방을 바꾸려는 방의 이전 방으로 설정
                room.Next.Prev = room.Prev;
            }

            Count--;
        }
    }

    class Board
    {
        public MyLinkedList<int> _data = new MyLinkedList<int>();

        public void Initialize()
        {
            _data.AddLast(101);
            _data.AddLast(102);
            MyLinkedListNode<int> node = _data.AddLast(103);
            _data.AddLast(104);
            _data.AddLast(105);

            _data.Remove(node);
        }
    }
}

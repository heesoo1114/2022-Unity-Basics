using System;
using System.Collections.Generic;

namespace MMORPG_게임개발_시리즈_CSharp__자료구조와_알고리즘
{
    internal class Sample
    {
        // 스택: LIFO (후입선출)
        // 큐: FIFO (선입선출)

        static void Main(string[] args)
        {
            #region Stack
            Stack<int> stack = new Stack<int>();

            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            stack.Push(5);

            int data1 = stack.Pop();
            int data2 = stack.Peek();
            #endregion

            #region Queue
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            queue.Enqueue(5);

            int data3 = queue.Dequeue();
            int data4 = queue.Peek();
            #endregion

            #region Linked List
            LinkedList<int> list = new LinkedList<int>();
            list.AddLast(101);
            list.AddLast(102);
            list.AddLast(103);

            int data5 = list.First.Value;
            list.RemoveFirst();

            int data6 = list.Last.Value;
            list.RemoveLast();
            #endregion
        }
    }
}

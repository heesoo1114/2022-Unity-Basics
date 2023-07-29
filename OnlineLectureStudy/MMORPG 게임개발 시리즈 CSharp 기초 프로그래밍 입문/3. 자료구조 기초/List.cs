using System;
using System.Collections;
using System.Collections.Generic;

namespace MMORPG_게임개발_시리즈_CSharp_기초_프로그래밍_입문
{
    internal class Test
    {
        static void Main()
        {
            // 참조 타입임
            
            List<int> list = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(i);
            }

            // 삽입
            list.Insert(2, 999); // 2번째에 999 대입

            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
            }

            // 삭제
            list.Remove(0); // 0이 들어간 번째 삭제
            list.RemoveAt(2); // 2번째 삭제

            foreach (int num in list)
            {
                Console.WriteLine(num);
            }

            list.Clear(); // 리스트 전체 삭제
        }
    }
}

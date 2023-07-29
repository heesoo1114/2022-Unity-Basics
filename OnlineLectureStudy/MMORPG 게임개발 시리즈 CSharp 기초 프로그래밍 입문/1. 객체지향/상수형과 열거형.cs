using System;

namespace MMORPG_게임개발_시리즈_CSharp_기초_프로그래밍_입문
{
    internal class Test
    {
        // 열거형
        /// <summary>
        /// 새로운 타입을 만든다고 생각하면 편함
        /// 안에 내용물들은 상수로 상수형의 성질을 가지고 있음
        /// </summary>
        enum Choice
        {
            rock = 1,
            scissiors = 2,
            paper = 3
        }

        // 상수형
        /// <summary>
        /// 프로그램이 실행되는 동안 데이터 값을 변경하지는 못함
        /// 선언할 때 초기화해주어야 함
        /// </summary>
        const int rock = 1;
        const int scissiors = 2;
        const int paper = 3;


        static void Main()
        {
            // rock = 3;
            // rock은 상수이기 때문에 데이터 값을 변경하지 못함

            Console.WriteLine(rock);
            Console.WriteLine(scissiors);
            Console.WriteLine(paper);

            Console.WriteLine(Choice.rock);
            Console.WriteLine(Choice.scissiors);
            Console.WriteLine(Choice.paper);
        }
    }
}

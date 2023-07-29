using System;

namespace MMORPG_게임개발_시리즈_CSharp_기초_프로그래밍_입문
{
    internal class Test
    {
        static void Main()
        {
            int[] scores = new int[5] { 1, 2, 3, 4, 5 };
            // 0부터 시작
            // 배열의 길이(Length)는 5로 지정해주었기 때문에 5 

            string[] str = { };
            // 배열의 길이, 생성자 안 해주어도 됨

            /*scores[0] = 10;
            scores[1] = 20;
            scores[2] = 30;
            scores[3] = 40;
            scores[4] = 50;*/

            for (int i = 0; i < scores.Length; i++)
            {
                Console.WriteLine(scores[i]);
            }

            foreach (int score in scores)
            {
                Console.WriteLine(score);
            }
        }
    }
}

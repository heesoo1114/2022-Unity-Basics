using System;

namespace MMORPG_게임개발_시리즈_CSharp_기초_프로그래밍_입문
{
    internal class Test
    {
        #region 나의 풀이
        static int Factorial(int number)
        {
            // for문으로 i를 계속 - 시켜 number에 계속 곱해주었다.
            for (int i = number - 1; i > 0; i--)
            {
                number *= i;
            }

            return number;
        }

        static void Main()
        {
            int num = 5;
            Console.WriteLine(Factorial(num));
        }
        #endregion

        #region 강의에서 풀어주는 풀이
        static int Factorial(int number)
        {
            return number * Factorial(number - 1);
            // 재귀함수를 사용하여 더욱 간단하게 가독성 있는 코드를 완성하였다
        }

        static void Main()
        {
            int num = 5;
            Console.WriteLine(Factorial(num));
        }
        #endregion
    }
}

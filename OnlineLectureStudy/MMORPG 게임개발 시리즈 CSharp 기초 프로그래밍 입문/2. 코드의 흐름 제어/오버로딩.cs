using System;

namespace MMORPG_게임개발_시리즈_CSharp_기초_프로그래밍_입문
{
    internal class Test
    {
        /// <summary>
        /// 
        /// 오버로딩: 메서드의 이름을 재사용하는 것
        /// 
        /// 오버로딩을 하기 위해서는 메서드이름과 매개변수 개수, 형식 -> 중요
        /// 반환형식은 중요하지 않음
        /// 
        /// </summary>

        static int Add(int a, int b)
        {
            return a + b;
        }

        // 반황형식은 영향을 미치지 않음
        static void Add(int a, int b, int c = 0, int d = 0) // c는 선택적 매개변수 -> 인수를 전달받지 못해도 상관없음
        {
            Console.WriteLine(a + b);
        }

        static float Add(float a, float b) // 형식을 int 에서 float 으로 변경
        {
            return a + b;
        }

        static void Main()
        {
            Add(1, 2); // 1번째 메서드
            Console.WriteLine(Add(1, 2));

            Add(1, 2, d:3); // 2번째 메서드
            // 선택적 매개변수 c, d 가 있지만 d만 인수를 보내고 싶으면 매개변수의 이름을 지정하고 인수를 넣으면 됨

            Add(1f, 2f); // 3번째 메서드
            Console.WriteLine(Add(1, 2));
        }
    }
}

using System;

namespace MMORPG_게임개발_시리즈_CSharp_기초_프로그래밍_입문
{
    internal class Test
    {
        /// <summary> 
        /// 
        /// ref 와 out 키워드는 인수를 전달하는데 사용
        /// 매개변수를 입력할 때 앞에 ref 와 out 키워드를 써줌으로써 진퉁으로 작업한다는 것을 다시 한번 확인
        /// 
        /// ref 는 인수를 전달하기 전에 초기화
        /// out 은 초기화가 필수가 아님
        /// 
        /// </summary>

        static void Divide(int a, int b, out int result1, out int result2)
        {
            // ref 추가설명: a, b는 ref를 사용하지 않았기 때문에 number1, number2에 참조하지 않음

            result1 = a / b;
            result2 = a % b;
        }

        static void AddOne(ref int number)
        {
            // 매개변수로 넘어온 인수 num1에 참조하게 돼
            // Main에서 선언되어 있는 num1을 직접적으로 ++ 해줌
            // 따라서 Main에서 number1을 출력을 하게 되면 10++을 한 11이 출력

            number++; // <- number1++
        }

        /// <summary>
        /// 
        /// 위의 메서드는 ref를 사용하였고
        /// 아래의 메서드는 ref를 사용하지 않고 int값으로 반환하게 해줌
        /// 
        /// 위의 메서드와 아래의 메서드 중 더 좋은 방향은 아래의 메서드
        /// 
        /// 이유: ref를 사용한 메서드는 받아온 인수에 참조하지만 ref를 사용하지 않고 반환하는 아래의 메서드는 활용도가 높아
        ///      규모 있는 코딩을 하게 되면 아래의 메서드가 훨씬 여러 측면에서 좋다고 볼 수 있다.
        /// 
        /// </summary>

        static int AddOne(int number)
        {
            return number++;
        }

        static void Main()
        {
            int num1 = 10;
            int num2 = 3;

            int result1;
            int result2;

            Divide(num1, num2, out result1, out result2);

            Console.WriteLine(result1);
            Console.WriteLine(result2);

            AddOne(ref num1);
            Console.WriteLine(num1);
        }
    }
}

using System;

namespace MMORPG_게임개발_시리즈_CSharp_기초_프로그래밍_입문
{
    internal class Test
    {
        delegate int OnCicked();
        // delegate -> 함수 자체를 인수로 넘겨주는 느낌의 형식
        // 반환: int 입력 void
        // OnClicked 가 delegage 형식의 이름이 됨

        // delegate 
        // + 반환타입이 있으면 Func
        // + 반환타입이 없으면 Action

        static void ButtonPress(OnCicked clickedFunc) // 함수 자체를 인수로 넘기고
        {
            // 함수를 호출
            clickedFunc();
        }

        static int TestDelegate()
        {
            Console.WriteLine("Hello Delegate");
            return 0;
        }

        static int TestDelegate2()
        {
            Console.WriteLine("Hello Delegate 2");
            return 0;
        }

        static void Main()
        {
            // delegate (대리자)
            Console.WriteLine();

            OnCicked clicked = new OnCicked(TestDelegate);
            clicked += TestDelegate2;

            ButtonPress(clicked);
        }
    }
}
using System;

namespace MMORPG_게임개발_시리즈_CSharp_기초_프로그래밍_입문
{
    internal class Test
    {
        static void Main()
        {
            string name = "Harry Potter";

            // 찾기
            bool found = name.Contains("Harry"); // 문자열에 Harry가 포함되었는지
            int index = name.IndexOf("H"); // 해당 문자가 몇 번째인지

            // 변형
            name = name + " Junior"; // name = Harry Potter Junior
            name.ToLower(); // 전부 소문자로 바꿈
            name.ToUpper(); // 전부 대문자로 바꿈
            name.Replace('H', 'h'); // 문자열에서 H를 h로 변경

            // 분할
            name.Split(new char[] { ' ' }); // 공백을 기준으로 분할
            name.Substring(5); // 지정한 부분부터 출력
        }
    }
}

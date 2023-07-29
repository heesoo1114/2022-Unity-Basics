using System;

namespace MMORPG_게임개발_시리즈_CSharp_기초_프로그래밍_입문
{
    internal class Test
    {
        // Null + able
        // Nullable -> null값을 가질 수 있어짐

        class Monster
        {
            public int Id { get; set; }
        }

        static void Main()
        {
            int number = null; // Nullable X -> null값을 가지지 못함 -> 에러
            int? number2 = null; // Nullable O -> null값을 가질 수 있음

            Monster monster = null;

            if (monster != null)
            {
                int monsterId = monster.Id;
            }

            int? id = monster?.Id;
        }
    }
}
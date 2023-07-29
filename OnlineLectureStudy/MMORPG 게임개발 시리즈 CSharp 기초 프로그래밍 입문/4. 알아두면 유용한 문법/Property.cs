using System;

namespace MMORPG_게임개발_시리즈_CSharp_기초_프로그래밍_입문
{
    internal class Test
    {
        // 객체지향 -> 은닉성

        class Knight
        {
            public int Hp
            {
                get; set;
            }

            // 위와 아래 코드는 같은 역할을 하는 코드

            private int _hp;
            public int GetHp() { return _hp; }
            public void SetHp(int value) { _hp = value; }
        }

        static void Main()
        {
            Knight knight = new Knight();
            int hp = knight.Hp;
            knight.Hp = 200;
        }
    }
}
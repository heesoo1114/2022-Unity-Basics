using System;

namespace MMORPG_게임개발_시리즈_CSharp_기초_프로그래밍_입문
{
    internal class Test
    {
        // 클래스는 참조 타입 변수이고, 구조체는 복사 타입 변수이다

        // 참조
        class Knight
        {
            public int hp;
            public int attack;
        }
        static void KillKnight(Knight knight)
        {
            knight.hp = 0;
        }

        // 복사
        struct Mage
        {
            public int hp;
            public int attack;
        }

        static void KillMage(Mage mage)
        {
            mage.hp = 0;
        }

        static void Main(string[] args)
        {
            
            Mage mage = new Mage();
            mage.hp = 50;
            mage.attack = 10;

            Mage mage2 = mage;
            KillMage(mage2); // mage2.hp = 0 -> mage.hp = 50
            Console.WriteLine(mage.hp);
            // 복사 타입 변수이기 때문에 mage의 hp 변수는 변경되지 않음


            Knight knight = new Knight();
            knight.hp = 100;
            knight.attack = 30;

            Knight knight2 = knight;
            KillKnight(knight2); //knight2.hp = 0 -> knight.hp = 0
            Console.WriteLine(knight.hp);
            // 클래스 타입 변수이기 때문에 knight를 참조하게 되어 knight의 hp 변수도 10으로 변경
        }
    }
}

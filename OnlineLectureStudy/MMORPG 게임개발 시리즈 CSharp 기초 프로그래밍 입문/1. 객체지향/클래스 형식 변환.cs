using System;

namespace MMORPG_게임개발_시리즈_CSharp_기초_프로그래밍_입문
{
    internal class Test
    {
        class Player
        {
            protected int hp;
            protected int attack;
        }

        class Knight : Player
        {

        }

        class Mage : Player
        {
            public int mp;
        }

        static void Entergame(Player player)
        {
            bool isMage = player is Mage; // player의 타입이 Mage인지 불값으로 확인

            Mage mage = player as Mage; // player의 타입이 Mage라면 mage에 대입하고 아니면 null 대입
            if (mage != null)
            {
                mage.mp = 10;
            }
        }

        static void Main()
        {
            Knight knight = new Knight();
            Mage mage = new Mage();

            Entergame(knight);
        }
    }
}

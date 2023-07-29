using System;

namespace MMORPG_게임개발_시리즈_CSharp_기초_프로그래밍_입문
{
    internal class Test
    {
        // this 키워드: 자신
        // base 키워드: 부모

        class Player // 부모 클래스 or 기반 클래스
        {
            static public int counter = 1;
            public int id;
            public int hp;
            public int attack;

            public Player()
            {
                Console.WriteLine("Player 생성자 호출");
            }

            public Player(int hp)
            {
                this.hp = hp;
                Console.WriteLine("Player hp 생성자 호출");
            }
        }

        class Knight : Player // 자식 클래스 or 파생 클래스
        {
            public Knight()
            {
                Console.WriteLine("Knigt 생성자 호출");
            }
        }

        class Mage : Player
        {
            public Mage() : base(10)
            {
                Console.WriteLine("Mage 생성자 호출");
            }
        }

        static void Main()
        {
            Knight knight = new Knight();
            // Player 클래스의 자식 클래스인 Knight 클래스를 생성자를 호출하게 되면 Player 생성자가 먼저 호출된 다음 Knigh 생성자가 호출됨

            Mage mage = new Mage();
            // Mage 클래스의 경우 생성자를 호출할 때 hp 생성자를 호출할 수 있게 부모클래스에게 인수 값 10을 보내줌으로써 어떤 생성자를 호출할지 결정하게 해줌
        }
    }
}

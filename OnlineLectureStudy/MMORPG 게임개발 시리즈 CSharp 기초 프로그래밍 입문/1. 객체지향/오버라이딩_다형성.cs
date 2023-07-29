using System;

namespace MMORPG_게임개발_시리즈_CSharp_기초_프로그래밍_입문
{
    internal class Test
    {
        // 오버라이딩: 부모 클래스가 가지고 있는 메서드를 자식 클래스가 안에 든 내용을 변경하여 사용하는 것
        
        class Player
        {
            protected int hp;
            protected int attack;

            public virtual void Move()
            {
                Console.WriteLine("Player 이동");
            }

            public virtual void Die()
            {
                Console.WriteLine("Player 사망");
            }
        }

        class Knight : Player
        {
            public override void Move()
            {
                Console.WriteLine("Knight 이동!");
            }


            // 현재 SuperKnight의 부모클래스인 상황
            // Knight는 Player의 자식 클래스
            // 부모의 Die 메서드를 오버라이딩

            // sealed: 자신의 자식 클래스부터는 Die를 사용하지 못하게 함
            public sealed override void Die()
            {
                Console.WriteLine("Knight 사망");
            }
        }

        class SuperKnight : Knight
        {
            public override void Move()
            {
                Console.WriteLine("SuperKnight 이동!");
            }

            // 
            public override void Die()
            {
                base.Die();
            }
        }

        class Mage : Player
        {
            public override void Move()
            {
                Console.WriteLine("Mage 이동!");
            }
        }

        static void EnterGame(Player player)
        {
            player.Move();
        }

        static void Main()
        {
            Knight knight = new Knight();
            Mage mage = new Mage();

            EnterGame(mage);
        }
    }
}

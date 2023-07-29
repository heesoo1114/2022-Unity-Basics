using System;

namespace MMORPG_게임개발_시리즈_CSharp_기초_프로그래밍_입문
{
    internal class Test
    {
        abstract class Monster
        {
            public virtual void shout()
            {
                Console.WriteLine("그아아");
            }
        }

        interface IFlyable
        {
            public void Fly();
        }

        class Orc : Monster
        {
            public override void shout()
            {
                Console.WriteLine("그로로");
            }
        }

        class FlyOrc : Orc, IFlyable
        {
            public void Fly()
            {
                Console.WriteLine("날다");
            }
        }

        static void DoFly(IFlyable flyable)
        {
            flyable.Fly();
        }

        static void Main()
        {
            IFlyable flyable = new FlyOrc();
            DoFly(flyable);
        }
    }
}
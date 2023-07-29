using System;
using System.Collections;
using System.Collections.Generic;

namespace MMORPG_게임개발_시리즈_CSharp_기초_프로그래밍_입문
{
    internal class Test
    {
        // 일반화

        // T가 참조 형식이어야 한다면 where T : class
        // T가 값 형식이어야 한다면 where T : struct
        // T가 Monster 또는 Monster를 상속받아야 한다면 where T : Monster

        class In<T> where T : class
        {

        }

        class Out<T> where T : struct
        {

        }

        class MonsterT<T> where T : Monster
        {

        }

        class MyList<T>
        {
            T[] arr = new T[10];

            public T GetItem(int i)
            {
                return arr[i];
            }
        }

        class Monster
        {
            public int hp = 100;
        }

        static void OutPut<T>(T Input)
        {
            Console.WriteLine(Input);
        }

        static void Main()
        {
            MyList<int> list = new MyList<int>();
            MyList<string> list2 = new MyList<string>();
            MyList<Monster> list3 = new MyList<Monster>();

            // 다양한 형식을 활용할 수 있음

            int num = 13;
            OutPut(num);

            string str = "heesoo";
            OutPut(str);

            Monster monster = new Monster();
            OutPut(monster.hp);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;

namespace MMORPG_게임개발_시리즈_CSharp_기초_프로그래밍_입문
{
    internal class Test
    {
        class Monster
        {
            public int id;

            public Monster(int id) {  this.id = id; }
        }

        static void Main()
        {
            // 기본적인 기능들은 List와 매우 흡사

            // Hashtable

            Dictionary<int, Monster> dic = new Dictionary<int, Monster>();
            for (int i = 0; i < 1000; i++)
            {
                dic.Add(i, new Monster(i));
            }

            Monster mon;
            bool found = dic.TryGetValue(500, out mon);

            dic.Remove(300); 
            dic.Clear();
        }
    }
}

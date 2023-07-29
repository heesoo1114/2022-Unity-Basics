using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace MMORPG_게임개발_시리즈_CSharp_기초_프로그래밍_입문
{
    internal class Test
    {
        // Reflection 은 X-Ray

        class Important : System.Attribute
        {
            string message;

            public Important(string message) { this.message = message; }
        }

        class Monster
        {
            [Important("Improtant")]
            public int hp;

            protected int attack;
            private float speed;

            void Attack() { }
        }

        static void Main()
        {
            Monster monster = new Monster();
            Type type = monster.GetType();

            var fields = type.GetFields(System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.NonPublic
                | System.Reflection.BindingFlags.Static
                | System.Reflection.BindingFlags.Instance);

            foreach (FieldInfo field in fields)
            {
                string access = "protected";
                if (field.IsPublic)
                {
                    access = "public";
                }
                else if (field.IsPrivate)
                {
                    access = "private";
                }

                var attributes = field.GetCustomAttributes();

                Console.WriteLine($"{access} {field.FieldType.Name} {field.Name}");
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;

namespace MMORPG_게임개발_시리즈_CSharp_기초_프로그래밍_입문
{
    class InputManager
    {
        public delegate void OnInputKey();
        public event OnInputKey InputKey;

        public void Update()
        {
            if (Console.KeyAvailable == false)
            {
                return;
            }

            ConsoleKeyInfo info = Console.ReadKey();
            if (info.Key == ConsoleKey.A)
            {
                InputKey();
            }
        }
    }

    internal class Test
    {
        static void OnInputTest()
        {
            Console.WriteLine("Input Received!");
        }

        static void Main()
        {
            InputManager inputManager = new InputManager();

            inputManager.InputKey += OnInputTest;

            while (true)
            {
                inputManager.Update();
            }
        }
    }
}
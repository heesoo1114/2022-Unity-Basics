using System;

namespace MMORPG_게임개발_시리즈_CSharp_기초_프로그래밍_입문
{
    internal class Test
    {
        static void Main()
        {
            int[,] arr = new int[2, 3] { {1, 2, 3}, {1, 2, 3}};

            // 가변 배열
            int[][] a = new int[2][];

            //   1 2 3
            // 1 @ @ @
            // 2 @ @ @

            Map map = new Map();
            map.Render();
        }

        class Map
        {
            int[,] tiles =
            {
                {1, 1, 1, 1, 1},
                {1, 0, 0, 0, 1},
                {1, 0, 0, 0, 1},
                {1, 0, 0, 0, 1},
                {1, 1, 1, 1, 1},
            };

            public void Render()
            {
                ConsoleColor defaultColor = Console.ForegroundColor;

                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    for (int x = 0; x < tiles.GetLength(0); x++)
                    {
                        if (tiles[y, x] == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        Console.Write('ㅁ');
                    }
                    Console.WriteLine();
                }

                Console.ForegroundColor = defaultColor;
            }
        }
    }
}

using System;

namespace MMORPG_게임개발_시리즈_CSharp__자료구조와_알고리즘
{
    internal class Sample
    {
        static void Main(string[] args)
        {
            #region 변수 관리
            Board board = new Board();
            Player player = new Player();
            Console.CursorVisible = false;

            const int WAIT_TICK = 1000 / 30;
            int lastTick = 0;
            #endregion

            board.Initialize(25, player);
            player.Initialize(1, 1, board);

            while (true)
            {
                #region 프레임 관리
                int currentTick = System.Environment.TickCount;
                int elapsedTick = currentTick - lastTick; // = 경과시간

                if (elapsedTick < WAIT_TICK)
                {
                    continue;
                }
                int deltaTick = currentTick - lastTick;
                lastTick = currentTick;
                #endregion

                // 입력


                // 로직
                player.Update(deltaTick);

                // 렌더링
                Console.SetCursorPosition(0, 0);
                board.Render();
            }
        }
    }
}

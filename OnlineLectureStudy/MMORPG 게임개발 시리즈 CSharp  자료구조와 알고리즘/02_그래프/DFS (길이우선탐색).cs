using System;
using System.Collections.Generic;

namespace MMORPG_게임개발_시리즈_CSharp__자료구조와_알고리즘
{
    class Graph
    {
        int[,] adj = new int[6, 6]
        {
            { 0, 1, 0, 1, 0, 0 },
            { 1, 0, 1, 1, 0, 0 },
            { 0, 1, 0, 0, 0, 0 },
            { 1, 1, 0, 0, 1, 0 },
            { 0, 0, 0, 1, 0, 1 },
            { 0, 0, 0, 0, 1, 0 },
        };

        List<int>[] adj2 = new List<int>[]
        {
            new List<int>() { 1, 3 },
            new List<int>() { 0, 2, 3 },
            new List<int>() { 1 },
            new List<int>() { 0, 1, 4 },
            new List<int>() { 3, 5 },
            new List<int>() { 4 },
        };

        #region DFS

        // 1. 우선 now부터 방문,
        // 2. now와 연결된 정점들을 하나씩 확인해서 아직 미방문 상태라면 방문
        bool[] visited = new bool[6];

        // 2차원 배열을 사용한 그래프
        public void DFS(int now)
        {
            Console.WriteLine(now);
            visited[now] = true; // 1번
            
            for (int next = 0; next < 6; next++)
            {
                if (adj[now, next] == 0) // 연결되어 있지 않으면 스킵
                {
                    continue;
                }
                if (visited[next]) // 이미 방문했으면 스킵
                {
                    continue;
                }

                DFS(next);
            }
        }

        // 동적 배열을 사용한 그래프
        public void DFS2(int now)
        {
            Console.WriteLine(now);
            visited[now] = true;

            foreach (int next in adj2[now])
            {
                if (visited[next]) // 이미 방문했으면 스킵
                {
                    continue;
                }

                DFS2(next);
            }
        }

        // 모든 정점들이 다 한번씩 돌 수 있게 해줌
        public void SearchAll()
        {
            visited = new bool[6];
            for (int now = 0; now < 6; now++)
            {
                if (visited[now] == false)
                {
                    DFS(now);
                }
            }
        }

        #endregion
    }

    class Sample
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();
            graph.DFS();
        }
    }
}

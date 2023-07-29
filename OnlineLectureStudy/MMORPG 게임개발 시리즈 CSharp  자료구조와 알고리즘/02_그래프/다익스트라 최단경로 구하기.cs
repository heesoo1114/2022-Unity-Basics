using System;
using System.Collections.Generic;

namespace MMORPG_게임개발_시리즈_CSharp__자료구조와_알고리즘
{
    class Graph
    {
        // -1은 연결되지 않은 상태임
        int[,] adj = new int[6, 6]
        {
            { -1, 15, -1, 35, -1, -1 },
            { 15, -1, 5, 10, -1, -1 },
            { -1, 5, -1, -1, -1, -1 },
            { 35, 10, -1, -1, 5, -1 },
            { -1, -1, -1, 5, -1, 5 },
            { -1, -1, -1, -1, 5, -1 },
        };

        public void Dijikstra(int start)
        {
            bool[] visited = new bool[6];
            int[] distance = new int[6];
            Array.Fill(distance, Int32.MaxValue);
            int[] parent = new int[6];

            distance[start] = 0;
            parent[start] = start;

            while (true)
            {
                int closet = Int32.MaxValue;
                int now = -1;

                for (int i = 0; i < 6; i++)
                {
                    // 이미 방문했으면
                    if (visited[i]) continue;

                    // 방문하지 X or 거리가 길면
                    if (distance[i] == Int32.MaxValue || distance[i] >= closet)
                    {
                        continue;
                    }

                    // 지금 시점까지 발견한 가장 좋은 후보
                    closet = distance[i];
                    now = i;
                }

                // 더 이상 없음
                if (now == -1)
                {
                    break;
                }

                // 제일 좋은 후보를 찾았으니까 방문한다.
                visited[now] = true;
                Console.WriteLine(now);

                // 최단거리 갱신
                for (int next = 0; next < 6; next++)
                {
                    // 연결되지 않으면
                    if (adj[now, next] == -1) continue;

                    // 이미 방문헀으면ㄴ
                    if (visited[next]) continue;

                    // 최단거리 계산
                    int nextDist = distance[now] + adj[now, next];

                    // 원래의 거리 > 새 최단거리 이면 
                    if (nextDist < distance[next])
                    {
                        distance[next] = nextDist;
                        parent[next] = now;
                    }
                }
            }
        }
    }

    class Sample
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();
            graph.Dijikstra(2);
        }
    }
}

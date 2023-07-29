using System;

namespace MMORPG_게임개발_시리즈_CSharp_기초_프로그래밍_입문
{
    internal class Test
    {
        #region 나의 풀이
        // 가장 큰 값을 반환
        static int GetHightestScore(int[] scores)
        {
            int result = scores[0];

            for (int i = 0; i < scores.Length; i++)
            {
                if (result <= scores[i]) // 크기 비교
                {
                    // 크면 result에 대입
                    result = scores[i];
                }
            }

            return result;
        }

        // 평균 구하기
        static int GetAverageScore(int[] scores)
        {
            int sum = 0;

            foreach (int score in scores)
            {
                // sum에 다 더해줌
                sum += score;
            }

            // 다 더해진 값에 length로 나누기
            return sum / scores.Length;
        }

        // value가 있으면 몇 번째에 있는지 없으면 없다고 알려줌
        static int GetIndexOf(int[] scores, int value)
        {
            for (int i = 0; i < scores.Length; i++)
            {
                if (scores[i] == value) // 받아온 인수 value와 scores[i]가 같으면
                {
                    // 몇 번째인지 반환
                    return i;
                }
            }

            return -1;
        }

        // 오름차순 정렬
        static void Sort(int[] scores)
        {
            for (int i = 0; i < scores.Length; i++)
            {
                for (int j = i; j < scores.Length; j++)
                {
                    if (scores[i] > scores[j]) // 자신보다 작으면
                    {
                        // 자신과 자리를 바꿔줌
                        // (자신보다 작으면 계속 앞으로 땡겨오고 자신의 차례일 때 자신이 더 크면 뒤로 밀림)

                        int temp = scores[i];
                        scores[i] = scores[j];
                        scores[j] = temp;
                    }
                }
            }

            for (int i = 0; i < scores.Length; i++)
            {
                Console.WriteLine(scores[i]);
            }
        }

        static void Main()
        {
            int[] scores = new int[5] { 10, 50, 40, 20, 30 };

            Console.WriteLine(GetHightestScore(scores));
            Console.WriteLine(GetAverageScore(scores));
            Console.WriteLine(GetIndexOf(scores, 20));
            Sort(scores);
        }
        #endregion

        #region 강의에서 풀어주는 풀이
        static int GetHightestScore(int[] scores)
        {
            int maxValue = 0;

            foreach (int score in scores)
            {
                if (score > maxValue)
                {
                    maxValue = score;
                }
            }

            return maxValue;
        }

        static int GetAverageScore(int[] scores)
        {
            int sum = 0;

            foreach (int score in scores)
            {
                sum += score;
            }

            return sum / scores.Length;
        }

        static int GetIndexOf(int[] scores, int value)
        {
            for (int i = 0; i < scores.Length; i++)
            {
                if (scores[i] == value)
                {
                    return i;
                }
            }

            return -1;
        }

        static void Sort(int[] scores)
        {
            for (int i = 0; i < scores.Length; i++)
            {
                int minIndex = i;
                // 제일 작은 숫자가 있는 index를 찾는다.

                for (int j = i; j < scores.Length; j++)
                {
                    if (scores[j] < scores[minIndex])
                    {
                        minIndex = j;
                    }
                }

                // 제일 작은 숫자와 변경
                int temp = scores[i];
                scores[i] = scores[minIndex];
                scores[minIndex] = temp;
            }

            for (int i = 0; i < scores.Length; i++)
            {
                Console.WriteLine(scores[i]);
            }
        }

        static void Main()
        {
            int[] scores = new int[5] { 10, 30, 40, 20, 50 };

            Sort(scores);
        }
        #endregion
    }
}

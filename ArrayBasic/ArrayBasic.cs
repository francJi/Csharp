﻿namespace ArrayBasic
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //int[] array1 = new int[5];       // 크기가 5인 int형 배열 선언
            //string[] array2 = new string[3]; // 크기가 3인 string형 배열 선언
            //int num = 0;

            //// 배열 초기화
            ///*
            //array1[0] = 1;
            //array1[1] = 2;
            //array1[2] = 3;
            //array1[3] = 4;
            //array1[4] = 5;
            // --> 보통은 일일히 배열에 넣지않고 규칙에 근거하여 반복문을 활용해 배열을 채운다. */

            //for (int i = 0; i < array1.Length; i++)
            //{
            //    array1[i] = i;
            //}

            //num = array1[0];

            // 게임캐릭터 능력치 만들기.

            // 플레이어의 공격력, 방어력, 체력, 스피드를 저장할 배열
            int[] playerStats = new int[4];

            // 능력치를 랜덤으로 생성하여 배열에 저장
            Random rand = new Random();
            for (int i = 0; i < playerStats.Length; i++)
            {
                playerStats[i] = rand.Next(1, 11);
            }

            // 능력치 출력
            Console.WriteLine("플레이어의 공격력: " + playerStats[0]);
            Console.WriteLine("플레이어의 방어력: " + playerStats[1]);
            Console.WriteLine("플레이어의 체력: " + playerStats[2]);
            Console.WriteLine("플레이어의 스피드: " + playerStats[3]);


            // 성적 평균 구하기
            int[] scores = new int[5];

            for (int i = 0; i < scores.Length; i++)
            {
                Console.Write($"{(i+1).ToString()}번째 학생의 점수를 입력해주세요 : ");
                scores[i] = int.Parse(Console.ReadLine());
            }

            int sum = 0;
            for (int i = 0;i < scores.Length; i++)
            {
                sum += scores[i];
            }

            float average = (float)sum / scores.Length;
            Console.WriteLine($"학생 5명의 평균은 {average}점 입니다.");



            // 숫자 3개 맞추기
            Random random = new Random();
            int[] numbers = new int[3];

            //
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(1, 10);
            }

            int attempt = 0;
            int correct = 0;
            while (true)
            {
                Console.Write("3개의 숫자를 입력하세요 : ");
                int[] guesses = new int[3];

                for (int j = 0; j < guesses.Length; j++)
                {
                    guesses[j] = int.Parse(Console.ReadLine()) ;
                }

                for (int i = 0; i < numbers.Length; i++)
                {
                    for (int j = 0; j < guesses.Length; j++)
                    {
                        if (numbers[i] == guesses[j])
                        {
                            Console.WriteLine("정답입니다.");
                            correct++;
                            break;
                        }
                    }
                }

                attempt++;
                Console.WriteLine($"시도 : {attempt.ToString()}회 || 3개의 숫자 중, {correct.ToString()}회 정답을 맞추셨습니다.");
                Console.WriteLine("다시 하시겠습니까? (y / n)");
                string answer = Console.ReadLine();
                if (answer == "n")
                {
                    break;
                }
            }
        }
    }
}
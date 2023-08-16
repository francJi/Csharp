using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using static System.Formats.Asn1.AsnWriter;

namespace ArrayBasic
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

            //// 플레이어의 공격력, 방어력, 체력, 스피드를 저장할 배열
            //int[] playerStats = new int[4];

            //// 능력치를 랜덤으로 생성하여 배열에 저장
            //Random rand = new Random();
            //for (int i = 0; i < playerStats.Length; i++)
            //{
            //    playerStats[i] = rand.Next(1, 11);
            //}

            //// 능력치 출력
            //Console.WriteLine("플레이어의 공격력: " + playerStats[0]);
            //Console.WriteLine("플레이어의 방어력: " + playerStats[1]);
            //Console.WriteLine("플레이어의 체력: " + playerStats[2]);
            //Console.WriteLine("플레이어의 스피드: " + playerStats[3]);


            //// 성적 평균 구하기
            //int[] scores = new int[5];

            //for (int i = 0; i < scores.Length; i++)
            //{
            //    Console.Write($"{(i+1).ToString()}번째 학생의 점수를 입력해주세요 : ");
            //    scores[i] = int.Parse(Console.ReadLine());
            //}

            //int sum = 0;
            //for (int i = 0;i < scores.Length; i++)
            //{
            //    sum += scores[i];
            //}

            //float average = (float)sum / scores.Length;
            //Console.WriteLine($"학생 5명의 평균은 {average}점 입니다.");



            //// 숫자 3개 맞추기
            //Random random = new Random();
            //int[] numbers = new int[3];

            ////
            //for (int i = 0; i < numbers.Length; i++)
            //{
            //    numbers[i] = random.Next(1, 10);
            //}

            //int attempt = 0;
            //int correct = 0;
            //while (true)
            //{
            //    Console.Write("3개의 숫자를 입력하세요 : ");
            //    int[] guesses = new int[3];

            //    for (int j = 0; j < guesses.Length; j++)
            //    {
            //        guesses[j] = int.Parse(Console.ReadLine()) ;
            //    }

            //    for (int i = 0; i < numbers.Length; i++)
            //    {
            //        for (int j = 0; j < guesses.Length; j++)
            //        {
            //            if (numbers[i] == guesses[j])
            //            {
            //                Console.WriteLine("정답입니다.");
            //                correct++;
            //                break;
            //            }
            //        }
            //    }

            //    attempt++;
            //    Console.WriteLine($"시도 : {attempt.ToString()}회 || 3개의 숫자 중, {correct.ToString()}회 정답을 맞추셨습니다.");
            //    Console.WriteLine("다시 하시겠습니까? (y / n)");
            //    string answer = Console.ReadLine();
            //    if (answer == "n")
            //    {
            //        break;
            //    }
            //}

            //// 2차원 배열
            //int[,] array2D = new int[2, 3];

            ///* 초기화
            //array2D[0, 0] = 1;
            //array2D[0, 1] = 2;
            //array2D[0, 2] = 3;
            //array2D[1, 0] = 4;
            //array2D[1, 1] = 5;
            //array2D[1, 2] = 6;
            //*/

            //// 마찬가지로, for문으로 배열을 생성해보면..

            //int array2DRow = array2D.GetLength(0);    // 행의 길이
            //int array2DCol = array2D.GetLength(1);    // 열의 길이
            //for (int i = 0; i < array2DRow; i++)
            //{
            //    for (int j = 1; j < array2DCol+1; j++)
            //    {
            //        array2D[i, j-1] = i * array2DCol + j;
            //        Console.Write(array2D[i, j-1] + " "); // 출력
            //    }
            //    Console.WriteLine();                  // 출력을 위한 줄바꿈
            //}


            //int[,] map = new int[5, 5]
            //{
            //    { 1, 1, 1, 1, 1 },
            //    { 1, 0, 0, 0, 1 },
            //    { 1, 0, 1, 0, 1 },
            //    { 1, 0, 0, 0, 1 },
            //    { 1, 1, 1, 1, 1 }
            //};

            //for (int i = 0; i < 5; i++)
            //{
            //    for (int j = 0; j < 5; j++)
            //    {
            //        if (map[i, j] == 1)
            //        {
            //            Console.Write("■ ");
            //        }
            //        else
            //        {
            //            Console.Write("□ ");
            //        }
            //    }
            //    Console.WriteLine();
            //}

            //List<int> numbers = new List<int>(); // 빈 리스트 생성
            //numbers.Add(1); // 리스트에 데이터 추가
            //numbers.Add(2);
            //numbers.Add(3);
            //numbers.Remove(2); // 리스트에서 데이터 삭제

            //foreach (int number in numbers) // 리스트 데이터 출력
            //{
            //    Console.WriteLine(number);
            //}

            //// 딕셔너리


            //Dictionary<string, int> skills = new Dictionary<string, int>(); // 빈 딕셔너리 생성
            //skills.Add("PowerStrike", 10); // 딕셔너리에 데이터 추가
            //skills.Add("MagicClaw", 40);
            //skills.Add("LuckySeven", 20);
            //skills.Remove("MagicClaw"); // 딕셔너리에서 데이터 삭제

            //foreach (KeyValuePair<string, int> pair in skills) // 딕셔너리 데이터 출력
            //{
            //    Console.WriteLine(pair.Key + ": " + pair.Value);
            //}


            //// 숫자 맞추기 게임
            //Random random = new Random();
            //int number = random.Next(1, 100);
            //int guess = 0;

            //int attempt = 0;

            //while (number != guess)
            //{
            //    Console.Write("1~100 사이의 숫자를 입력하세요 : ");
            //    guess = int.Parse(Console.ReadLine());

            //    if (number == guess)
            //    {
            //        Console.WriteLine("정답입니다.");
            //        attempt++;
            //        Console.WriteLine($"시도한 횟수 : {attempt.ToString()}");
            //    }
            //    else if (number < guess)
            //    {
            //        Console.WriteLine("더 작은 숫자를 입력해주세요");
            //        attempt++;
            //    }
            //    else
            //    {
            //        Console.WriteLine("더 큰 숫자를 입력해주세요");
            //        attempt++;
            //    }
            //}

            // 틱택토

            Console.WriteLine("틱택토 게임에 오신걸 환영합니다.\n\n");
            
            string[,] field = new string[3, 3];
            int[,] calField = new int[3, 3];
            int[] calArray = new int[8];

            for (int i = 0; i < field.GetLength(0); i++) 
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    field[i, j] = $"({i},{j})";
                }
            }

            for (int i = 0; i < calField.GetLength(0); i++)
            {
                for (int j = 0; j < calField.GetLength(1); j++)
                {
                    calField[i, j] = 0;
                }
            }


            Random random = new Random();
            int OXNum = random.Next(0, 2);
            string playerChar;
            string computerChar;
            string order;
            int attempt = 0;

            if (OXNum == 0)
            {
                playerChar = "  O  ";
                computerChar = "  X  ";
                order = "선공";
            }
            else
            {
                playerChar = "X    ";
                computerChar = "O    ";
                order = "후공";
            }
            

            while (true)
            {
                if (attempt == 0)
                {
                    Console.WriteLine($"당신은 {order}입니다.\n");
                    attempt += 1;

                    // 후공일 시, 컴퓨터 턴.
                    if (order == "후공")
                    {
                        field[1, 1] = computerChar;
                        calField[1, 1] = -1;
                    }
                }

                // calArray 정의 및 계산
                for (int i = 0; i < calField.GetLength(1); i++)                      // 각 가로 줄의 합
                { 
                    int sum = 0;
                    for (int j = 0; j < calField.GetLength(0); j++)
                    {
                        sum += calField[i, j];
                    }
                    calArray[i] = sum;
                }
                for (int j = 0; j < calField.GetLength(0); j++)                      // 각 세로 줄의 합
                {
                    int sum = 0;
                    for (int i = 0; i < calField.GetLength(1); i++)
                    {
                        sum += calField[i, j];
                    }
                    calArray[j + 3] = sum;
                }
                calArray[6] = calField[0, 0] + calField[1, 1] + calField[2, 2];       // 대각선 줄의 합 (왼쪽)               
                calArray[7] = calField[0, 2] + calField[1, 1] + calField[2, 0];       // 대각선 줄의 합 (오른쪽)

                // 화면 출력
                for (int i = 0; i < field.GetLength(0); i++)
                {
                    for (int j = 0; j < field.GetLength(1); j++)
                    {
                        Console.Write(field[i, j] + "  ");
                    }
                    Console.WriteLine('\n');
                }

                // 좌표 입력
                Console.WriteLine($"좌표를 입력해주세요. 입력할 때마다, {playerChar}가 표시될 것입니다. ex) 1 1  : \n\n");

                string input = Console.ReadLine();
                // 틀린 형식을 입력했을 시.
                while (input.Contains(" ") == false)
                {
                    Console.WriteLine("올바른 좌표를 입력해주세요! ");
                    input = Console.ReadLine();
                }

                string[] coordinate = input.Split(' ');
                int row = int.Parse(coordinate[0]);
                int col = int.Parse(coordinate[1]);

                // 아닌 좌표를 입력했을 시
                if (
                    ((row < 0) || (row > 2)) ||
                    ((col < 0) || (col > 2))
                    )
                {
                    Console.WriteLine("올바른 좌표를 입력해주세요.");
                    continue;
                }
                
                // 화면에 반영 및 컴퓨터 차례 로직 구현
                else
                {
                    // 입력
                    field[row, col] = playerChar;
                    calField[row, col] = 1;
                    attempt++;

                    // 입력 후, calField 및 calArray 최신화
                    // calArray 정의 및 계산
                    for (int i = 0; i < calField.GetLength(1); i++)                      // 각 가로 줄의 합
                    {
                        int sum = 0;
                        for (int j = 0; j < calField.GetLength(0); j++)
                        {
                            sum += calField[i, j];
                        }
                        calArray[i] = sum;
                    }
                    for (int j = 0; j < calField.GetLength(0); j++)                      // 각 세로 줄의 합
                    {
                        int sum = 0;
                        for (int i = 0; i < calField.GetLength(1); i++)
                        {
                            sum += calField[i, j];
                        }
                        calArray[j + 3] = sum;
                    }
                    calArray[6] = calField[0, 0] + calField[1, 1] + calField[2, 2];       // 대각선 줄의 합 (왼쪽)               
                    calArray[7] = calField[0, 2] + calField[1, 1] + calField[2, 0];       // 대각선 줄의 합 (오른쪽)

                    // 필드 상황 표시
                    
                    Console.WriteLine("\n\n좌표 반영\n");
                    for (int i = 0; i < field.GetLength(0); i++)
                    {
                        for (int j = 0; j < field.GetLength(1); j++)
                        {
                            Console.Write(field[i, j] + "  ");
                        }
                        Console.WriteLine("\n");
                    }
                    Console.WriteLine("\n\n컴퓨터 행동\n\n");


                    // 승리시 break
                    if (calArray.Max() == 3)
                    {
                        Console.WriteLine("올ㅋ 승리하셨습니다.");
                        break;
                    }

                    // attempt가 9번이라면, 무승부화면 추가.
                    if (attempt == 9)
                    {
                        Console.WriteLine("용호상박..!");
                        break;
                    }


                    // 컴퓨터 행동 
                    // 중간이 제일 유리하므로, 비어있으면 가져오기
                    if (calField[1,1] == 0)
                    {
                        row = 1;
                        col = 1;
                        field[row, col] = computerChar;
                        calField[row, col] = -1;
                    }
                    else // 중간을 못 가져올 경우,
                    {
                        bool isAgain = false;
                        // 가장 높은 합을 가진 인덱스를 찾음.
                        int caseNum = Array.FindIndex(calArray, element => element == calArray.Max());
                        
                        // -2 일시, 승리를 위해 마침표 찍어야 함.
                        int finalNum = Array.FindIndex(calArray, element => element == calArray.Min());
                        if (calArray.Min() == -2)
                        {
                            caseNum = finalNum;
                            Console.Write("잡았다. 승리의 실마리!");
                        }

                        // 
                        // 인덱스에 따라 경우 나누기
                        if (caseNum < 3)
                        {
                            List<int> tempList = new List<int>();
                            for (int i = 0; i < 3; i++)
                            {
                                tempList.Add(calField[caseNum, i]);
                            }

                            if (tempList.Contains(0))
                            {
                                int indexNum = tempList.FindIndex(element => element == 0);

                                // 해당 좌표에 컴퓨터 표시
                                row = caseNum;
                                col = indexNum;
                                field[row, col] = computerChar;
                                calField[row, col] = -1;
                            }
                            else
                            {
                                isAgain = true;
                            }
                        }
                        else if (caseNum < 6)
                        {
                            List<int> tempList = new List<int>();
                            for (int i = 0; i < 3; i++)
                            {
                                tempList.Add(calField[i, caseNum-3]);
                            }

                            if (tempList.Contains(0))
                            {
                                int indexNum = tempList.FindIndex(element => element == 0);

                                // 해당 좌표에 컴퓨터 표시
                                //
                                Console.WriteLine(indexNum);
                                //
                                row = indexNum;
                                col = caseNum-3;
                                field[row, col] = computerChar;
                                calField[row, col] = -1;
                            }
                            else
                            {
                                isAgain = true;
                            }
                        }
                        else if (caseNum == 6) 
                        {
                            List<int> tempList = new List<int>();
                            for (int i=0; i < 3; i++)
                            {
                                tempList.Add(calField[i, i]);
                            }

                            if (tempList.Contains(0))
                            {
                                int indexNum = tempList.FindIndex(element => element == 0);

                                // 해당 좌표에 컴퓨터 표시
                                row = indexNum;
                                col = indexNum;
                                field[row, col] = computerChar;
                                calField[row, col] = -1;
                            }
                            else
                            {
                                isAgain = true;
                            }
                        }
                        else
                        {
                            List<int> tempList = new List<int>();
                            for (int i = 0; i < 3; i++)
                            {
                                tempList.Add(calField[i, 2-i]);
                            }

                            if (tempList.Contains(0))
                            {
                                int indexNum = tempList.FindIndex(element => element == 0);

                                // 해당 좌표에 컴퓨터 표시
                                row = indexNum;
                                col = 2-indexNum;
                                field[row, col] = computerChar;
                                calField[row, col] = -1;
                            }
                            else
                            {
                                isAgain = true;
                            }
                        }


                        if (isAgain)
                        {
                            List<int> countList = new List<int>();
                            for (int i = 0; i < calArray.Length; i++) 
                            {
                                int count = 0;
                                if (i < 3)
                                {
                                    for (int j = 0; j < 3; j++)
                                    {
                                        if (calField[i, j] == 0)
                                        {
                                            count++;
                                        }
                                    }
                                    countList.Add(count);
                                }
                                else if (i < 6)
                                {
                                    for (int j = 0; j < 3; j++)
                                    {
                                        if (calField[j, i - 3] == 0)
                                        {
                                            count++;
                                        }
                                    }
                                    countList.Add(count);
                                }
                                else if (i == 6)
                                {
                                    for (int j = 0; j < 3; j++)
                                    {
                                        if (calField[j, j] == 0)
                                        {
                                            count++;
                                        }
                                    }
                                    countList.Add(count);
                                }
                                else
                                {
                                    for (int j = 0; j < 3; j++)
                                    {
                                        if (calField[j, 2-j] == 0)
                                        {
                                            count++;
                                        }
                                    }
                                    countList.Add(count);
                                }
                            }

                            int countMax = countList.Max();
                            if (countMax < 3)
                            {
                                caseNum = countMax;
                                for (int i = 0; i < 3; i++)
                                {
                                    if (calField[caseNum, i] == 0)
                                    {
                                        row = caseNum;
                                        col = i;
                                        field[row, col] = computerChar;
                                        calField[row, col] = -1;
                                        break;
                                    }
                                 }
                                isAgain = false;
                            }
                            else if (countMax < 6)
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    if (calField[i, caseNum-3] == 0)
                                    {
                                        row = i;
                                        col = caseNum-3;
                                        field[row, col] = computerChar;
                                        calField[row, col] = -1;
                                        break;
                                    }
                                }
                                isAgain = false;
                            }
                            else if (countMax == 6)
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    if (calField[i, i] == 0)
                                    {
                                        row = i;
                                        col = i;
                                        field[row, col] = computerChar;
                                        calField[row, col] = -1;
                                        break;
                                    }
                                }
                                isAgain = false;
                            }
                            else
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    if (calField[i, 2-i] == 0)
                                    {
                                        row = i;
                                        col = 2-i;
                                        field[row, col] = computerChar;
                                        calField[row, col] = -1;
                                        break;
                                    }
                                }
                                isAgain = false;
                            }

                        }



                        // attempt ++
                        attempt++;

                        // attempt가 9번이라면, 무승부화면 추가.
                        if (attempt == 9)
                        {
                            Console.WriteLine("용호상박..!");
                            break;
                        }

                        // calArray 정의 및 계산
                        for (int i = 0; i < calField.GetLength(1); i++)                      // 각 가로 줄의 합
                        {
                            int sum = 0;
                            for (int j = 0; j < calField.GetLength(0); j++)
                            {
                                sum += calField[i, j];
                            }
                            calArray[i] = sum;
                        }
                        for (int j = 0; j < calField.GetLength(0); j++)                      // 각 세로 줄의 합
                        {
                            int sum = 0;
                            for (int i = 0; i < calField.GetLength(1); i++)
                            {
                                sum += calField[i, j];
                            }
                            calArray[j + 3] = sum;
                        }
                        calArray[6] = calField[0, 0] + calField[1, 1] + calField[2, 2];       // 대각선 줄의 합 (왼쪽)               
                        calArray[7] = calField[0, 2] + calField[1, 1] + calField[2, 0];       // 대각선 줄의 합 (오른쪽)

                        //break 부분 졌을 때
                        if (calArray.Min() == -3)
                        {
                            Console.WriteLine("컴퓨터에게 패배한 허접~\n\n");
                            for (int i = 0; i < field.GetLength(0); i++)
                            {
                                for (int j = 0; j < field.GetLength(1); j++)
                                {
                                    Console.Write(field[i, j] + "  ");
                                }
                                Console.WriteLine("\n");
                            }
                            break;
                        }
                    }
                }
            }
        }
    }
}
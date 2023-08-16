using System.ComponentModel.Design;

namespace TicTacToe
{
    internal class TicTacToe
    {
        static void Main(string[] args)
        {
            // !! 1. 게임 시작
            Console.WriteLine("틱택토 게임에 오신걸 환영합니다.\n\n");

            string[,] field = new string[3, 3]; // 화면 출력을 위한 Array
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    field[i, j] = " ";
                 }
            }

            int[,] calField = new int[3, 3];    // 게임 상황 확인을 위한 Array
            for (int i = 0; i < calField.GetLength(0); i++)
            {
                for (int j = 0; j < calField.GetLength(1); j++)
                {
                    calField[i, j] = 0;
                }
             }

            Board board = new Board();

            // !! 2. 선공과 후공 정하기
            Random random = new Random();
            int OXNum = random.Next(0, 2);
            string playerChar;
            string computerChar;

            if (OXNum == 0)
            {
                playerChar = "O";
                computerChar = "X";
                Console.WriteLine("당신은 선공입니다.\n");
                Console.WriteLine("당신의 표식은 O 입니다.\n");
            }
            else
            {
                playerChar = "X";
                computerChar = "O";
                Console.WriteLine("당신은 후공입니다.\n");
                Console.WriteLine("당신의 표식은 X 입니다.\n");
            }

            int attempt = 0;                         // 첫턴 구분용.
            bool isEnd = false;                      // 게임 끝내기용

            while (true)
            {
                if (isEnd)
                {
                    break;
                }
                if (attempt == 0)
                {
                    if (OXNum == 1)                    // 후공이라면..
                    {
                        field[1, 1] = computerChar;    // 컴퓨터가 중앙에 놓기
                        calField[1, 1] = -1;            // 계산용 배열에 추가하기
                        attempt += 1;
                    }
                    Console.WriteLine("<시작>\n");
                    board.GameBoard(field);
                    string input = board.Announcement(field);
                    board.inputApply(input, playerChar, ref field, ref calField);
                    attempt += 1;
                }
                else 
                {
                    // 컴퓨터 작동
                    board.ComputerPlay(ref calField, ref field, computerChar);
                    attempt += 1;
                    Console.WriteLine("<컴퓨터>\n");
                    board.GameBoard(field);
                    isEnd = board.GameOver(ref calField, attempt);
                    if (isEnd)
                    {
                        break;
                    }
                    // 플레이어 차례
                    string input = board.Announcement(field);
                    input = board.inputApply(input, playerChar, ref field, ref calField);
                    attempt += 1;
                    Console.WriteLine("<플레이어>\n");
                    board.GameBoard(field);
                    isEnd = board.GameOver(ref calField, attempt);
                    if (isEnd)
                    {
                        break;
                    }
                }
            }
        }

        class Board
        {
            
            public void GameBoard(string[,] boardArray)
            {
                // 보드는 3 * 3 크기
                Console.WriteLine("     |     |     ");
                Console.WriteLine("  {0}  |  {1}  |  {2}  ", boardArray[0,0], boardArray[1, 0], boardArray[2, 0]);
                Console.WriteLine("_____|_____|_____");
                Console.WriteLine("     |     |     ");
                Console.WriteLine("  {0}  |  {1}  |  {2}  ", boardArray[0, 1], boardArray[1, 1], boardArray[1, 2]);
                Console.WriteLine("_____|_____|_____");
                Console.WriteLine("     |     |     ");
                Console.WriteLine("  {0}  |  {1}  |  {2}  ", boardArray[0, 2], boardArray[1, 2], boardArray[2, 2]);
                Console.WriteLine("     |     |     ");
                Console.WriteLine("\n\n");
            }

            public bool GameOver(ref int[,] calField, int attempt)
            {
                int[] calArray = GetCalArray(ref calField);
                int calMax = calArray.Max();
                int calMin = calArray.Min();
                if (calMax == 3)
                {
                    Console.WriteLine("올ㅋ 플레이어 승리");
                    return true;
                }
                else if (calMin == -3)
                {
                    Console.WriteLine("허~접~");
                    return true;
                }
                else
                {
                    if (attempt == 9)
                    {
                        Console.WriteLine("무승부");
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            public string Announcement(string[,] boardArray)
            {
                Console.WriteLine($"\n 두고 싶은 곳의 좌표를 설정해 주세요 ex) 1 1 : \n");
                string input = Console.ReadLine();
                return input;
            }

            public string inputApply(string input, string playerChar, ref string[,] field, ref int[,] calField)
            {
                int attempts = 0;
                while (true)
                {
                    int inputResult = int.TryParse(input[0].ToString(), out inputResult) ? inputResult : -1;
                    if (
                        (input.Contains(" ") == false) ||
                        (inputResult == -1)
                        )
                    {
                        Console.WriteLine("올바른 좌표를 입력해주세요!");
                        input = Console.ReadLine();
                        continue;
                    }
                    string[] coordinate = input.Split(' ');
                    int row = int.Parse(coordinate[0]);
                    int col = int.Parse(coordinate[1]);
                    if ((row < 0) || (row > 2) || (col < 0) || (col > 2)) // 아닌 좌표를 입력했을 시
                    {
                        Console.WriteLine("올바른 좌표를 입력해주세요!");
                        input = Console.ReadLine();
                        continue;
                    }
                    else if ((calField[row, col] != 0))
                    {
                        Console.WriteLine("중복된 좌표를 입력하셨습니다. 다시 입력해주세요!");
                        input = Console.ReadLine();
                        continue;
                    }
                    else
                    {
                        // 배열에 적용
                        coordinate = input.Split(' ');
                        row = int.Parse(coordinate[0]);
                        col = int.Parse(coordinate[1]);
                        field[row, col] = playerChar;
                        calField[row, col] = 1;

                        // 화면 출력
                        Console.WriteLine("\n\n");
                        GameBoard(field);
                        Console.WriteLine("\n\n");
                        break;
                    }
                }
                return input;
            }

            public int[] GetCalArray(ref int[,] calField)
            {
                int[] calArray = new int[8];
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
                
                return calArray;
            }

            public void ArrayCheck(ref int[,] array)
            {
                for (int i = 0; i < array.GetLength(0); i++)
                {
                    for (int j = 0; j < array.GetLength(1); j++)
                    {
                        Console.Write(array[i, j] + " , ");
                    }
                    Console.WriteLine('\n');
                }
            }

            public bool DuplicateChecker(int type, List<int> tempList, bool isDuplicated, string computerChar, int caseNum, ref int[,] calField, ref string[,]field)
            {
                if (tempList.Contains(0))                                               // 빈 값 찾기
                {
                    int indexNum = tempList.FindIndex(element => element == 0);
                    int row;
                    int col;
                    switch (type)
                    {
                        case 1:
                            row = caseNum;                                                      // 해당 좌표에 컴퓨터 표시
                            col = indexNum;
                            field[row, col] = computerChar;
                            calField[row, col] = -1;
                            break;
                        case 2:
                            row = indexNum;
                            col = caseNum - 3;
                            field[row, col] = computerChar;
                            calField[row, col] = -1;
                            break;
                        case 3:
                            row = indexNum;
                            col = indexNum;
                            field[row, col] = computerChar;
                            calField[row, col] = -1;
                            break;
                        case 4:
                            row = indexNum;
                            col = 2-indexNum;
                            field[row, col] = computerChar;
                            calField[row, col] = -1;
                            break;
                    }
                    isDuplicated = false;
                    return isDuplicated;
                    
                }
                else                                                                    // 빈 값이 없다면 다른 index(caseNum) 찾기
                {
                    isDuplicated = true;
                    return isDuplicated;
                }
            }

            private bool DuplicateLoop(bool isDuplicated, string computerChar, int caseNum, ref int[,] calField, ref string[,] field, int[] calArray)
            {
                if (isDuplicated)
                {
                    {
                        List<int> countList = new List<int>();                     // calArray 와 같은 방식으로 0인 갯수를 찾음.
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
                                    if (calField[j, 2 - j] == 0)
                                    {
                                        count++;
                                    }
                                }
                                countList.Add(count);
                            }
                        }

                        int countMax = countList.Max();
                        caseNum = countMax;
                        if (countMax < 3)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (calField[caseNum, i] == 0)
                                {
                                    int row = caseNum;
                                    int col = i;
                                    field[row, col] = computerChar;
                                    calField[row, col] = -1;
                                    break;
                                }
                            }
                        }
                        else if (countMax < 6)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (calField[i, caseNum - 3] == 0)
                                {
                                    int row = i;
                                    int col = caseNum - 3;
                                    field[row, col] = computerChar;
                                    calField[row, col] = -1;
                                    break;
                                }
                            }
                        }
                        else if (countMax == 6)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (calField[i, i] == 0)
                                {
                                    int row = i;
                                    int col = i;
                                    field[row, col] = computerChar;
                                    calField[row, col] = -1;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (calField[i, 2 - i] == 0)
                                {
                                    int row = i;
                                    int col = 2 - i;
                                    field[row, col] = computerChar;
                                    calField[row, col] = -1;
                                    break;
                                }
                            }
                        }
                        isDuplicated = false;
                        return isDuplicated;
                    }
                }
                else
                {
                    isDuplicated = false;
                    return isDuplicated;
                }
            }
 
            public void ComputerPlay(ref int[,] calField, ref string[,] field, string computerChar)
            {
                bool isDuplicated = false;             // 중복 여부 확인

                int[] calArray = GetCalArray(ref calField);
                int caseNum = Array.FindIndex(calArray, element => element == calArray.Max());   // 가장 높은 합을 가진 인덱스를 찾음.
                int finalNum = Array.FindIndex(calArray, element => element == calArray.Min());  // 가장 낮은 합을 가진 인덱스를 찾음.

                while (isDuplicated == false)
                {
                    // finalNum이 -2일 때, 마저 이어서 게임을 승리한다.
                    if (calArray.Min() == -2)
                    {
                        caseNum = finalNum;
                    }
                    // 중간값 비어있다면..
                    if (calField[1,1] == 0)
                    {
                        int row = 1;
                        int col = 1;
                        field[row, col] = computerChar;
                        calField[row, col] = -1;
                        break;
                    }
                    else
                    {
                        // caseNum의 index에 따라 경우 나누기
                        if (caseNum < 3)
                        {
                            List<int> tempList = new List<int>();
                            for (int i = 0; i < 3; i++)
                            {
                                tempList.Add(calField[caseNum, i]);
                            }
                            int type = 1;
                            isDuplicated = DuplicateChecker(type, tempList, isDuplicated, computerChar, caseNum, ref calField, ref field);
                            if (isDuplicated != true)
                            {
                                break;
                            }
                            else
                            {
                                isDuplicated = DuplicateLoop(isDuplicated, computerChar, caseNum, ref calField, ref field, calArray);
                                break;
                            }

                        }
                        else if (caseNum < 6)
                        {
                            List<int> tempList = new List<int>();
                            for (int i = 0; i < 3; i++)
                            {
                                tempList.Add(calField[i, caseNum - 3]);
                            }

                            int type = 2;
                            isDuplicated = DuplicateChecker(type, tempList, isDuplicated, computerChar, caseNum, ref calField, ref field);
                            if (isDuplicated != true)
                            {
                                break;
                            }
                            else
                            {
                                isDuplicated = DuplicateLoop(isDuplicated, computerChar, caseNum, ref calField, ref field, calArray);
                                break;
                            }
                        }
                        else if (caseNum == 6)
                        {
                            List<int> tempList = new List<int>();
                            for (int i = 0; i < 3; i++)
                            {
                                tempList.Add(calField[i, i]);
                            }

                            int type = 3;
                            isDuplicated = DuplicateChecker(type, tempList, isDuplicated, computerChar, caseNum, ref calField, ref field);
                            if (isDuplicated != true)
                            {
                                break;
                            }
                            else
                            {
                                isDuplicated = DuplicateLoop(isDuplicated, computerChar, caseNum, ref calField, ref field, calArray);
                                break;
                            }
                        }
                        else
                        {
                            List<int> tempList = new List<int>();
                            for (int i = 0; i < 3; i++)
                            {
                                tempList.Add(calField[i, 2 - i]);
                            }

                            int type = 4;
                            isDuplicated = DuplicateChecker(type, tempList, isDuplicated, computerChar, caseNum, ref calField, ref field);
                            if (isDuplicated != true)
                            {
                                break;
                            }
                            else
                            {
                                isDuplicated = DuplicateLoop(isDuplicated, computerChar, caseNum, ref calField, ref field, calArray);
                                break;
                            }
                        }




                    }
                }
            }
        }
    }
}
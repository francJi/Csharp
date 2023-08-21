using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace SnakeGame
{
    internal class Program
    {

        // 뱀은 매턴마다 자신의 앞으로 이동합니다.
        // 사용자는 방향키를 이용하여 뱀의 이동 방향을 제어할 수 있습니다.
        // 뱀은 맵에 무작위로 생성되는 음식을 먹을 수 있습니다.
        // 뱀은 벽이나 자신의 몸에 부딪히면 게임이 끝나고 'Game Over' 메시지가 뜹니다.


        // Main 함수에서 게임을 제어하는 코드를 작성합니다 : 뱀의 이동, 음식 먹기, 게임 오버 조건 확인 등을 주기적으로 수행합니다.
        static void Main(string[] args)
        {
            ExampleField example = new ExampleField(20, 20, null);
            
            // calField 세팅
            int [,] calField = example.CalField;

            // initField 세팅
            int[,] initField = calField;

            // snake 객체
            Snake snake = new Snake(2,3);
            snake.snakeLength = 1;

            // 음식 객체
            FoodCreator food = new FoodCreator();

            // 방향 
            Direction direction = Direction.Right; // Default Direction
            ConsoleKeyInfo directKey;

            Direction GetDirectKey(Direction direction)
            {
                directKey = Console.ReadKey();
                switch (directKey.Key)
                {
                    case ConsoleKey.LeftArrow:
                        direction = Direction.Left;
                        return direction;

                    case ConsoleKey.RightArrow:
                        direction = Direction.Right;
                        return direction;

                    case ConsoleKey.UpArrow:
                        direction = Direction.Up;
                        return direction;

                    case ConsoleKey.DownArrow:
                        direction = Direction.Down;
                        return direction;

                    default:
                        return direction;
                }

            }

            snake.onDirect += GetDirectKey;


            // 뱀의 움직임 재현.
            int foodControl = 0;
            while (true)
            {
                if (snake.snakeLength < 0)
                {
                    Console.Clear();
                    example.GameOver(ref calField);
                    example.PrintArray(snake);
                    break;
                }

                example = new ExampleField(20, 20, calField);
                calField[snake._snakeHeadSiteX, snake._snakeHeadSiteY] = snake.snakeLength;
                if (Console.KeyAvailable)
                {
                    snake.direction = snake.Direct(direction);
                }
                Console.Clear();
                snake.SnakeMove(ref calField);
                snake.BodyController(ref calField);
                if (foodControl % 3 == 1)
                {
                    food.MakeFood(ref calField);
                }
                foodControl++;
                example.PrintArray(snake);
                Thread.Sleep(400);
            }

        }
    }
    // 예시 필드 생성
    class ExampleField
    {
        private int rowSize;
        public int RowSize
        {
            get { return rowSize; }
            set { rowSize = value; }
        }
        private int columnSize;
        public int ColumnSize
        {
            get { return columnSize; }
            set { columnSize = value; }
        }

        private int[,] calField;
        public int[,] CalField
        {
            get { return calField; }
            set { calField = new int[rowSize, columnSize];
                if (value == null || value[0,0] == 0)
                {
                    for (int row = 0; row < rowSize; row++)
                    {
                        for (int column = 0; column < columnSize; column++)
                        {
                            if (column == 0 || row == calField.GetLength(0) - 1 || row == 0 || column == calField.GetLength(1) - 1)
                            {
                                calField[row, column] = -3000;
                            }
                            else
                            {
                                calField[row, column] = 0;
                            }
                        }
                    }
                }
                else
                {
                    calField = value;
                }
            }
        }


        public ExampleField(int rowSize, int columnSize, int[,] calField)
        {
            RowSize = rowSize;
            ColumnSize = columnSize;
            CalField = calField;
        }

        public void PrintArray(Snake snake)
        {
            int overCount = 0;
            string gameOverText = "GAME OVER";
            for (int row = 0; row < RowSize; row++)
            {
                for (int column = 0; column < ColumnSize; column++)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    switch (calField[row, column])
                    {
                        case 1:
                            if (row == snake._snakeHeadSiteX && column == snake._snakeHeadSiteY)
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write('▶');
                                    break;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.Write('●');
                                break;
                            }
                        case -3000:
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write('■');
                            break;
                        case -2000:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write('■');
                            break;
                        case 0:
                            Console.Write("  ");
                            break;
                        case -5999:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(gameOverText[overCount] + " ");
                            overCount += 1;
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write('▶');
                            break;
                    }
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        public void GameOver(ref int[,] calField)
        {
            int startSiteX = calField.GetLength(0) / 2;
            int startSiteY = (calField.GetLength(1) / 2) - 4;

            for (int i = 0; i < 9; i++)
            {
                int realSiteY = startSiteY + i - 4;
                calField[startSiteX, startSiteY + realSiteY] = -5999;
            }
        }
    }

    

    

    // 이벤트 델리게이트 설정
    delegate Direction SnakeDirectionHandler(Direction direction);

    // Snake 클래스를 만듭니다 : 뱀의 상태와 이동, 음식 먹기, 자신의 몸에 부딪혔는지 확인 등의 기능을 담당합니다.
    class Snake
    {
        // 뱀의 머리를 좌표로 결정.
        private int snakeHeadSiteX;
        private int snakeHeadSiteY;
        public int _snakeHeadSiteX
        {
            get { return snakeHeadSiteX; }
            set { snakeHeadSiteX = value; }
        }

        public int _snakeHeadSiteY
        {
            get { return snakeHeadSiteY; }
            set { snakeHeadSiteY = value; }
        }

        // 생성자
        public Snake(int siteX, int siteY)
        {
            _snakeHeadSiteX = siteX;
            _snakeHeadSiteY = siteY;
        }

        // 뱀의 길이 선언.
        public int snakeLength { get; set; }

        // 뱀의 방향 결정
        public event SnakeDirectionHandler onDirect;

        public Direction Direct(Direction direction)
        {
            direction = onDirect.Invoke(direction);
            return direction;
        }

        public Direction direction;

        // 뱀의 몸통 재현.

        Queue<int[]> bodySites = new Queue<int[]>();
        public void BodyController(ref int[,] calField)
        {
            snakeLength = calField[snakeHeadSiteX, snakeHeadSiteY];
            int bodyLength = snakeLength - 1;
            int[] newSite = new int[2] { _snakeHeadSiteX, _snakeHeadSiteY };
            bodySites.Enqueue(newSite);
            while (bodySites.Count > snakeLength && snakeLength > 0)
            {
                int[] deleteSite = bodySites.Dequeue();                 // 큐에서 요소 값을 지우면서 따로 선언.
                calField[deleteSite[0], deleteSite[1]] = 0;             // 더 이상, 몸통에 해당되지 않는 값 초기 calField 값으로.
            }

            int count = 0;
            if (bodySites.Count < snakeLength)
            {
                if (count != bodySites.Count)
                {
                    foreach (int[] bodyData in bodySites)
                    {
                        if (count != bodySites.Count - 1)
                        {
                            calField[bodyData[0], bodyData[1]] = -2000;
                            count++;
                        }
                    }
                }
            }
            else
            {
                foreach (int[] bodyData in bodySites)
                {
                    if (count != bodyLength)
                    {
                        calField[bodyData[0], bodyData[1]] = -2000;
                        count++;
                    }
                }
            }
           
        }

        // 뱀의 속도 결정.
        public int speed = 400;
        public int threadSpeed = 400;


        // 뱀의 움직임 재현.
        public void SnakeMove(ref int[,] calField)
        {
            if (bodySites.Count == 0)
            {
                int[] startSite = {_snakeHeadSiteX, _snakeHeadSiteY };
                bodySites.Enqueue(startSite);
            }

            // 속도 계산
            int snakeSpeed = speed / threadSpeed;


            // 다음 좌표 변수 선언
            int aftSnakeHeadSiteX = snakeHeadSiteX;
            int aftSnakeHeadSiteY = snakeHeadSiteY;


            

            // 방향별 변화 설정.
            switch (direction)
            {
                case Direction.Up:
                    aftSnakeHeadSiteX -= snakeSpeed;
                    break;
                case Direction.Down:
                    aftSnakeHeadSiteX += snakeSpeed;
                    break;
                case Direction.Left:
                    aftSnakeHeadSiteY -= snakeSpeed;
                    break;
                case Direction.Right:
                    aftSnakeHeadSiteY += snakeSpeed;
                    break;
            }

            // 기존 값 불러오기
            int beforeHeadValue = calField[snakeHeadSiteX, snakeHeadSiteY];

            // 다음에 움직였을 때의 값 계산 및 대입하기
            int aheadValue = calField[aftSnakeHeadSiteX, aftSnakeHeadSiteY];
            int resultValue = beforeHeadValue + aheadValue;
            snakeHeadSiteX = aftSnakeHeadSiteX;
            snakeHeadSiteY = aftSnakeHeadSiteY;
            calField[snakeHeadSiteX, snakeHeadSiteY] = resultValue;
        }
    }

    // FoodCreator 클래스를 만듭니다 : 이 클래스는 맵의 크기 내에서 무작위 위치에 음식을 생성하는 역할을 합니다.
    class FoodCreator
    {
        Random random = new Random();
        int FoodSpeed;

        public void MakeFood(ref int[,] calField)
        {
            int createCount = 0;
            while (createCount < 1)
            {
                int foodSiteX = random.Next(1, calField.GetLength(0) - 1);
                int foodSiteY = random.Next(1, calField.GetLength(1) - 1);

                if (calField[foodSiteX, foodSiteY] == 0)
                {
                    calField[foodSiteX, foodSiteY] = 1;
                    createCount++;
                }
                else
                {
                    continue;
                }
            }
        }
    }

    enum Direction
    {
        Up, Down, Left, Right
    }
}
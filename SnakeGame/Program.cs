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
            snake.snakeLength = 5;

            // calField에 뱀 반영 (1)
            calField[2, 3] = snake.snakeLength;

            // siteCalc 객체
            SiteCalculator siteCal = new SiteCalculator(2,3,calField);

            snake.OnMove += siteCal.HandleLength;


            //이벤트 발생
            snake.MoveEvent();
            Console.WriteLine(snake.snakeLength);


            // 뱀의 움직임 재현.
            while (true)
            {
                if (snake._snakeHeadSiteX < 20)
                {
                    example = new ExampleField(20, 20, calField);
                    calField[snake._snakeHeadSiteX, snake._snakeHeadSiteY] = snake.snakeLength;
                    snake.SnakeMove(ref calField);
                    snake.BodyController(ref calField);
                    example.PrintArray();
                    Thread.Sleep(500);
                    Console.Clear();
                }
                else
                {
                    break;
                }

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

        public void PrintArray()
        {
            for (int row = 0; row < RowSize; row++)
            {
                for (int column = 0; column < ColumnSize; column++)
                {
                    Console.Write(calField[row, column] + " ");
                }
                Console.WriteLine();
            }
        }
    }

    

    

    // 이벤트 델리게이트 설정
    public delegate int SnakeCollideHandler(int snakeHeadSiteX, int snakeHeadSiteY, int snakeLength);

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

        // 뱀의 속도 결정.
        public int speed = 500;
        public int threadSpeed = 500;


        // 뱀의 몸통 재현.
        Queue<int[]> bodySites = new Queue<int[]>();
        public void BodyController(ref int[,] calField)
        {
            int bodyLength = snakeLength - 1;
            int[] newSite = new int[2] { _snakeHeadSiteX, _snakeHeadSiteY };
            bodySites.Enqueue(newSite);
            while (bodySites.Count > snakeLength)
            {
                int[] deleteSite = bodySites.Dequeue();                 // 큐에서 요소 값을 지우면서 따로 선언.
                calField[deleteSite[0], deleteSite[1]] = 0;             // 더 이상, 몸통에 해당되지 않는 값 초기 calField 값으로.
            }

            Console.WriteLine($"몸길이 : {bodyLength}");
            int count = 0;
            foreach (int[] bodyData in bodySites)
            {
                if (count != bodyLength)
                {
                    Console.WriteLine($"몸통 좌표 : {bodyData[0]}, {bodyData[1]}");
                    calField[bodyData[0], bodyData[1]] = -3000;
                    count++;
                }
                else
                {
                    Console.WriteLine($"머리 좌표 : {bodyData[0]}, {bodyData[1]}");
                }
            }
        }
       

        // 뱀의 움직임 재현.
        public void SnakeMove(ref int[,] calField)
        {
            if (bodySites.Count == 0)
            {
                int[] startSite = {_snakeHeadSiteX, _snakeHeadSiteY };
                bodySites.Enqueue(startSite);
            }
            int beforeHeadValue = calField[_snakeHeadSiteX, _snakeHeadSiteY];
            int aftSnakeHeadSiteX = snakeHeadSiteX + (speed / threadSpeed);
            if (aftSnakeHeadSiteX < calField.GetLength(0))
            {
                int aheadValue = calField[aftSnakeHeadSiteX, _snakeHeadSiteY];
                int resultValue = beforeHeadValue + aheadValue;
                snakeHeadSiteX = aftSnakeHeadSiteX;
                calField[snakeHeadSiteX, snakeHeadSiteY] = resultValue;
            }                
            else
            {
                Console.WriteLine("벽꽝");
            }
        }


        // 음식과 부딪혔을 때의 이벤트 설정
        public event SnakeCollideHandler OnMove;

        public int MoveEvent()
        {
            snakeLength = OnMove.Invoke(snakeHeadSiteX, snakeHeadSiteY, snakeLength);
            return snakeLength;
        }

    }

    // 좌표 계산 해주는 클래스 설정
    class SiteCalculator
    {
        // 계산할 좌표 선언
        private int calcSiteX;
        private int calcSiteY;
        public int _calcSiteX
        {
            get { return calcSiteX; }
            set { calcSiteX = value; }
        }
        public int _calcSiteY
        {
            get { return calcSiteY; }
            set { calcSiteY = value; }
        }
        
        // 계산 배열 선언 및 초기화.
        public int[,] calField { get; set; }

        //생성자
        public SiteCalculator(int siteX, int siteY, int[,] array2D) 
        {
            calcSiteX = siteX;
            calcSiteY = siteY;
            calField = array2D;
        }

        // 뱀의 길이 설정 (뱀의 머리 좌표값 계산 후 Length 반영)
        public int HandleLength(int snakeHeadSiteX, int snakeHeadSiteY, int snakeLength)
        {
            snakeHeadSiteX = calcSiteX;
            snakeHeadSiteY = calcSiteY;
            snakeLength = calField[snakeHeadSiteX, snakeHeadSiteY];
            return snakeLength;
        }
    }

    // FoodCreator 클래스를 만듭니다 : 이 클래스는 맵의 크기 내에서 무작위 위치에 음식을 생성하는 역할을 합니다.
    class FoodCreator
    {

    }

    enum Direction
    {
        Up, Down, Left, Right
    }
}
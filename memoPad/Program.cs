using System.ComponentModel;
using System.Reflection.Metadata;
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
            // 초기 배열
            string[,] gameScreen = new string[40, 30];
            // screen 인스턴스화                    필드 : gameScreen
            Screen screen = new Screen(gameScreen);
            screen.MainField(ref gameScreen);


            // calField.
            int[,] calField = new int[40, 30];
            screen.SetCalField(calField);


            // calField 최초 값.

            // 시작 위치.
            RandomSite randomSIte = new RandomSite();
            int[] startSite = new int[2];
            startSite = randomSIte.GetRandomSite(calField);
            Console.WriteLine($"{startSite[0]} , {startSite[1]}");

            int[] nowSite = new int[2];
            Snake snake = new Snake(startSite);
            Console.WriteLine($"SnakeSite : {snake.SnakeSite[0]} , {snake.SnakeSite[1]}");
            gameScreen[snake.SnakeSite[0], snake.SnakeSite[1]] = "X ";
            calField[snake.SnakeSite[0], snake.SnakeSite[1]] = 1;
            nowSite = snake.SnakeSite;


            //
            Point snakeHead = new Point(nowSite);
            ConsoleKeyInfo directionKey;

            for (int k = 0; k < 10; k++)              // 10회 테스트 입력
            {
                Console.WriteLine("방향 입력.");
                directionKey = Console.ReadKey();
                nowSite = snakeHead.movePoint(directionKey);
                gameScreen[nowSite[0], nowSite[1]] = "X ";
                Console.WriteLine($"SnakeSite : {nowSite[0]} , {nowSite[1]}");
                printArray(gameScreen);
            }



            void printArray<T>(T[,] Array2D)
            {
                Console.WriteLine("\n\n");
                for (int i = 0; i < Array2D.GetLength(0); i++)
                {
                    for (int j = 0; j < Array2D.GetLength(1); j++)
                    {
                        Console.Write(Array2D[i, j]);
                    }
                    Console.Write('\n');
                }
            }


            //
            //Point p = new Point(startSite[0], startSite[1], '*');


        }
    }


    // Snake 클래스를 만듭니다 : 뱀의 상태와 이동, 음식 먹기, 자신의 몸에 부딪혔는지 확인 등의 기능을 담당합니다.
    class Snake
    {
        // 뱀의 상태 : 길이
        // 뱀의 상태 : 머리가 지나온 길을 나머지가 가야함.
        public int length = 1;
        // 임시 : 뱀의 길이 1로 고정.

        private int[] snakeSite;
        public int[] SnakeSite                                                     // 스네이크 위치 조기화
        {
            get { return snakeSite; }
            set { snakeSite = value; }
        }
        public Snake(int[] siteInfo)
        {
            SnakeSite = siteInfo;
        }

        public void snakeHead()
        {

        }




        void Eat()                           // 음식 먹기 : 음식이랑 부딪히면, 음식은 사라지고, 뱀의 몸 길이가 늘어남.
        {
            length++;
        }

        void Collision()                     // 부딪혔는 지 확인 : 게임 오버로 이어짐.
        {
            // calField 에서 몸과 테두리, 장애물을 -3000 으로 할 예정.
            // 머리를 length 로 둘 예정.
            // 만약, calField에서 -2999 보다 큰 음수가 나타난다면, 게임 오버로.
        }
    }
    // FoodCreator 클래스를 만듭니다 : 이 클래스는 맵의 크기 내에서 무작위 위치에 음식을 생성하는 역할을 합니다.

    class FoodCreator : RandomSite
    {
        public void MakeFood(int[,] array2D)
        {
            int[] foodSite = GetRandomSite(array2D);
            // foodSite를 calField와 현재 필드에 나타내기

        }

    }


    // 랜덤한 위치 생성하는 클래스
    class RandomSite
    {
        public int[] GetRandomSite(int[,] array2D)
        {
            int leftEnd = 2;
            int rightEnd = array2D.GetLength(0) - 2;
            int upEnd = 2;
            int bottomEnd = array2D.GetLength(1) - 2;

            Random random = new Random();
            int startX = random.Next(leftEnd, rightEnd);
            int startY = random.Next(upEnd, bottomEnd);

            int[] randomSite = { startX, startY };
            return randomSite;
        }
    }



    // 화면 클래스
    class Screen
    {
        // 게임 화면을 로드. 배열을 필드값으로
        private string[,] screenInfo;
        public string[,] ScreenInfo { get { return screenInfo; } set { screenInfo = value; } }

        public Screen(string[,] screenInfo)
        {
            ScreenInfo = screenInfo;
        }

        public void MainField(ref string[,] gameScreen)                        // 출력과 동시에 배열에 반영.
        {
            for (int row = 0; row < gameScreen.GetLength(0); row++)
            {
                for (int col = 0; col < gameScreen.GetLength(1); col++)
                {
                    if (col == 0 || row == gameScreen.GetLength(0) - 1 || row == 0 || col == gameScreen.GetLength(1) - 1)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        gameScreen[row, col] = "■";
                        Console.Write(gameScreen[row, col]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        gameScreen[row, col] = "  ";
                        Console.Write(gameScreen[row, col]);
                        Console.ResetColor();
                    }
                }
                Console.Write("\n");
            }
        }

        public void SetCalField(int[,] calField)
        {
            for (int row = 0; row < calField.GetLength(0); row++)
            {
                for (int col = 0; col < calField.GetLength(1); col++)
                {
                    if (col == 0 || row == calField.GetLength(0) - 1 || row == 0 || col == calField.GetLength(1) - 1)
                    {
                        calField[row, col] = -3000;
                    }
                    else
                    {
                        calField[row, col] = 0;
                    }
                }
            }
        }
        //public void FieldSum(ref int[,] calField, ref int[,] )
    }

    public class Point
    {
        public int x { get; set; }
        public int y { get; set; }
        //ConsoleKeyInfo direction;

        public Point(int[] siteArray)
        {
            x = siteArray[0];
            y = siteArray[1];
        }

        public int[] movePoint(ConsoleKeyInfo directionKey)
        {
            ConsoleKeyInfo direction = directionKey;
            if (direction.Key == ConsoleKey.UpArrow)
            {
                x -= 1;
            }
            else if (direction.Key == ConsoleKey.DownArrow)
            {
                x += 1;
            }
            else if (direction.Key == ConsoleKey.LeftArrow)
            {
                y -= 1;
            }
            else if (direction.Key == ConsoleKey.RightArrow)
            {
                y += 1;
            }

            return new int[] { x, y };
        }
    }
}
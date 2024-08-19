using System;
using System.Threading;

namespace SnakeConsoleGame
{
    class Program
    {
        static int width = 20;
        static int height = 20;
        static int score = 0;
        static int foodX, foodY;
        static int headX, headY;
        static int velocityX = 0, velocityY = 0;
        static char[,] grid = new char[width, height];

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            InitializeGame();

            while (true)
            {
                DrawGrid();
                Input();
                Logic();
                Thread.Sleep(100); // Control the game speed
            }
        }

        static void InitializeGame()
        {
            headX = width / 2;
            headY = height / 2;
            SpawnFood();
        }

        static void DrawGrid()
        {
            Console.Clear();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == headY && j == headX)
                        Console.Write("O");
                    else if (i == foodY && j == foodX)
                        Console.Write("*");
                    else
                        Console.Write(" ");
                }
                Console.WriteLine();
            }

            Console.WriteLine($"Score: {score}");
        }

        static void Input()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        velocityX = -1;
                        velocityY = 0;
                        break;
                    case ConsoleKey.RightArrow:
                        velocityX = 1;
                        velocityY = 0;
                        break;
                    case ConsoleKey.UpArrow:
                        velocityX = 0;
                        velocityY = -1;
                        break;
                    case ConsoleKey.DownArrow:
                        velocityX = 0;
                        velocityY = 1;
                        break;
                }
            }
        }

        static void Logic()
        {
            headX += velocityX;
            headY += velocityY;

            if (headX < 0) headX = width - 1;
            if (headX >= width) headX = 0;
            if (headY < 0) headY = height - 1;
            if (headY >= height) headY = 0;

            if (headX == foodX && headY == foodY)
            {
                score++;
                SpawnFood();
            }
        }

        static void SpawnFood()
        {
            Random rand = new Random();
            foodX = rand.Next(0, width);
            foodY = rand.Next(0, height);
        }
    }
}

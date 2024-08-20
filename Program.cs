using System;
using System.Collections.Generic;
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
        static List<Tuple<int, int>> snake = new List<Tuple<int, int>>(); //stores coordinates of each part of the snake's body


        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            InitializeGame();

            while (true)
            {
                DrawGrid();
                Input();
                Logic();
                Thread.Sleep(80);
            }
        }

        static void InitializeGame()
        {
            headX = width / 2;
            headY = height / 2;
            snake.Add(Tuple.Create(headX, headY)); 
            SpawnFood();
        }

        static void DrawGrid()
        {
            Console.Clear();
            //

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == headY && j == headX)
                      Console.Write("O");
                    else if (IsSnakeSegment(j, i))
                        Console.Write("o");
                    else if (i == foodY && j == foodX)
                        Console.Write("*");
                    else
                        Console.Write(" ");
                }
                Console.WriteLine();
            }

            Console.WriteLine($"Score: {score}");
        }

              static bool IsSnakeSegment(int x, int y)
        {
            foreach (var segment in snake)
            {
                if (segment.Item1 == x && segment.Item2 == y)
                    return true;
            }
            return false;
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

          int prevX = headX;
            int prevY = headY;

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
                snake.Add(Tuple.Create(prevX, prevY));
            }

            // move the snake's body
            for (int i = snake.Count - 1; i > 0; i--)
            {
                snake[i] = snake[i - 1];
            }

            if (snake.Count > 0)
                snake[0] = Tuple.Create(headX, headY);
        }

        static void SpawnFood()
        {
            Random rand = new Random();
            foodX = rand.Next(0, width);
            foodY = rand.Next(0, height);
        }
    }
}

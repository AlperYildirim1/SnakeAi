using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.ComponentModel;


class Program
{
    static int[,] old_game = new int[30, 30];
    static int[,] game = new int[30, 30];
    static int[,] board = new int[30, 30];
    // If someone adds a hamiltonian path generator method and combine it, this can be fastest snake game AI
    static void Main(string[] args)
    {
        Random rnd = new Random();

        int a = rnd.Next(1, game.GetLength(0) - 20);
        Console.WriteLine(a);
        int b = rnd.Next(1, game.GetLength(1) - 20);
        Console.WriteLine(b);
        int a1 = rnd.Next(1, game.GetLength(0) - 1);
        int b1 = rnd.Next(1, game.GetLength(1) - 1);
        List<Point> snake_positions = new List<Point>();
        snake_positions.Add(new Point(a, b));
        snake_positions.Add(new Point(a + snake_positions.Count, b));
        snake_positions.Add(new Point(a + snake_positions.Count, b));

        while (a == a1)
        {
            a1 = rnd.Next(game.GetLength(0));
        }
        bool bo = true;
        while (bo == false)
        {
            for (int i = 0; i < snake_positions.Count; i++)
            {
                if (a1 == snake_positions[i].X && b == snake_positions[i].Y)
                {
                    a1 = rnd.Next(game.GetLength(0));
                    bo = false;
                    break;
                }
                else
                {
                    bo = true;
                }
            }
        }
        for (int i = 0; i < game.GetLength(0); i++)
        {
            for (int j = 0; j < game.GetLength(1); j++)
            {
                if (i == 0 || i == game.GetLength(0) - 1 || j == 0 || j == game.GetLength(1) - 1)
                {
                    game[i, j] = -1;
                }
            }
        }
        for (int i = 0; i < 30; i++)
        {
            board[0, i] = -1;
            board[29, i] = -1;
            board[i, 0] = -1;
            board[i, 29] = -1;
        }
        int num = 3;
        for (int j = 1; j < 29; j++)
        {
            if (j % 2 == 0)
            {
                for (int i = 27; i >= 1; i--)
                {
                    board[i + 1, j] = num;
                    num++;
                }
            }
            else
            {
                for (int i = 1; i < 28; i++)
                {
                    board[i + 1, j] = num;
                    num++;
                }
            }
        }
        int n = 759;
        int i2 = 1; int j2 = 28;
        while (board[i2, j2] != -1)
        {
            board[i2, j2] = n;
            n++;
            j2--;
        }
        Array.Copy(game, old_game, game.Length);
        print(game);
        game[15, 15] = 2;



        for (int i = 0; i < snake_positions.Count; i++)
        {
            int x = snake_positions[i].X;
            int y = snake_positions[i].Y;
            game[x, y] = 1;
        }
        Console.WriteLine("write 2 to play manually , 1 to spectate, controls: w a s d");
        int v = 3;
        int h = Convert.ToInt32(Console.ReadLine());
        int skip = 0;
        while (true)
        {

            int food_location = 0;
            if (board[snake_positions.Last().X, snake_positions.Last().Y] == 3)
            {
                skip = 0;
            }

            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    if (game[i, j] == 2)
                    {
                        food_location = board[i, j];

                        break;
                    }
                }
            }
            int current = board[snake_positions.Last().X, snake_positions.Last().Y];
            int w = board[snake_positions.Last().X - 1, snake_positions.Last().Y];
            int d = board[snake_positions.Last().X, snake_positions.Last().Y + 1];
            int s = board[snake_positions.Last().X + 1, snake_positions.Last().Y];
            int a3 = board[snake_positions.Last().X, snake_positions.Last().Y - 1];
            int[] nums = { w, d, s, a3 };
            Array.Sort(nums);
            int secondLargest = nums[2];
            int p = Math.Max(Math.Max(w, d), Math.Max(s, a3));






            if (h == 1)
            {
                if (board[snake_positions.Last().X, snake_positions.Last().Y] + 1 == board[snake_positions.Last().X + 1, snake_positions.Last().Y])
                {

                    v = 3;
                }
                else if (board[snake_positions.Last().X, snake_positions.Last().Y] + 1 == board[snake_positions.Last().X - 1, snake_positions.Last().Y]) // %50 ihtimalle buradan başlayıp başta hata verir.
                {
                    v = 1;
                }
                else if (board[snake_positions.Last().X + 1, snake_positions.Last().Y] == 3)
                {
                    v = 3;
                }
                else if (board[snake_positions.Last().X, snake_positions.Last().Y] + 1 == board[snake_positions.Last().X, snake_positions.Last().Y + 1])
                {
                    v = 2;
                }
                else if (board[snake_positions.Last().X, snake_positions.Last().Y] + 1 == board[snake_positions.Last().X, snake_positions.Last().Y - 1])
                {
                    v = 4;
                }
            }

            int u = 720;
            if (p <= food_location && p != -1)
            {
                if (p == w && snake_positions.Count + skip + w - current - 1 < u && h == 1)
                {
                    v = 1;
                    skip = skip + w - current - 1;

                }
                else if (p == d && snake_positions.Count + skip + d - current - 1 < u && h == 1)
                {
                    v = 2;
                    skip = skip + d - current - 1;
                }
                else if (p == s && snake_positions.Count + skip + s - current - 1 < u && h == 1)
                {
                    v = 3;
                    skip = skip + s - current - 1;

                }
                else if (p == a3 && snake_positions.Count + skip + a3 - current - 1 < u && h == 1)
                {
                    v = 4;
                    skip = skip + a3 - current - 1;
                }
                if (current == 786)
                {
                    v = 3;
                }


            }
            if (snake_positions.Last().X == 2 && h == 1 && secondLargest <= food_location && p != -1)
            {
                if (snake_positions.Count + skip + d - current - 1 < u && d != -1 && snake_positions.Last().X != 1)
                {
                    v = 2;
                    skip = skip + d - current - 1;
                }
            }


            else if (current > food_location && p == w && snake_positions.Count + skip + w - current - 1 < u && w != -1 && h == 1)
            {
                v = 1;
                skip = skip + w - current - 1;
            }
            else if (current > food_location && p == d && snake_positions.Count + skip + d - current - 1 < u && d != -1 && h == 1)
            {
                v = 2;
                skip = skip + d - current - 1;
            }
            else if (current > food_location && p == s && snake_positions.Count + skip + s - current - 1 < u && s != -1 && h == 1)
            {
                v = 3;
                skip = skip + s - current - 1;

            }
            else if (current > food_location && p == a3 && snake_positions.Count + skip + a3 - current - 1 < u && a != -1 && h == 1)
            {
                v = 4;
                skip = skip + a3 - current - 1;
            }
            if (current == 786)
            {
                v = 3;
            }
            if (snake_positions.Last().X == 2 && h == 1 && current > food_location)
            {
                if (snake_positions.Count + skip + d - current - 1 < 710 && d != -1 && snake_positions.Last().X != 1)
                {
                    v = 2;
                    skip = skip + d - current - 1;
                }
            }
            if (h == 2)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.D && v != 4)
                    {
                        v = 2;

                    }
                    else if (key.Key == ConsoleKey.S && v != 1)
                    {
                        v = 3;

                    }
                    else if (key.Key == ConsoleKey.A && v != 2)
                    {
                        v = 4;
                    }
                    else if (key.Key == ConsoleKey.W && v != 3)
                    {
                        v = 1;
                    }
                }

            }


            switch (v)
            {
                case 1:
                    {
                        move_snake_up(ref snake_positions);


                        // Console.Clear();
                        // print(game);
                        Thread.Sleep(50);
                        break;
                    }
                case 2:
                    {
                        move_snake_right(ref snake_positions);

                        // Console.Clear();
                        //print(game);
                        Thread.Sleep(50);
                        break;
                    }
                case 3:
                    {
                        move_snake_down(ref snake_positions);

                        // Console.Clear();
                        //     print(game);
                        Thread.Sleep(50);


                        break;
                    }
                case 4:
                    {
                        move_snake_left(ref snake_positions);

                        //   Console.Clear();
                        //      print(game);
                        Thread.Sleep(50);
                        break;
                    }
            }


            cursor(game, old_game);
        }


    }
    static void move_snake_down(ref List<Point> snake_positions)
    {

        if (game[snake_positions.Last().X + 1, snake_positions.Last().Y] == 0)
        {
            snake_positions.Add(new Point(snake_positions.Last().X + 1, snake_positions.Last().Y));
            int x = snake_positions[0].X;
            int y = snake_positions[0].Y;
            game[x, y] = 0;
            snake_positions.RemoveAt(0);

            for (int i = 0; i < snake_positions.Count; i++)
            {
                x = snake_positions[i].X;
                y = snake_positions[i].Y;
                game[x, y] = 1;
            }
        }
        else if (game[snake_positions.Last().X + 1, snake_positions.Last().Y] == 2)
        {

            snake_positions.Add(new Point(snake_positions.Last().X + 1, snake_positions.Last().Y));
            for (int i = 0; i < snake_positions.Count; i++)
            {
                int x = snake_positions[i].X;
                int y = snake_positions[i].Y;
                game[x, y] = 1;

            }
            generate_food(ref snake_positions, game);
        }
        else
        {


            Console.WriteLine("Game Over");


            Console.ReadLine();
        }
    }
    static void move_snake_up(ref List<Point> snake_positions)
    {
        if (game[snake_positions.Last().X - 1, snake_positions.Last().Y] == 0)
        {
            snake_positions.Add(new Point(snake_positions.Last().X - 1, snake_positions.Last().Y));
            int x = snake_positions[0].X;
            int y = snake_positions[0].Y;
            game[x, y] = 0;
            snake_positions.RemoveAt(0);

            for (int i = 0; i < snake_positions.Count; i++)
            {
                x = snake_positions[i].X;
                y = snake_positions[i].Y;
                game[x, y] = 1;
            }
        }
        else if (game[snake_positions.Last().X - 1, snake_positions.Last().Y] == 2)
        {
            snake_positions.Add(new Point(snake_positions.Last().X - 1, snake_positions.Last().Y));
            for (int i = 0; i < snake_positions.Count; i++)
            {
                int x = snake_positions[i].X;
                int y = snake_positions[i].Y;
                game[x, y] = 1;
            }
            generate_food(ref snake_positions, game);
        }
        else
        {


            Console.WriteLine("Game Over");
            Console.ReadLine();
        }
    }
    static void move_snake_right(ref List<Point> snake_positions)
    {
        if (game[snake_positions.Last().X, snake_positions.Last().Y + 1] == 0)
        {
            snake_positions.Add(new Point(snake_positions.Last().X, snake_positions.Last().Y + 1));
            int x = snake_positions[0].X;
            int y = snake_positions[0].Y;
            game[x, y] = 0;
            snake_positions.RemoveAt(0);

            for (int i = 0; i < snake_positions.Count; i++)
            {
                x = snake_positions[i].X;
                y = snake_positions[i].Y;
                game[x, y] = 1;
            }
        }
        else if (game[snake_positions.Last().X, snake_positions.Last().Y + 1] == 2)
        {
            snake_positions.Add(new Point(snake_positions.Last().X, snake_positions.Last().Y + 1));
            for (int i = 0; i < snake_positions.Count; i++)
            {
                int x = snake_positions[i].X;
                int y = snake_positions[i].Y;
                game[x, y] = 1;
            }
            generate_food(ref snake_positions, game);
        }
        else
        {


            Console.WriteLine("Game Over");
            Console.ReadLine();
        }
    }
    static void move_snake_left(ref List<Point> snake_positions)
    {

        if (game[snake_positions.Last().X, snake_positions.Last().Y - 1] == 0)
        {
            snake_positions.Add(new Point(snake_positions.Last().X, snake_positions.Last().Y - 1));
            int x = snake_positions[0].X;
            int y = snake_positions[0].Y;
            game[x, y] = 0;
            snake_positions.RemoveAt(0);

            for (int i = 0; i < snake_positions.Count; i++)
            {
                x = snake_positions[i].X;
                y = snake_positions[i].Y;
                game[x, y] = 1;
            }
        }
        else if (game[snake_positions.Last().X, snake_positions.Last().Y - 1] == 2)
        {
            snake_positions.Add(new Point(snake_positions.Last().X, snake_positions.Last().Y - 1));
            for (int i = 0; i < snake_positions.Count; i++)
            {
                int x = snake_positions[i].X;
                int y = snake_positions[i].Y;
                game[x, y] = 1;
            }
            generate_food(ref snake_positions, game);
        }
        else
        {


            Console.WriteLine("Game Over");
            Console.ReadLine();
        }
    }
    static void recursion(ref List<Point> snake_positions, int v = 2)
    {



    }
    static void generate_food(ref List<Point> snake_positions, int[,] game)
    {
        Random rnd = new Random();
        int a1, b1;
        bool placed = false;

        while (!placed)
        {
            a1 = rnd.Next(1, game.GetLength(0) - 1);
            b1 = rnd.Next(1, game.GetLength(1) - 1);

            bool isFoodOnSnake = false;
            foreach (Point p in snake_positions)
            {
                if (a1 == p.X && b1 == p.Y)
                {
                    isFoodOnSnake = true;
                    break;
                }
            }

            if (!isFoodOnSnake)
            {
                game[a1, b1] = 2;
                placed = true;
            }
        }
    }

    static void print(int[,] game)
    {
        for (int i = 0; i < 30; i++)
        {
            for (int j = 0; j < 30; j++)
            {

                if (game[i, j] == -1 || game[i, j] == 0)
                {
                    if (board[i, j] == -1 && i == 0)
                    {
                        Console.Write("_ ");
                    }
                    else if (board[i, j] == -1 && i == 29)
                    {
                        Console.Write("* ");
                    }
                    else
                    {
                        Console.Write(" ");
                    }

                }
                else
                    Console.Write(game[i, j] + " ");
            }
            Console.WriteLine("                           l");
        }
    }
    static void cursor(int[,] game, int[,] old_game)
    {

        for (int i = 1; i < 29; i++)
        {
            for (int j = 1; j < 29; j++)
            {
                if (game[i, j] != old_game[i, j])
                {
                    Console.SetCursorPosition(j * 2 - 2, i + 2);

                    if (game[i, j] == -1 || game[i, j] == 0)
                    {
                        Console.Write(" ");
                    }
                    else
                        Console.Write(game[i, j] + " ");
                    old_game[i, j] = game[i, j];
                }
            }
        }
    }
}
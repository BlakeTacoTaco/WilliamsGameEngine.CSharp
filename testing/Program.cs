using System;
using System.Collections.Generic;
class HelloWorld
{
    static List<List<int>> SwipeDown(List<List<int>> board, ref int score)
    {
        int iterations = 0;
        for (int x = 0; x < 4; x++)
        {
            for (int y = 2; y >= 0; y--)
            {
                bool moved = false;
                int value = board[x][y];
                board[x][y] = 0;
                for (int i = y; i < 4; i++)
                {
                    if (board[x][i] == value)
                    {
                        board[x][i] *= 2;
                        score += board[i][y];
                        moved = true;
                        break;
                    }
                    if (board[x][i] != 0)
                    {
                        board[x][i - 1] = value;
                        moved = true;
                        break;
                    }
                }
                if (!moved)
                {
                    board[x][3] = value;
                }
            }
        }
        return board;
    }
    static List<List<int>> SwipeUp(List<List<int>> board, ref int score)
    {
        int iterations = 0;
        for (int x = 0; x < 4; x++)
        {
            for (int y = 1; y < 4; y++)
            {
                bool moved = false;
                int value = board[x][y];
                board[x][y] = 0;
                for (int i = y; i >= 0; i--)
                {
                    if (board[x][i] == value)
                    {
                        board[x][i] *= 2;
                        score += board[i][y];
                        moved = true;
                        break;
                    }
                    if (board[x][i] != 0)
                    {
                        board[x][i + 1] = value;
                        moved = true;
                        break;
                    }
                }
                if (!moved)
                {
                    board[x][0] = value;
                }
            }
        }
        return board;
    }
    static List<List<int>> SwipeLeft(List<List<int>> board, ref int score)
    {
        int iterations = 0;
        for (int y = 0; y < 4; y++)
        {
            for (int x = 1; x < 4; x++)
            {
                bool moved = false;
                int value = board[x][y];
                board[x][y] = 0;
                for (int i = x; i >= 0; i--)
                {
                    if (board[i][y] == value)
                    {
                        board[i][y] *= 2;
                        score += board[i][y];
                        moved = true;
                        break;
                    }
                    if (board[i][y] != 0)
                    {
                        board[i + 1][y] = value;
                        moved = true;
                        break;
                    }
                }
                if (!moved)
                {
                    board[0][y] = value;
                }
            }
        }
        return board;
    }
    static List<List<int>> SwipeRight(List<List<int>> board, ref int score)
    {
        int iterations = 0;
        for (int y = 0; y < 4; y++)
        {
            for (int x = 2; x >= 0; x--)
            {
                bool moved = false;
                int value = board[x][y];
                board[x][y] = 0;
                for (int i = x; i < 4; i++)
                {
                    if (board[i][y] == value)
                    {
                        board[i][y] *= 2;
                        score += board[i][y];
                        moved = true;
                        break;
                    }
                    if (board[i][y] != 0)
                    {
                        board[i - 1][y] = value;
                        moved = true;
                        break;
                    }
                }
                if (!moved)
                {
                    board[3][y] = value;
                }
            }
        }
        return board;
    }
    static List<List<int>> AddTile(List<List<int>> board)
    {
        //makes a list of all the available tiles
        List<int> empty = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (board[i][j] == 0) { empty.Add(i + (j * 4)); }
            }
        }

        //doesnt do anything if the board is full
        if (empty.Count == 0) { return board; }

        //figures out what number to Add
        Random random = new Random();
        int number = (random.Next(2) + 1) * 2;

        //selects a random tile to put the number on
        int selected = empty[random.Next(empty.Count)];
        board[selected % 4][selected / 4] = number;

        return board;
    }
    static void PrintBoard(List<List<int>> board)
    {
        Console.WriteLine("+---------------------+\n|                     |");
        for (int i = 0; i < 4; i++)
        {
            Console.Write("| ");
            for (int j = 0; j < 4; j++)
            {
                if (board[j][i] != 0)
                {
                    string pixel = Convert.ToString(board[j][i]);
                    int pixelLength = pixel.Length;
                    int n = 0;
                    bool right = true;
                    do
                    {
                        n++;
                        if (right) { pixel += " "; }
                        else { pixel = " " + pixel; }
                        right = !right;
                    }
                    while (n < 5 - pixelLength);
                    switch (board[j][i])
                    {
                        case 2:
                            Console.BackgroundColor = ConsoleColor.Red;
                            break;
                        case 4:
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            break;
                        case 8:
                            Console.BackgroundColor = ConsoleColor.Green;
                            break;
                        case 16:
                            Console.BackgroundColor = ConsoleColor.Cyan;
                            break;
                        case 32:
                            Console.BackgroundColor = ConsoleColor.Blue;
                            break;
                        case 64:
                            Console.BackgroundColor = ConsoleColor.Magenta;
                            break;
                        case 128:
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            break;
                        case 256:
                            Console.BackgroundColor = ConsoleColor.Cyan;
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            break;
                        case 512:
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            break;
                        case 1024:
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            break;
                        default:
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            break;
                    }
                    Console.Write(pixel);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else { Console.Write("     "); }
            }
            Console.Write("|");
            Console.WriteLine("\n|                     |");
        }
        Console.WriteLine("+---------------------+");
    }
    static void Main()
    {
        int score = 0;
        List<List<int>> board = new List<List<int>>()
    {
        new List<int>() { 0,0,0,0 },
        new List<int>() { 0,0,0,0 },
        new List<int>() { 0,0,0,0 },
        new List<int>() { 0,0,0,0 }
    };
        Console.BackgroundColor = ConsoleColor.Black;
        while (true)
        {
            Console.Clear();
            board = AddTile(board);
            Console.WriteLine(score);
            PrintBoard(board);
            char move = Console.ReadKey().KeyChar;
            switch (move)
            {
                case 'w':
                    board = SwipeUp(board, ref score);
                    break;
                case 'a':
                    board = SwipeLeft(board, ref score);
                    break;
                case 's':
                    board = SwipeDown(board, ref score);
                    break;
                case 'd':
                    board = SwipeRight(board, ref score);
                    break;
                case 'r':
                    board = new List<List<int>>()
                {
                    new List<int>() { 0,0,0,0 },
                    new List<int>() { 0,0,0,0 },
                    new List<int>() { 0,0,0,0 },
                    new List<int>() { 0,0,0,0 }
                };
                    score = 0;
                    break;
            }
        }
    }
}

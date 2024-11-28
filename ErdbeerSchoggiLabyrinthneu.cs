using System;
using System.Threading;

namespace Erdbeerschoggi.Labyrinth
{
    internal class Program
    {
        
        static int playerX;
        static int playerY;
        static string playerSymbol = "O";

        
        static int currentLevel = 0;

        // Levels
        static char[][,] Mazes = new char[][,]
        {
            // Level 1
            new char[,]
            {
                { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
                { 'S', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', '#', '#', '#' },
                { '#', '#', '#', ' ', '#', '#', '#', '#', '#', ' ', '#', '#', '#', '#' },
                { '#', ' ', ' ', ' ', ' ', ' ', ' ', '#', '#', ' ', ' ', ' ', ' ', '#' },
                { '#', '#', '#', '#', '#', '#', ' ', '#', '#', '#', ' ', '#', ' ', '#' },
                { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#' },
                { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', 'E', '#' }
            },
            // Level 2
            new char[,]
            {
              { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
              { 'S', ' ', ' ', ' ', '#', ' ', '#', '#', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
              { '#', '#', '#', ' ', '#', ' ', '#', '#', ' ', '#', '#', '#', '#', '#', '#' },
              { '#', ' ', ' ', ' ', '#', ' ', '#', ' ', ' ', '#', ' ', ' ', ' ', ' ', '#' },
              { '#', ' ', '#', '#', '#', ' ', '#', '#', '#', '#', ' ', '#', ' ', '#', '#' },
              { '#', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', '#', ' ', '#', ' ', '#', '#' },
              { '#', '#', '#', ' ', '#', '#', ' ', '#', ' ', '#', ' ', '#', ' ', '#', '#' },
              { '#', ' ', '#', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#', ' ', ' ', '#' },
              { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', 'E', '#' }

            },
            // Level 3
            new char[,]
            {
                { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
                { 'S', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', '#', ' ', '#', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', '#' },
                { '#', '#', '#', ' ', '#', ' ', '#', '#', '#', ' ', '#', '#', ' ', '#', ' ', '#', ' ', '#', '#', '#', ' ', '#', '#', ' ', '#' },
                { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#', ' ', ' ', '#' },
                { '#', ' ', '#', '#', '#', '#', '#', ' ', '#', '#', '#', '#', '#', ' ', '#', '#', '#', ' ', ' ', ' ', ' ', ' ', ' ', '#', '#' },
                { '#', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#', '#', '#', '#', '#', ' ', ' ', ' ', '#' },
                { '#', ' ', '#', ' ', '#', '#', '#', '#', '#', '#', '#', '#', '#', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', '#', '#', '#', '#' },
                { '#', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', '#', '#', '#', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#' },
                { '#', '#', '#', ' ', '#', ' ', '#', '#', '#', '#', '#', ' ', '#', ' ', ' ', ' ', '#', ' ', '#', '#', '#', '#', '#', ' ', '#' },
                { '#', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', '#' },
                { '#', '#', '#', '#', '#', '#', '#', '#', '#', ' ', '#', '#', '#', ' ', '#', '#', '#', '#', '#', '#', '#', '#', ' ', ' ', '#' },
                { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
                { '#', ' ', '#', '#', '#', '#', ' ', '#', '#', ' ', '#', ' ', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
                { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
                { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', 'E', '#' }
            }
        };

        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            

            while (currentLevel < Mazes.Length)
            {
                Console.Clear();
                FindStartPosition();
                PlayLevel();
                currentLevel++;
            }

            
            Console.Clear();
           
        }

        static void PlayLevel()
        {
            while (true)
            {
                Console.Clear();
                DrawMaze();

                Console.SetCursorPosition(playerX, playerY);
                Console.Write(playerSymbol);

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);

                    int newX = playerX;
                    int newY = playerY;

                    
                    if (key.Key == ConsoleKey.W) newY--;
                    else if (key.Key == ConsoleKey.S) newY++;
                    else if (key.Key == ConsoleKey.A) newX--;
                    else if (key.Key == ConsoleKey.D) newX++;

                    
                    if (newX >= 0 && newX < Mazes[currentLevel].GetLength(1) &&
                        newY >= 0 && newY < Mazes[currentLevel].GetLength(0) &&
                        (Mazes[currentLevel][newY, newX] == ' ' || Mazes[currentLevel][newY, newX] == 'S' || Mazes[currentLevel][newY, newX] == 'E'))
                    {
                        playerX = newX;
                        playerY = newY;

                        if (Mazes[currentLevel][playerY, playerX] == 'E')
                        {
                            Console.Clear();
                            Console.WriteLine("Okidoki, hier ist deine Erdbeerschoggi!");
                            Thread.Sleep(1000);
                            break;
                        }
                    }
                }

                Thread.Sleep(75);
            }
        }

        static void DrawMaze()
        {
            for (int y = 0; y < Mazes[currentLevel].GetLength(0); y++)
            {
                for (int x = 0; x < Mazes[currentLevel].GetLength(1); x++)
                {
                    Console.Write(Mazes[currentLevel][y, x]);
                }
                Console.WriteLine();
            }
        }

        static void FindStartPosition()
        {
            for (int y = 0; y < Mazes[currentLevel].GetLength(0); y++)
            {
                for (int x = 0; x < Mazes[currentLevel].GetLength(1); x++)
                {
                    if (Mazes[currentLevel][y, x] == 'S')
                    {
                        playerX = x;
                        playerY = y;
                        return;
                    }
                }
            }
        }

    }
}













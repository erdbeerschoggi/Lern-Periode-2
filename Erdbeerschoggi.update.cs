using System;
using System.Collections.Generic;
using System.Threading;

class Programm
{
    static int playerX;
    static int playerY;
    static string playerSymbol = "O";

    // Aktuelles Level
    static int currentLevel = 0;

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

    static int playerX = 1;
    static int playerY = 1;

    static void Main()
    {
        int berriesCollected = BerryCollectionStage();

        if (berriesCollected > 0)
        {
            Console.Clear();
            Console.WriteLine("Great! You've collected all the berries. Now complete the maze!");
            Thread.Sleep(2000);
            MazeStage();
        }
    }

    static int BerryCollectionStage()
    {
        int landingGroundLevel = 15;
        int ceilingLevel = 1;
        int jumpHeight = 3;
        int playerX = 10;
        int playerY = landingGroundLevel - 1;
        char playerSymbol = 'O';
        char berrySymbol = '*';
        bool isJumping = false;
        int jumpProgress = 0;

        Random random = new Random();
        List<int> spikes = new List<int>();
        List<int> berries = new List<int>();
        int spikeCooldown = 0;
        int berryCooldown = 0;
        int berriesCollected = 0;
        int totalBerries = 5;

        Console.CursorVisible = false;

        while (berriesCollected < totalBerries)
        {
            Console.Clear();

            for (int x = 0; x < Console.WindowWidth; x++)
            {
                Console.SetCursorPosition(x, ceilingLevel);
                Console.Write("-");
            }

            for (int x = 0; x < Console.WindowWidth; x++)
            {
                Console.SetCursorPosition(x, landingGroundLevel);
                Console.Write("-");
            }

            Console.SetCursorPosition(playerX, playerY);
            Console.Write(playerSymbol);

            for (int i = 0; i < spikes.Count; i++)
            {
                int spikeX = spikes[i];
                DrawSpike(spikeX, landingGroundLevel);

                if (spikeX == playerX && playerY == landingGroundLevel - 1)
                {
                    GameOver();
                    return 0;
                }

                spikes[i]--;
            }

            spikes.RemoveAll(spikeX => spikeX < 0);

            if (spikeCooldown == 0 && random.Next(0, 10) < 2)
            {
                spikes.Add(Console.WindowWidth - 1);
                spikeCooldown = 10;
            }

            if (spikeCooldown > 0) spikeCooldown--;

            for (int i = 0; i < berries.Count; i++)
            {
                int berryX = berries[i];
                int berryY = landingGroundLevel - 1;

                Console.SetCursorPosition(berryX, berryY);
                Console.Write(berrySymbol);

                if (berryX == playerX && playerY == berryY)
                {
                    berriesCollected++;
                    berries[i] = -1;
                }

                berries[i]--;
            }

            berries.RemoveAll(berryX => berryX < 0);

            if (berryCooldown == 0 && random.Next(0, 10) < 1)
            {
                berries.Add(Console.WindowWidth - 1);
                berryCooldown = 15;
            }

            if (berryCooldown > 0) berryCooldown--;

            Console.SetCursorPosition(0, 0);
            Console.Write($"Berries Collected: {berriesCollected}/{totalBerries}");

            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Spacebar && !isJumping && playerY == landingGroundLevel - 1)
                {
                    isJumping = true;
                    jumpProgress = jumpHeight;
                }
            }

            if (isJumping)
            {
                if (jumpProgress > 0)
                {
                    playerY--;
                    jumpProgress--;
                }
                else
                {
                    isJumping = false;
                }
            }
            else if (playerY < landingGroundLevel - 1)
            {
                playerY++;
            }

            Thread.Sleep(100);
        }

        return berriesCollected;
    }
    static void Main(string[] args)
    {
        Console.CursorVisible = false;

        // Hauptschleife für alle Levels
        while (currentLevel < Mazes.Length)
        {
            Console.Clear();
            FindStartPosition();
            PlayLevel();
            currentLevel++;
        }

        // Spielende
        Console.Clear();
        Console.WriteLine("Okidoki");
    }


    static void MazeStage()
    {
        Console.Clear();

        while (true)
        {
            DrawMaze();
            DrawPlayer();

            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                MovePlayer(key.Key);
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

    static void DrawPlayer()
    {
        Console.SetCursorPosition(playerX, playerY);
        Console.Write("O");
    }

    static void MovePlayer(ConsoleKey key)
    {
        int newX = playerX;
        int newY = playerY;

        switch (key)
        {
            case ConsoleKey.W: newY--; break;
            case ConsoleKey.S: newY++; break;
            case ConsoleKey.A: newX--; break;
            case ConsoleKey.D: newX++; break;
        }

        if (maze[newY, newX] != '#')
        {
            playerX = newX;
            playerY = newY;

            if (maze[playerY, playerX] == 'E')
            {
                Console.Clear();
                Console.WriteLine("Congratulations! You completed the maze and collected all berries!");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }
    }

    static void DrawSpike(int x, int groundLevel)
    {
        Console.SetCursorPosition(x, groundLevel);
        Console.Write("^");
    }

    static void GameOver()
    {
        Console.Clear();
        Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2);
        Console.WriteLine("Game Over!");
        Thread.Sleep(2000);
        Environment.Exit(0);
    }
}





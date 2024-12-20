using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    
    static char[,] level1Maze = {
        { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
        { 'S', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#' },
        { '#', '#', '#', ' ', '#', ' ', '#', '#', '#', ' ', '#', ' ', '#', '#', ' ', '#', ' ', '#', ' ', '#' },
        { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', '#', ' ', '#' },
        { '#', ' ', '#', '#', '#', ' ', '#', ' ', '#', '#', '#', ' ', '#', '#', '#', '#', ' ', '#', ' ', '#' },
        { '#', ' ', '#', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#' },
        { '#', ' ', '#', ' ', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', ' ', '#', ' ', '#', '#', '#' },
        { '#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
        { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', 'E', '#' }
    };

    static char[,] level2Maze = {
              { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
              { 'S', ' ', ' ', ' ', '#', ' ', '#', '#', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
              { '#', '#', '#', ' ', '#', ' ', '#', '#', ' ', '#', '#', '#', '#', '#', '#' },
              { '#', ' ', ' ', ' ', '#', ' ', '#', ' ', ' ', '#', ' ', ' ', ' ', ' ', '#' },
              { '#', ' ', '#', '#', '#', ' ', '#', '#', '#', '#', ' ', '#', ' ', '#', '#' },
              { '#', ' ', ' ', ' ', '#', ' ', ' ', ' ', ' ', '#', ' ', '#', ' ', '#', '#' },
              { '#', '#', '#', ' ', '#', '#', ' ', '#', ' ', '#', ' ', '#', ' ', '#', '#' },
              { '#', ' ', '#', ' ', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#', ' ', ' ', '#' },
              { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', 'E', '#' }

    };
    static char[,] level3Maze = {
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
        };


static char[,] currentMaze;
    static int playerX = 1;
    static int playerY = 1;

    
    static void Main()
    {
        Console.Clear();
        Console.WriteLine("Welcome to Erdbeerschoggi! Collect strawberries and complete three levels!");
        Thread.Sleep(3000);

        
        int berriesCollected = BerryCollectionStage(5);
        if (berriesCollected > 0)
        {
            Console.Clear();
            Console.WriteLine("Level 1 Schoggimaze: Complete the Schoggimaze!");
            Thread.Sleep(2000);
            currentMaze = level1Maze;
            MazeStage();
        }

        
        TransitionToNextLevel(2, level2Maze, 10);

        
        TransitionToNextLevel(3, level3Maze, 15);
    }

    
    static void TransitionToNextLevel(int level, char[,] maze, int berriesNeeded)
    {
        Console.Clear();
        Console.WriteLine($"Congratulations on completing Level {level - 1}! Level {level} starts now!");
        Thread.Sleep(3000);

        int berriesCollected = BerryCollectionStage(berriesNeeded);
        if (berriesCollected > 0)
        {
            Console.Clear();
            Console.WriteLine($"Level {level} Schoggimaze: Complete the Schoggimaze!");
            Thread.Sleep(2000);
            currentMaze = maze;
            playerX = 1;
            playerY = 1;
            MazeStage();
        }
    }

    
    static int BerryCollectionStage(int totalBerries)
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

        Console.CursorVisible = false;

        
        while (berriesCollected < totalBerries)
        {
            DrawBerryStage(landingGroundLevel, ceilingLevel, playerX, playerY, playerSymbol, berrySymbol, spikes, berries);
            UpdateSpikesAndBerries(random, spikes, berries, ref spikeCooldown, ref berryCooldown, playerX, playerY, ref berriesCollected, landingGroundLevel);

            HandlePlayerJump(ref isJumping, ref jumpProgress, ref playerY, landingGroundLevel);

            Console.SetCursorPosition(0, 0);
            Console.Write($"Berries Collected: {berriesCollected}/{totalBerries}");
            Thread.Sleep(100);
        }

        return berriesCollected;
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

            if (currentMaze[playerY, playerX] == 'E')
            {
                Console.Clear();
                Console.WriteLine("Oki Doki, hier ist deine Erdbeerschoggi!");
                Thread.Sleep(2000);
                break;
            }

            Thread.Sleep(75);
        }
    }

    
    static void DrawMaze()
    {
        for (int y = 0; y < currentMaze.GetLength(0); y++)
        {
            for (int x = 0; x < currentMaze.GetLength(1); x++)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(currentMaze[y, x]);
            }
        }
    }

    static void DrawPlayer()
    {
        Console.SetCursorPosition(playerX, playerY);
        Console.Write("O");
    }

    static void DrawBerryStage(int landingGroundLevel, int ceilingLevel, int playerX, int playerY, char playerSymbol, char berrySymbol, List<int> spikes, List<int> berries)
    {
        Console.Clear();
        for (int x = 0; x < Console.WindowWidth; x++)
        {
            Console.SetCursorPosition(x, ceilingLevel);
            Console.Write("-");
            Console.SetCursorPosition(x, landingGroundLevel);
            Console.Write("-");
        }

        Console.SetCursorPosition(playerX, playerY);
        Console.Write(playerSymbol);

        foreach (int spikeX in spikes)
        {
            DrawSpike(spikeX, landingGroundLevel);
        }

        foreach (int berryX in berries)
        {
            Console.SetCursorPosition(berryX, landingGroundLevel - 1);
            Console.Write(berrySymbol);
        }
    }

    
    static void MovePlayer(ConsoleKey key)
    {
        int newX = playerX;
        int newY = playerY;

        switch (key)
        {
            case ConsoleKey.LeftArrow: newX--; break;
            case ConsoleKey.RightArrow: newX++; break;
            case ConsoleKey.UpArrow: newY--; break;
            case ConsoleKey.DownArrow: newY++; break;
        }

        if (currentMaze[newY, newX] != '#')
        {
            playerX = newX;
            playerY = newY;
        }
    }

    
    static void UpdateSpikesAndBerries(Random random, List<int> spikes, List<int> berries, ref int spikeCooldown, ref int berryCooldown, int playerX, int playerY, ref int berriesCollected, int landingGroundLevel)
    {
        for (int i = 0; i < spikes.Count; i++)
        {
            spikes[i]--;
            if (spikes[i] == playerX && playerY == landingGroundLevel - 1)
            {
                GameOver();
            }
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
            berries[i]--;
            if (berries[i] == playerX && playerY == landingGroundLevel - 1)
            {
                berriesCollected++;
                berries[i] = -1;
            }
        }

        berries.RemoveAll(berryX => berryX < 0);

        if (berryCooldown == 0 && random.Next(0, 10) < 1)
        {
            berries.Add(Console.WindowWidth - 1);
            berryCooldown = 15;
        }

        if (berryCooldown > 0) berryCooldown--;
    }

    static void HandlePlayerJump(ref bool isJumping, ref int jumpProgress, ref int playerY, int landingGroundLevel)
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Spacebar && !isJumping && playerY == landingGroundLevel - 1)
            {
                isJumping = true;
                jumpProgress = 3; 
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


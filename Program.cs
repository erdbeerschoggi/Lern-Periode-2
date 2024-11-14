class Program
{
    static void Main()
    {
        // Spielparameter
        int landingGroundLevel = 15;
        int deathGroundLevel = Console.WindowHeight - 2;
        int ceilingLevel = 1;
        int jumpHeight = 3;
        int playerX = 10;
        int playerY = landingGroundLevel - 1;
        char playerSymbol = 'O';
        bool isJumping = false;
        int jumpProgress = 0;

        Random random = new Random();
        List<int> circles = new List<int>();

        Console.CursorVisible = false;

        // Hauptspielschleife
        while (true)
        {
            // Spiel zurücksetzen
            Console.Clear();
            playerY = landingGroundLevel - 1;
            isJumping = false;
            circles.Clear();
            circles.Add(Console.WindowWidth - 1);

            // Spielschleife
            while (true)
            {
                // Bildschirm löschen und Umgebungen zeichnen
                Console.Clear();

                // Decke zeichnen
                for (int x = 0; x < Console.WindowWidth; x++)
                {
                    Console.SetCursorPosition(x, ceilingLevel);
                    Console.Write("-");
                }

                // Boden zeichnen
                for (int x = 0; x < Console.WindowWidth; x++)
                {
                    Console.SetCursorPosition(x, landingGroundLevel);
                    Console.Write("-");
                }

                // Todeslinie zeichnen
                for (int x = 0; x < Console.WindowWidth; x++)
                {
                    Console.SetCursorPosition(x, deathGroundLevel);
                    Console.Write("-");
                }

                // Spieler zeichnen
                Console.SetCursorPosition(playerX, playerY);
                Console.Write(playerSymbol);

                // Kreise zeichnen (Spielhindernisse)
                for (int i = 0; i < circles.Count; i++)
                {
                    int circleX = circles[i];
                    DrawCircle(circleX, landingGroundLevel - 2);

                    // Kollisionsprüfung: Wenn ein Kreis den Spieler trifft
                    if (circleX == playerX && playerY >= landingGroundLevel - 3 && playerY <= landingGroundLevel - 1)
                    {
                        GameOver();
                        goto Restart;
                    }

                    circles[i]--;
                }

                // Entferne alle Kreise, die aus dem Bildschirm verschwunden sind
                circles.RemoveAll(circleX => circleX < 0);

                // Gelegentlich neuen Kreis hinzufügen
                if (random.Next(0, 10) < 2)
                {
                    circles.Add(Console.WindowWidth - 1);
                }

                // Tasteneingabe
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Spacebar && !isJumping)
                    {
                        isJumping = true;
                        jumpProgress = jumpHeight;
                    }
                }

                // Spieler springen
                if (isJumping)
                {
                    if (jumpProgress > 0 && playerY > ceilingLevel + 1)
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

        Restart:
            Thread.Sleep(2000);
        }
    }

    // Methode, um einen Kreis zu zeichnen
    static void DrawCircle(int x, int groundLevel)
    {
        Console.SetCursorPosition(x, groundLevel);
        Console.Write(" ()");
    }

    // GameOver Methode
    static void GameOver()
    {
        Console.Clear();
        Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2);
        Console.WriteLine("Game Over!");
        Thread.Sleep(2000);
    }



}



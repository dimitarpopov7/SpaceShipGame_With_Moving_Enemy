namespace SpaceShipGame_HighScores
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.IO;
    using System.Text.RegularExpressions;

    public class Program
    {
        public static void Main()
        {
            StartGame:
            //Enter Name
            Console.WriteLine("Please enter your name :)");
            var PlayerName = string.Empty;
            PlayerName = Console.ReadLine();
            if (PlayerName.Length == 0)
            {
                Console.Clear();
                Console.WriteLine("Invalid name!");
                goto StartGame;
            }
            // Choose spaceShip
            Console.Clear();
            SHIPPICK:
            Console.WriteLine("Choose your Space Ship");
            Console.WriteLine("For \\^/ press 1");
            Console.WriteLine("For /\\^/\\ press 2");
            Console.WriteLine("For custom press 3");
            string spaceShip = string.Empty;
            //read option
            
            ConsoleKeyInfo pickShip = Console.ReadKey();
            if (pickShip.Key == ConsoleKey.D1)
            {
                spaceShip = ($"\\^/");
            }
            else if (pickShip.Key == ConsoleKey.D2)
            {
                spaceShip = ($"/\\^/\\");
            }
            else if (pickShip.Key == ConsoleKey.D3)
            {
                Console.WriteLine();
                spaceShip=Console.ReadLine();
                if (spaceShip.Length % 2 == 0)
                {
                    Console.WriteLine("Please create a ship with \"Odd Symbol Lenght\", so that the blaster is Centred");
                    goto SHIPPICK;
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Press again");
                goto SHIPPICK;
            }


            //Set Screen
            Console.Clear();
            int width = 60;
            int height = 28;
            Console.SetWindowSize(width, height);
            Console.BufferHeight = height;
            Console.BufferWidth = width;
            Console.CursorVisible = false;

            //Setting up OuRSHiP

            //string spaceShip = ($"\\^/");
            int colOfSS = 0;
            int rowOfSS = Console.WindowHeight - 1;
            string projectile = "^";

            //Setting up the enemy

            char enemyShip = '*';
            Random EnemyGenerator = new Random();
            int RowMinEnemy = 1;
            int RowMaxEnemy = Console.WindowHeight / 2;
            int ColMinEnemy = 1;
            int ColMaxEnemy = Console.WindowWidth / 2;
            int colOfEnemy = EnemyGenerator.Next(ColMinEnemy, ColMaxEnemy);
            int rowOfEnemy = EnemyGenerator.Next(RowMinEnemy, RowMaxEnemy);
            int PlayerPoints = 0;
            //Game Starts

            Console.SetCursorPosition(colOfSS, rowOfSS);
            Console.Write(spaceShip);
            Console.SetCursorPosition(colOfEnemy, rowOfEnemy);
            Console.Write(enemyShip);


            while (true)
            {
                ConsoleKeyInfo currentKeyPressed = Console.ReadKey();
                if (currentKeyPressed.Key == ConsoleKey.LeftArrow && colOfSS > 0)
                {
                    colOfSS--;
                }
                else if (currentKeyPressed.Key == ConsoleKey.RightArrow && colOfSS < Console.WindowWidth - 1)
                {
                    colOfSS++;
                }
                else if (currentKeyPressed.Key == ConsoleKey.UpArrow && rowOfSS > 0)
                {
                    rowOfSS--;
                }
                else if (currentKeyPressed.Key == ConsoleKey.DownArrow && rowOfSS < Console.WindowHeight - 1)
                {
                    rowOfSS++;
                }
                else if (currentKeyPressed.Key == ConsoleKey.Spacebar)
                {
                    if (spaceShip == "\\^/")
                    {
                        int rowOfProjectile = rowOfSS - 1;
                        int colOfProjectile = colOfSS + 1;

                        while (rowOfProjectile > 0)
                        {
                            Console.Clear();
                            Console.SetCursorPosition(colOfProjectile, rowOfProjectile);
                            Console.Write(projectile);
                            Console.SetCursorPosition(colOfSS, rowOfSS);
                            Console.Write(spaceShip);
                            Console.SetCursorPosition(colOfEnemy, rowOfEnemy);
                            Console.Write(enemyShip);

                            Thread.Sleep(30);
                            if (colOfProjectile == colOfEnemy && rowOfProjectile == rowOfEnemy)
                            {
                                PlayerPoints++;

                                colOfEnemy = EnemyGenerator.Next(ColMinEnemy, ColMaxEnemy);
                                rowOfEnemy = EnemyGenerator.Next(RowMinEnemy, RowMaxEnemy);
                                break;

                            }
                            rowOfProjectile--;

                        }
                        if (rowOfProjectile == 0)
                        {
                            goto GameOver;
                        }
                    }
                    else
                    {
                        int rowOfProjectile = rowOfSS - 1;
                        int colOfProjectile = colOfSS + spaceShip.Length/2;

                        while (rowOfProjectile > 0)
                        {
                            Console.Clear();
                            Console.SetCursorPosition(colOfProjectile, rowOfProjectile);
                            Console.Write(projectile);
                            Console.SetCursorPosition(colOfSS, rowOfSS);
                            Console.Write(spaceShip);
                            Console.SetCursorPosition(colOfEnemy, rowOfEnemy);
                            Console.Write(enemyShip);

                            Thread.Sleep(30);
                            if (colOfProjectile == colOfEnemy && rowOfProjectile == rowOfEnemy)
                            {
                                PlayerPoints++;
                                colOfEnemy = EnemyGenerator.Next(ColMinEnemy, ColMaxEnemy);
                                rowOfEnemy = EnemyGenerator.Next(RowMinEnemy, RowMaxEnemy);
                                break;
                            }
                            rowOfProjectile--;

                        }
                        if (rowOfProjectile == 0)
                        {
                            goto GameOver;
                        }
                    }
                }
                Console.Clear();
                Console.SetCursorPosition(colOfSS, rowOfSS);
                Console.Write(spaceShip);
                Console.SetCursorPosition(colOfEnemy, rowOfEnemy);
                Console.Write(enemyShip);
            }
            GameOver:

            Console.Clear();
            //RECORD HIGHSCORES
            string highScores = "./HighScores.txt";
            Console.Clear();
            var getTopPlayersInfo = new Dictionary<string, int>();
            record:
            if (File.Exists(highScores))
            {
                var resultsRecord = File.ReadAllLines(highScores).ToList();
                resultsRecord.Add($"1. {PlayerName} - {PlayerPoints}");
                foreach (var line in resultsRecord)
                {
                    string pattern = @"\d*\. ([A-Z|a-z|0-9]*) - (\d*)";
                    var regex = new Regex(pattern);
                    var matches = regex.Matches(line);
                    
                    foreach (Match stats in matches)
                    {
                        string name = stats.Groups[1].Value.ToString();
                        int score = int.Parse(stats.Groups[2].Value);
                        if (!getTopPlayersInfo.ContainsKey(name))
                        {
                            getTopPlayersInfo[name] = score;
                        }
                        else
                        {
                            int previousScore = 0;
                            int currentScore = score;
                            foreach (var players in getTopPlayersInfo)
                            {
                                if (players.Key == name)
                                {
                                    previousScore = players.Value;
                                    if (previousScore < score)
                                    {
                                        getTopPlayersInfo[name] = score;
                                        break;
                                    }
                                }
                            }
                        }                        
                    }
                }
            }
            else
            {
                File.Create(highScores);
                goto record;
            }
            int place = 1;
            Console.WriteLine("HIGHSCORES");
            File.WriteAllText(highScores, String.Empty);
            foreach (var profile in getTopPlayersInfo.OrderByDescending(x=>x.Value).Take(5))
            {
                Console.WriteLine($"{place}. {profile.Key} - {profile.Value}");
                File.AppendAllText(highScores, $"{place}. {profile.Key} - {profile.Value}");
                File.AppendAllText(highScores, Environment.NewLine);
                place++;
            }
            Console.WriteLine();
            Console.WriteLine("Good luck next time!");
            Console.WriteLine();
            Console.WriteLine("To play again press Enter or any other key to end the game!");
            Console.WriteLine();
            Console.WriteLine("To delete Highscore and leave the game, please press DEL");
            ConsoleKeyInfo playAgainKey = Console.ReadKey();
            if (playAgainKey.Key == ConsoleKey.Enter)
            {
                Console.Clear();
                goto StartGame;
            }
            else if(playAgainKey.Key==ConsoleKey.Delete)
            {
                File.WriteAllText(highScores, String.Empty);
                return;
            }
        }
    }
}



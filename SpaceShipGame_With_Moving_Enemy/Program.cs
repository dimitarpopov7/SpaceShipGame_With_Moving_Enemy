using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceShipGame_With_Moving_Enemy
{
    class Program
    {
        static void Main(string[] args)
        {
            //For auto Color Change
            //Random r = new Random();
            //Console.ForegroundColor = (ConsoleColor)r.Next(0, 3);
            //Console.BackgroundColor = (ConsoleColor)r.Next(4, 6);
            //Console.Write(r.Next(0, 2));            


            StartGame:
            int width = 60;
            int height = 28;
            Console.SetWindowSize(width, height);
            Console.BufferHeight = height;
            Console.BufferWidth = width;
            Console.CursorVisible = false;

            //Setting up OuRSHiP

            string spaceShip = ($"\\^/");
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
                        goto PrintResult;
                    }

                }

                Console.Clear();
                Console.SetCursorPosition(colOfSS, rowOfSS);
                Console.Write(spaceShip);
                Console.SetCursorPosition(colOfEnemy, rowOfEnemy);
                Console.Write(enemyShip);
            }
            PrintResult:
            Console.Clear();
            Console.WriteLine($"You have {PlayerPoints} points, good luck next time! To play again press Enter or any other key to close the program!");

            ConsoleKeyInfo playAgainKey = Console.ReadKey();
            if (playAgainKey.Key == ConsoleKey.Enter)
            {
                goto StartGame;
            }
            else
            {
                Console.WriteLine("Thank you for playing");
                return;
            }
        }
    }
}
